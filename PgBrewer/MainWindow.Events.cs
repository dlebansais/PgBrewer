namespace PgBrewer;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Win32;
using WpfLayout;

/// <summary>
/// Main window implementation.
/// </summary>
public partial class MainWindow
{
    public override async void OnMainWindowClosing(object sender, CancelEventArgs e)
    {
        if (IsChanged)
        {
            MessageBoxResult Answer = MessageBox.Show("Save changes before exit?", "Closing", MessageBoxButton.YesNoCancel);

            switch (Answer)
            {
                case MessageBoxResult.Yes:
                    await SaveAll();
                    break;

                case MessageBoxResult.No:
                    break;

                default:
                case MessageBoxResult.Cancel:
                    e.Cancel = true;
                    break;
            }
        }
    }

    public override async void OnSave(object sender, ExecutedRoutedEventArgs e)
    {
        await SaveAll();
        SetIsChanged(false);
    }

    public override void OnDeleteLine(object sender, ExecutedRoutedEventArgs e)
    {
        AlcoholLine Line = (AlcoholLine)((Button)e.OriginalSource).DataContext;
        Line.EffectIndex = -1;
    }

    public override void OnDelete(object sender, ExecutedRoutedEventArgs e)
    {
        ComponentAssociation Association = (ComponentAssociation)((Button)e.OriginalSource).DataContext;
        Association.AssociationIndex = -1;
    }

    public override void OnExport(object sender, ExecutedRoutedEventArgs e)
    {
        FileDialogResult Result = (FileDialogResult)e.Parameter;

        if (Result.FilePath.Length == 0)
            Result.FilePath = "Brewing.csv";

        using MemoryStream Stream = new MemoryStream();
        using StreamWriter Writer = new StreamWriter(Stream, Encoding.UTF8);
        OnExport(Writer);
        Writer.Flush();

        Stream.Seek(0, SeekOrigin.Begin);
        using StreamReader Reader = new StreamReader(Stream, Encoding.UTF8, false, (int)Stream.Length, true);
        string Content = Reader.ReadToEnd();

        Result.Content = Content;
    }

    private void OnExport(StreamWriter writer)
    {
        ExportVersionNumber(writer);
        ExportAssociations(writer);
    }

    private void ExportVersionNumber(StreamWriter writer)
    {
        if (SystemTools.GetVersion() is string Version)
            writer.WriteLine($"{VersionProlog}{Version}");
    }

    public override void OnImport(object sender, ExecutedRoutedEventArgs e)
    {
        FileDialogResult Result = (FileDialogResult)e.Parameter;
        OnImport(Result.Content);
    }

    private void OnImport(string content)
    {
        try
        {
            byte[] ContentBytes = Encoding.UTF8.GetBytes(content);
            using MemoryStream Stream = new MemoryStream(ContentBytes);
            using StreamReader Reader = new StreamReader(Stream, Encoding.UTF8);

            int ChangeCount = 0;
            if (OnImport(Reader, ref ChangeCount))
                if (ChangeCount == 0)
                    MessageBox.Show("The imported file contains the same data as the software.\r\n\r\nNo change made.", "Import", MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                    MessageBox.Show("File content imported.", "Import", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
        }
    }

    private bool OnImport(StreamReader reader, ref int changeCount)
    {
        if (!ImportVersionNumber(reader))
            return false;

        if (OnImportConfirmed(reader, ref changeCount))
            return true;
        else
        {
            MessageBox.Show("Invalid format, not all of the file content was imported.", "Import", MessageBoxButton.OK, MessageBoxImage.Error);
            return false;
        }
    }

    private bool ImportVersionNumber(StreamReader reader)
    {
        if (SystemTools.GetVersion() is not string Version)
            return false;

        if (reader.ReadLine() is not string Line)
            return false;

        if (!Line.StartsWith(VersionProlog))
            return false;

        Line = Line.Substring(VersionProlog.Length);

        if (Line != Version)
        {
            MessageBoxResult Answer = MessageBox.Show($"This file was exported from version {Line}. The current version is {Version} and is probably not compatible. Continue?", "Import", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (Answer != MessageBoxResult.Yes)
                return false;
        }

        return true;
    }

    private bool OnImportConfirmed(StreamReader reader, ref int changeCount)
    {
        return ImportAssociations(reader, ref changeCount);
    }

    public override void OnBack(object sender, ExecutedRoutedEventArgs e)
    {
        ChangeLine(-1);
    }

    public override void OnForward(object sender, ExecutedRoutedEventArgs e)
    {
        ChangeLine(+1);
    }

    private void ChangeLine(int offset)
    {
        PgBrewerPage SelectedPage = PageList[SelectedPageIndex];
        if (SelectedPage is IAlcoholPage AsAlcoholPage)
        {
            int SelectedAlcoholIndex = AsAlcoholPage.SelectedAlcoholIndex;
            IList<Alcohol> AlcoholList = AsAlcoholPage.AlcoholList;

            if (SelectedAlcoholIndex >= 0 && SelectedAlcoholIndex < AlcoholList.Count)
            {
                Alcohol SelectedAlcohol = AlcoholList[SelectedAlcoholIndex];
                int SelectedLine = SelectedAlcohol.SelectedLine;
                IList<AlcoholLine> LineList = SelectedAlcohol.Lines;

                if (SelectedLine >= 0 && SelectedLine < LineList.Count)
                {
                    AlcoholLine Line = LineList[SelectedLine];
                    Alcohol Owner = SelectedAlcohol;

                    int NewLine = -1;
                    if (offset < 0)
                    {
                        IFourComponentsAlcohol Next = (IFourComponentsAlcohol)Owner;
                        IFourComponentsAlcohol Previous = (IFourComponentsAlcohol)Owner.Previous;
                        List<ComponentAssociationCollection> PreviousToNext = ((Alcohol)Previous).PreviousToNext;

                        int NextLineIndex = Owner.Lines.IndexOf(Line);
                        GetPreviousLineIndex(Next, Previous, PreviousToNext[0], PreviousToNext[1], PreviousToNext[2], PreviousToNext[3], NextLineIndex, out NewLine);
                    }
                    else
                    {
                        IFourComponentsAlcohol Previous = (IFourComponentsAlcohol)Owner;
                        IFourComponentsAlcohol Next = (IFourComponentsAlcohol)Owner.Next;
                        List<ComponentAssociationCollection> PreviousToNext = ((Alcohol)Previous).PreviousToNext;

                        int PreviousLineIndex = Owner.Lines.IndexOf(Line);
                        GetNextLineIndex(Previous, Next, PreviousToNext[0], PreviousToNext[1], PreviousToNext[2], PreviousToNext[3], PreviousLineIndex, out NewLine);
                    }

                    if (NewLine >= 0)
                    {
                        AsAlcoholPage.SelectedAlcoholIndex = SelectedAlcoholIndex + offset;
                        int TotalLines = Owner.Lines.Count;

                        TaskDispatcher.Dispatch(() => OnLineMove(NewLine, TotalLines));
                    }
                }
            }
        }
    }

    private void OnLineMove(int newLineIndex, int totalLines)
    {
        Debug.Assert(SelectedPageIndex >= 0 && SelectedPageIndex < PageList.Count);

        PgBrewerPage SelectedPage = PageList[SelectedPageIndex];
        if (SelectedPage is IAlcoholPage AsAlcoholPage)
        {
            int SelectedAlcoholIndex = AsAlcoholPage.SelectedAlcoholIndex;
            IList<Alcohol> AlcoholList = AsAlcoholPage.AlcoholList;

            if (SelectedAlcoholIndex >= 0 && SelectedAlcoholIndex < AlcoholList.Count)
            {
                Alcohol SelectedAlcohol = AlcoholList[SelectedAlcoholIndex];
                IList<AlcoholLine> LineList = SelectedAlcohol.Lines;

                if (newLineIndex >= 0 && newLineIndex < LineList.Count)
                    SelectedAlcohol.SelectedLine = newLineIndex;
            }
        }
    }
}

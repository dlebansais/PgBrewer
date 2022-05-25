namespace PgBrewer;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using Microsoft.Win32;

/// <summary>
/// Main window implementation.
/// </summary>
public partial class MainWindow
{
    public override void OnClosing(object sender, CancelEventArgs e)
    {
        if (IsChanged)
        {
            MessageBoxResult Answer = MessageBox.Show("Save changes before exit?", "Closing", MessageBoxButton.YesNoCancel);

            switch (Answer)
            {
                case MessageBoxResult.Yes:
                    SaveAll();
                    break;

                case MessageBoxResult.No:
                    break;

                default:
                case MessageBoxResult.Cancel:
                    e.Cancel = true;
                    break;
            }
        }

        if (!e.Cancel)
            SaveGUI();
    }

    public override void OnSave(object sender, ExecutedRoutedEventArgs e)
    {
        SaveAll();
        SetIsChanged(false);
    }

    private void SaveAll()
    {
        PageBeers.BasicLager.Save();
        PageBeers.PaleAle.Save();
        PageBeers.Marzen.Save();
        PageBeers.GoblinAle.Save();
        PageBeers.OrcishBock.Save();
        PageBeers.BrownAle.Save();
        PageBeers.HegemonyLager.Save();
        PageBeers.DwarvenStout.Save();
        PageLiquors.PotatoVodka.Save();
        PageLiquors.Applejack.Save();
        PageLiquors.BeetVodka.Save();
        PageLiquors.PaleRum.Save();
        PageLiquors.Whisky.Save();
        PageLiquors.Tequila.Save();
        PageLiquors.DryGin.Save();
        PageLiquors.Bourbon.Save();

        SaveAssociations();
        SaveGUI();
    }

    private void SaveAssociations()
    {
        foreach (ComponentAssociationCollection AssociationList in AssociationTable)
        {
            List<int> IndexList = new List<int>();
            for (int i = 0; i < AssociationList.Count; i++)
                IndexList.Add(AssociationList[i].AssociationIndex);

            DataArchive.SetIndexList($"{AssociationSettingName}{AssociationList.Name}", IndexList);
        }
    }

    private void SaveGUI()
    {
        if (WindowState != WindowState.Normal)
            return;

        List<int> Coordinates = new List<int>();
        Coordinates.Add((int)Left);
        Coordinates.Add((int)Top);
        Coordinates.Add((int)ActualWidth);
        Coordinates.Add((int)ActualHeight);

        DataArchive.SetIndexList(GuiSettingName, Coordinates);
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
        SaveFileDialog Dlg = new SaveFileDialog();
        Dlg.Filter = "CSV file (*.csv)|*.csv";
        bool? Continue = Dlg.ShowDialog(this);

        if (Continue.HasValue && Continue.Value)
            OnExport(Dlg.FileName);
    }

    private void OnExport(string fileName)
    {
        try
        {
            using (FileStream Stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                using (StreamWriter Writer = new StreamWriter(Stream, Encoding.UTF8))
                {
                    OnExport(Writer);
                }
            }
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
        }
    }

    private void OnExport(StreamWriter writer)
    {
        ExportVersionNumber(writer);

        ExportAssociations(writer);

        PageBeers.BasicLager.Export(writer);
        PageBeers.PaleAle.Export(writer);
        PageBeers.Marzen.Export(writer);
        PageBeers.GoblinAle.Export(writer);
        PageBeers.OrcishBock.Export(writer);
        PageBeers.BrownAle.Export(writer);
        PageBeers.HegemonyLager.Export(writer);
        PageBeers.DwarvenStout.Export(writer);
        PageLiquors.PotatoVodka.Export(writer);
        PageLiquors.Applejack.Export(writer);
        PageLiquors.BeetVodka.Export(writer);
        PageLiquors.PaleRum.Export(writer);
        PageLiquors.Whisky.Export(writer);
        PageLiquors.Tequila.Export(writer);
        PageLiquors.DryGin.Export(writer);
        PageLiquors.Bourbon.Export(writer);
    }

    private void ExportVersionNumber(StreamWriter writer)
    {
        string Version = GetVersion();
        writer.WriteLine($"{VersionProlog}{Version}");
    }

    private void ExportAssociations(StreamWriter writer)
    {
        writer.WriteLine("Associations");
        writer.WriteLine();

        foreach (ComponentAssociationCollection AssociationList in AssociationTable)
        {
            writer.WriteLine(AssociationList.Name);

            foreach (ComponentAssociation Association in AssociationList)
            {
                string AssociationName = Association.Component.Name;

                if (Association.AssociationIndex >= 0)
                    writer.WriteLine($"{AssociationName};{Association.ChoiceList[Association.AssociationIndex]}");
                else
                    writer.WriteLine($"{AssociationName};");
            }

            writer.WriteLine();
        }

        writer.WriteLine();
    }

    public override void OnImport(object sender, ExecutedRoutedEventArgs e)
    {
        OpenFileDialog Dlg = new OpenFileDialog();
        Dlg.Filter = "CSV file (*.csv)|*.csv";
        bool? Continue = Dlg.ShowDialog(this);

        if (Continue.HasValue && Continue.Value)
            OnImport(Dlg.FileName);
    }

    private void OnImport(string fileName)
    {
        try
        {
            using (FileStream Stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader Reader = new StreamReader(Stream, Encoding.UTF8))
                {
                    int ChangeCount = 0;
                    if (OnImport(Reader, ref ChangeCount))
                        if (ChangeCount == 0)
                            MessageBox.Show("The imported file contains the same data as the software.\r\n\r\nNo change made.", "Import", MessageBoxButton.OK, MessageBoxImage.Warning);
                        else
                            MessageBox.Show("File content imported.", "Import", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
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
        string Version = GetVersion();

        string Line = reader.ReadLine()!;
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
        if (!ImportAssociations(reader, ref changeCount))
            return false;

        if (!PageBeers.BasicLager.Import(reader, ref changeCount))
            return false;

        if (!PageBeers.PaleAle.Import(reader, ref changeCount))
            return false;

        if (!PageBeers.Marzen.Import(reader, ref changeCount))
            return false;

        if (!PageBeers.GoblinAle.Import(reader, ref changeCount))
            return false;

        if (!PageBeers.OrcishBock.Import(reader, ref changeCount))
            return false;

        if (!PageBeers.BrownAle.Import(reader, ref changeCount))
            return false;

        if (!PageBeers.HegemonyLager.Import(reader, ref changeCount))
            return false;

        if (!PageBeers.DwarvenStout.Import(reader, ref changeCount))
            return false;

        if (!PageLiquors.PotatoVodka.Import(reader, ref changeCount))
            return false;

        if (!PageLiquors.Applejack.Import(reader, ref changeCount))
            return false;

        if (!PageLiquors.BeetVodka.Import(reader, ref changeCount))
            return false;

        if (!PageLiquors.PaleRum.Import(reader, ref changeCount))
            return false;

        if (!PageLiquors.Whisky.Import(reader, ref changeCount))
            return false;

        if (!PageLiquors.Tequila.Import(reader, ref changeCount))
            return false;

        if (!PageLiquors.DryGin.Import(reader, ref changeCount))
            return false;

        if (!PageLiquors.Bourbon.Import(reader, ref changeCount))
            return false;

        return true;
    }

    private bool ImportAssociations(StreamReader reader, ref int changeCount)
    {
        if (reader.ReadLine() != "Associations")
            return false;

        reader.ReadLine();

        foreach (ComponentAssociationCollection AssociationList in AssociationTable)
        {
            if (AssociationList.Name != reader.ReadLine())
                return false;

            foreach (ComponentAssociation Association in AssociationList)
            {
                string AssociationString = reader.ReadLine()!;
                string AssociationName = Association.Component.Name;
                AssociationName += ";";

                if (!AssociationString.StartsWith(AssociationName))
                    return false;

                AssociationString = AssociationString.Substring(AssociationName.Length);
                if (AssociationString.Length > 0)
                {
                    int SelectedIndex = -1;
                    for (int i = 0; i < Association.ChoiceList.Count; i++)
                    {
                        Component Choice = Association.ChoiceList[i];
                        if (Choice.ToString() == AssociationString)
                        {
                            SelectedIndex = i;
                            break;
                        }
                    }

                    if (SelectedIndex < 0)
                        return false;

                    if (Association.AssociationIndex != SelectedIndex)
                    {
                        Association.AssociationIndex = SelectedIndex;
                        changeCount++;
                    }
                }
                else if (Association.AssociationIndex >= 0)
                {
                    Association.AssociationIndex = -1;
                    changeCount++;
                }
            }

            reader.ReadLine();
        }

        reader.ReadLine();

        return true;
    }

    private string GetVersion()
    {
        Assembly CurrentAssembly = Assembly.GetExecutingAssembly();
#pragma warning disable IL3000 // Avoid using accessing Assembly file path when publishing as a single-file
        FileVersionInfo VersionInfo = FileVersionInfo.GetVersionInfo(CurrentAssembly.Location);
#pragma warning restore IL3000 // Avoid using accessing Assembly file path when publishing as a single-file

        return VersionInfo.FileVersion!;
    }

    public void OnGotFocus(ComboBox sender)
    {
        LastFocusedCombo = sender;

        AlcoholLine Line = (AlcoholLine)sender.DataContext;
        Alcohol Owner = Line.Owner;

        BackForward.CanGoBack = Owner.Previous != null;
        BackForward.CanGoForward = Owner.Next != null;
    }

    public void OnLostFocus(ComboBox sender)
    {
        // CanGoBack = false;
        // CanGoForward = false;
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
        if (LastFocusedCombo != null && FindTabControl(out TabControl CtrlPage))
        {
            Debug.Assert(CtrlPage.SelectedIndex + offset >= 0 && CtrlPage.SelectedIndex + offset < CtrlPage.Items.Count);

            AlcoholLine Line = (AlcoholLine)LastFocusedCombo.DataContext;
            Alcohol Owner = Line.Owner;

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
                CtrlPage.SelectedIndex = CtrlPage.SelectedIndex + offset;
                int TotalLines = Owner.Lines.Count;
                Dispatcher.BeginInvoke(DispatcherPriority.ContextIdle, new LineMoveHandler(OnLineMove), CtrlPage, NewLine, TotalLines);
            }
        }
    }

    private delegate void LineMoveHandler(TabControl ctrlPage, int newLineIndex, int totalLines);

    private void OnLineMove(TabControl ctrlPage, int newLineIndex, int totalLines)
    {
        FrameworkElement Root = (FrameworkElement)ctrlPage.SelectedContent;
        if (Tools.FindFirstControl(Root, out ScrollViewer FirstScrollViewer))
        {
            FirstScrollViewer.ScrollToVerticalOffset((double)newLineIndex / (double)totalLines);
            if (Tools.FindFirstControl(FirstScrollViewer, out ItemsControl FirstItemsControl))
            {
                ItemContainerGenerator Generator = FirstItemsControl.ItemContainerGenerator;
                FrameworkElement LineContent = (FrameworkElement)Generator.ContainerFromIndex(newLineIndex);
                if (Tools.FindFirstControl(LineContent, out ComboBox FirstComboBox))
                {
                    Dispatcher.BeginInvoke(DispatcherPriority.ContextIdle, new SetLineFocusHandler(OnSetLineFocus), FirstComboBox);
                }
            }
        }
    }

    private delegate void SetLineFocusHandler(ComboBox firstComboBox);
    private void OnSetLineFocus(ComboBox firstComboBox)
    {
        firstComboBox.Focus();
    }

    private bool FindTabControl(out TabControl ctrlPage)
    {
        FrameworkElement? Ctrl = LastFocusedCombo;

        while (Ctrl != null)
        {
            if (Ctrl is TabControl AsTabControl)
            {
                ctrlPage = AsTabControl;
                return true;
            }

            Ctrl = VisualTreeHelper.GetParent(Ctrl) as FrameworkElement;
        }

        ctrlPage = null!;
        return false;
    }

    private ComboBox? LastFocusedCombo;
}

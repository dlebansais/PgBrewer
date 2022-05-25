namespace PgBrewer;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Contracts;
using Microsoft.Win32;

/// <summary>
/// Main window implementation.
/// </summary>
public partial class MainWindow : MainWindowUI
{
    #region Constants
    private static readonly string AssociationSettingName = "Association";
    private static readonly string GuiSettingName = "GUI";
    private static readonly string VersionProlog = "Exported from PgBrewing.exe version ";
    #endregion

    #region Init
    public MainWindow()
    {
        LoadAssociations();
        LoadGUI();
        LoadIcons();
        IsChangedInternal = false;
        PageBeers.SetSelected(true);

        Loaded += OnLoaded;
    }

    private void LoadAssociations()
    {
        foreach (PgBrewerPage Page in PageList)
            if (Page is PgBrewerPageSettings AsPageSettings)
            {
                LoadAssociations(AsPageSettings.AssociationFruit1);
                LoadAssociations(AsPageSettings.AssociationFruit2);
                LoadAssociations(AsPageSettings.AssociationVeggie1);
                LoadAssociations(AsPageSettings.AssociationVeggie2Beer);
                LoadAssociations(AsPageSettings.AssociationVeggie2Liquor);
                LoadAssociations(AsPageSettings.AssociationMushroom3);
                LoadAssociations(AsPageSettings.AssociationParts1);
                LoadAssociations(AsPageSettings.AssociationParts2);
                LoadAssociations(AsPageSettings.AssociationFlavor1Beer);
                LoadAssociations(AsPageSettings.AssociationFlavor1Liquor);
                LoadAssociations(AsPageSettings.AssociationFlavor2Beer);
                LoadAssociations(AsPageSettings.AssociationFlavor2Liquor);
            }
    }

    private void LoadAssociations(ComponentAssociationCollection associationList)
    {
        List<int> AssociationIndexes = DataArchive.GetIndexList($"{AssociationSettingName}{associationList.Name}", associationList.Count);
        for (int i = 0; i < associationList.Count; i++)
            associationList[i].AssociationIndex = AssociationIndexes[i];

        AssociationTable.Add(associationList);
    }

    private void LoadGUI()
    {
        List<int> Coordinates = DataArchive.GetIndexList(GuiSettingName, 4);

        if (Coordinates[0] >= 0)
            Left = Coordinates[0];

        if (Coordinates[1] >= 0)
            Top = Coordinates[1];

        if (Coordinates[2] >= 0)
            Width = Coordinates[2];

        if (Coordinates[3] >= 0)
            Height = Coordinates[3];

        if (Coordinates[2] >= 0 && Coordinates[3] >= 0)
            SizeToContent = SizeToContent.Manual;
    }

    private void LoadIcons()
    {
        try
        {
            string UserRootFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string ApplicationFolder = Path.Combine(UserRootFolder, "PgJsonParse");
            string VersionCacheFolder = Path.Combine(ApplicationFolder, "Versions");

            if (!Directory.Exists(VersionCacheFolder))
                return;

            string? FinalFolder = null;
            string SharedFolder = Path.Combine(ApplicationFolder, "Shared Icons");

            if (Directory.Exists(SharedFolder))
                FinalFolder = SharedFolder;
            else
            {
                string[] VersionFolders = Directory.GetDirectories(VersionCacheFolder);
                int LastVersion = -1;

                foreach (string Folder in VersionFolders)
                {
                    if (int.TryParse(Path.GetFileName(Folder), out int FolderVersion))
                        if (LastVersion < FolderVersion)
                            LastVersion = FolderVersion;
                }

                if (LastVersion > 0)
                    FinalFolder = Path.Combine(VersionCacheFolder, LastVersion.ToString());
            }

            if (FinalFolder != null)
            {
                IconBeer = new BitmapImage(new Uri(Path.Combine(FinalFolder, "icon_5744.png")));
                IconLiquor = new BitmapImage(new Uri(Path.Combine(FinalFolder, "icon_5746.png")));
                IconSettings = new BitmapImage(new Uri(Path.Combine(FinalFolder, "icon_5476.png")));
            }
        }
        catch
        {
        }
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        Alcohol.Chain(PageBeers.OrcishBock, PageBeers.BrownAle, new List<ComponentAssociationCollection>() { PageSettings.AssociationVeggie2Beer, ComponentAssociationCollection.None, ComponentAssociationCollection.None, PageSettings.AssociationFlavor1Beer });
        Alcohol.Chain(PageBeers.BrownAle, PageBeers.HegemonyLager, new List<ComponentAssociationCollection>() { ComponentAssociationCollection.None, PageSettings.AssociationFruit2, ComponentAssociationCollection.None, ComponentAssociationCollection.None });
        Alcohol.Chain(PageBeers.HegemonyLager, PageBeers.DwarvenStout, new List<ComponentAssociationCollection>() { ComponentAssociationCollection.None, ComponentAssociationCollection.None, PageSettings.AssociationMushroom3, PageSettings.AssociationFlavor2Beer });

        Alcohol.Chain(PageLiquors.PotatoVodka, PageLiquors.Applejack, new List<ComponentAssociationCollection>() { ComponentAssociationCollection.None, PageSettings.AssociationVeggie1, ComponentAssociationCollection.None, ComponentAssociationCollection.None });
        Alcohol.Chain(PageLiquors.Applejack, PageLiquors.BeetVodka, new List<ComponentAssociationCollection>() { PageSettings.AssociationFruit1, ComponentAssociationCollection.None, ComponentAssociationCollection.None, ComponentAssociationCollection.None });
        Alcohol.Chain(PageLiquors.BeetVodka, PageLiquors.PaleRum, new List<ComponentAssociationCollection>() { ComponentAssociationCollection.None, ComponentAssociationCollection.None, ComponentAssociationCollection.None, ComponentAssociationCollection.None });
        Alcohol.Chain(PageLiquors.PaleRum, PageLiquors.Whisky, new List<ComponentAssociationCollection>() { ComponentAssociationCollection.None, ComponentAssociationCollection.None, PageSettings.AssociationParts1, ComponentAssociationCollection.None });
        Alcohol.Chain(PageLiquors.Whisky, PageLiquors.Tequila, new List<ComponentAssociationCollection>() { ComponentAssociationCollection.None, ComponentAssociationCollection.None, ComponentAssociationCollection.None, PageSettings.AssociationFlavor1Liquor });
        Alcohol.Chain(PageLiquors.Tequila, PageLiquors.DryGin, new List<ComponentAssociationCollection>() { PageSettings.AssociationFruit2, PageSettings.AssociationMushroom3, ComponentAssociationCollection.None, ComponentAssociationCollection.None });
        Alcohol.Chain(PageLiquors.DryGin, PageLiquors.Bourbon, new List<ComponentAssociationCollection>() { ComponentAssociationCollection.None, ComponentAssociationCollection.None, PageSettings.AssociationParts2, PageSettings.AssociationFlavor2Liquor });

        Recalculate();
    }
    #endregion

    #region Properties
    public static BackForward BackForward { get; } = new();
    public static PgBrewerPageBeers PageBeers { get; } = new PgBrewerPageBeers(BackForward);
    public static PgBrewerPageLiquors PageLiquors { get; } = new PgBrewerPageLiquors(BackForward);
    public static PgBrewerPageSettings PageSettings { get; } = new PgBrewerPageSettings(BackForward);

    public ImageSource? IconBeer { get; private set; }
    public ImageSource? IconLiquor { get; private set; }
    public ImageSource? IconSettings { get; private set; }
    public override ObservableCollection<PgBrewerPage> PageList { get; } = new()
    {
        PageBeers,
        PageLiquors,
        PageSettings,
    };

    public override int SelectedPageIndex
    {
        get
        {
            return SelectedPageIndexInternal;
        }
        set
        {
            if (SelectedPageIndexInternal != value)
            {
                SelectedPageIndexInternal = value;

                for (int i = 0; i < PageList.Count; i++)
                    PageList[i].SetSelected(i == SelectedPageIndexInternal);
            }
        }
    }

    private int SelectedPageIndexInternal = 0;

    public bool IsChanged
    {
        get => IsChangedInternal;
        private set
        {
            if (IsChangedInternal != value)
            {
                IsChangedInternal = value;
                NotifyThisPropertyChanged();
            }
        }
    }

    private bool IsChangedInternal;

    public List<ComponentAssociationCollection> AssociationTable { get; } = new List<ComponentAssociationCollection>();
    #endregion

    #region Events
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

    public override void OnSave(object sender, RoutedEventArgs e)
    {
        SaveAll();
        IsChanged = false;
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

    public override void OnDeleteLine(object sender, RoutedEventArgs e)
    {
        AlcoholLine Line = (AlcoholLine)((Button)e.OriginalSource).DataContext;
        Line.EffectIndex = -1;
    }

    public override void OnDelete(object sender, RoutedEventArgs e)
    {
        ComponentAssociation Association = (ComponentAssociation)((Button)sender).DataContext;
        Association.AssociationIndex = -1;
    }

    public override void OnExport(object sender, RoutedEventArgs e)
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

    public override void OnImport(object sender, RoutedEventArgs e)
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

    public override void OnBack(object sender, RoutedEventArgs e)
    {
        ChangeLine(-1);
    }

    public override void OnForward(object sender, RoutedEventArgs e)
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

        Contract.Unused(out ctrlPage);
        return false;
    }

    private ComboBox? LastFocusedCombo;
    #endregion

    #region Client Interface
    public void Recalculate()
    {
        RecalculateFromBottom(PageBeers.OrcishBock);
        RecalculateFromBottom(PageLiquors.PotatoVodka);
    }

    public void RecalculateFromBottom(IFourComponentsAlcohol start)
    {
        IFourComponentsAlcohol Previous = start;
        IFourComponentsAlcohol Next;
        List<ComponentAssociationCollection> AssociationLists;

        ((Alcohol)Previous).ClearCalculateIndexes();

        for (; ;)
        {
            Alcohol NextAlcohol = ((Alcohol)Previous).Next;
            if (NextAlcohol == Alcohol.None)
                break;

            Next = (IFourComponentsAlcohol)NextAlcohol;

            ((Alcohol)Next).ClearCalculateIndexes();

            AssociationLists = ((Alcohol)Previous).PreviousToNext;
            RecalculateBottomToTop(Previous, Next, AssociationLists[0], AssociationLists[1], AssociationLists[2], AssociationLists[3]);

            Previous = Next;
        }

        Next = Previous;

        for (; ;)
        {
            ((Alcohol)Next).RecalculateMismatchCount();

            Alcohol PreviousAlcohol = ((Alcohol)Next).Previous;
            if (PreviousAlcohol == Alcohol.None)
                break;

            Previous = (IFourComponentsAlcohol)PreviousAlcohol;

            AssociationLists = ((Alcohol)Previous).PreviousToNext;
            RecalculateTopToBottom(Next, Previous, AssociationLists[0], AssociationLists[1], AssociationLists[2], AssociationLists[3]);

            Next = Previous;
        }
    }

    public void RecalculateBottomToTop(IFourComponentsAlcohol previous, IFourComponentsAlcohol next, ComponentAssociationCollection associationList1, ComponentAssociationCollection associationList2, ComponentAssociationCollection associationList3, ComponentAssociationCollection associationList4)
    {
        Debug.Assert((previous is Alcohol) && (next is Alcohol) && ((Alcohol)previous).Next == next && ((Alcohol)next).Previous == previous);

        int Multiplier1 = previous.Multiplier1;
        int Multiplier2 = previous.Multiplier2;
        int Multiplier3 = previous.Multiplier3;
        Debug.Assert(Multiplier1 == next.Multiplier1);
        Debug.Assert(Multiplier2 == next.Multiplier2);
        Debug.Assert(Multiplier3 == next.Multiplier3);

        for (int PreviousLineIndex = 0; PreviousLineIndex < previous.Lines.Count; PreviousLineIndex++)
            if (GetNextLineIndex(previous, next, associationList1, associationList2, associationList3, associationList4, PreviousLineIndex, out int NextLineIndex))
                next.Lines[NextLineIndex].CalculatedIndex = previous.Lines[PreviousLineIndex].BestIndex;
    }

    public bool GetNextLineIndex(IFourComponentsAlcohol previous, IFourComponentsAlcohol next, ComponentAssociationCollection associationList1, ComponentAssociationCollection associationList2, ComponentAssociationCollection associationList3, ComponentAssociationCollection associationList4, int previousLineIndex, out int nextLineIndex)
    {
        IFourComponentsAlcoholLine Line = (IFourComponentsAlcoholLine)previous.Lines[previousLineIndex];
        int BestIndex = Line.BestIndex;
        if (BestIndex >= 0)
        {
            int NextIndex1 = GetPreviousToNextIndex(associationList1, Line.Index1);
            int NextIndex2 = GetPreviousToNextIndex(associationList2, Line.Index2);
            int NextIndex3 = GetPreviousToNextIndex(associationList3, Line.Index3);
            int NextIndex4 = GetPreviousToNextIndex(associationList4, Line.Index4);

            if (NextIndex1 >= 0 && NextIndex2 >= 0 && NextIndex3 >= 0 && NextIndex4 >= 0)
            {
                int Multiplier1 = previous.Multiplier1;
                int Multiplier2 = previous.Multiplier2;
                int Multiplier3 = previous.Multiplier3;

                nextLineIndex = (NextIndex1 * Multiplier1) + (NextIndex2 * Multiplier2) + (NextIndex3 * Multiplier3) + NextIndex4;
                return true;
            }
        }

        nextLineIndex = -1;
        return false;
    }

    public void RecalculateTopToBottom(IFourComponentsAlcohol next, IFourComponentsAlcohol previous, ComponentAssociationCollection associationList1, ComponentAssociationCollection associationList2, ComponentAssociationCollection associationList3, ComponentAssociationCollection associationList4)
    {
        int Multiplier1 = next.Multiplier1;
        int Multiplier2 = next.Multiplier2;
        int Multiplier3 = next.Multiplier3;
        Debug.Assert(Multiplier1 == previous.Multiplier1);
        Debug.Assert(Multiplier2 == previous.Multiplier2);
        Debug.Assert(Multiplier3 == previous.Multiplier3);

        for (int NextLineIndex = 0; NextLineIndex < next.Lines.Count; NextLineIndex++)
            if (GetPreviousLineIndex(next, previous, associationList1, associationList2, associationList3, associationList4, NextLineIndex, out int PreviousLineIndex))
                previous.Lines[PreviousLineIndex].CalculatedIndex = next.Lines[NextLineIndex].BestIndex;
    }

    public bool GetPreviousLineIndex(IFourComponentsAlcohol next, IFourComponentsAlcohol previous, ComponentAssociationCollection associationList1, ComponentAssociationCollection associationList2, ComponentAssociationCollection associationList3, ComponentAssociationCollection associationList4, int nextLineIndex, out int previousLineIndex)
    {
        IFourComponentsAlcoholLine Line = (IFourComponentsAlcoholLine)next.Lines[nextLineIndex];
        int BestIndex = Line.BestIndex;
        if (BestIndex >= 0)
        {
            int PreviousIndex1 = GetNextToPreviousIndex(associationList1, Line.Index1);
            int PreviousIndex2 = GetNextToPreviousIndex(associationList2, Line.Index2);
            int PreviousIndex3 = GetNextToPreviousIndex(associationList3, Line.Index3);
            int PreviousIndex4 = GetNextToPreviousIndex(associationList4, Line.Index4);

            if (PreviousIndex1 >= 0 && PreviousIndex2 >= 0 && PreviousIndex3 >= 0 && PreviousIndex4 >= 0)
            {
                int Multiplier1 = next.Multiplier1;
                int Multiplier2 = next.Multiplier2;
                int Multiplier3 = next.Multiplier3;

                previousLineIndex = (PreviousIndex1 * Multiplier1) + (PreviousIndex2 * Multiplier2) + (PreviousIndex3 * Multiplier3) + PreviousIndex4;
                return true;
            }
        }

        previousLineIndex = -1;
        return false;
    }

    private int GetPreviousToNextIndex(ComponentAssociationCollection associationList, int previousIndex)
    {
        int NextIndex;

        if (associationList.IsNone)
            NextIndex = previousIndex;
        else if (associationList[previousIndex].AssociationIndex >= 0)
            NextIndex = associationList[previousIndex].AssociationIndex;
        else
            NextIndex = -1;

        return NextIndex;
    }

    private int GetNextToPreviousIndex(ComponentAssociationCollection associationList, int nextIndex)
    {
        int PreviousIndex;

        if (associationList.IsNone)
            PreviousIndex = nextIndex;
        else
        {
            PreviousIndex = -1;

            for (int i = 0; i < associationList.Count; i++)
                if (associationList[i].AssociationIndex == nextIndex)
                {
                    PreviousIndex = i;
                    break;
                }
        }

        return PreviousIndex;
    }

    public static void SetChanged()
    {
        MainWindow Window = (MainWindow)App.Current.MainWindow;
        Window.IsChanged = true;
    }
    #endregion
}

namespace PgBrewer;

/// <summary>
/// Main window implementation.
/// </summary>
public partial class MainWindow : MainWindowUI
{
    public MainWindow()
    {
        LoadAssociations();
        LoadGUI();
        LoadIcons();
        IsChangedInternal = false;
        PageBeers.SetSelected(true);

        Loaded += OnLoaded;
    }
}

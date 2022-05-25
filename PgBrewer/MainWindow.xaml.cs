namespace PgBrewer;

using WpfLayout;

/// <summary>
/// Main window implementation.
/// </summary>
public partial class MainWindow : MainWindowUI
{
    public MainWindow()
    {
        TaskDispatcher = TaskDispatcher.Create(this);

        LoadAssociations();
        LoadGUI();
        LoadIcons();
        IsChangedInternal = false;
        PageBeers.SetSelected(true);

        Loaded += OnLoaded;
    }

    private TaskDispatcher TaskDispatcher;
}

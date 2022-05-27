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
    }

    private TaskDispatcher TaskDispatcher;
}

namespace PgBrewer;

using WpfLayout;
using System.Threading.Tasks;
using System.Windows;

/// <summary>
/// Main window implementation.
/// </summary>
public partial class MainWindow : MainWindowUI
{
    public MainWindow()
    {
        TaskDispatcher = TaskDispatcher.Create(this);

        Loaded += async (_, __) =>
        {
            await InitializePages();
            SelectedPageIndex = 0; // safe now
        };
    }

    private TaskDispatcher TaskDispatcher;
}
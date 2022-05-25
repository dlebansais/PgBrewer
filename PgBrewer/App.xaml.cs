namespace PgBrewer;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

/// <summary>
/// Application implementation.
/// </summary>
public partial class App : Application
{
    #region Init
    private void OnStartup(object sender, StartupEventArgs e)
    {
        MainWindow MainWindow = new MainWindow();
        MainWindow.Show();
    }
    #endregion
}

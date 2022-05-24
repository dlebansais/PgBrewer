namespace PgBrewer;

using System.Windows;

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

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

    #region Events
    public void OnGotFocus(object sender, RoutedEventArgs e)
    {
        ((MainWindow)Current.MainWindow).OnGotFocus((ComboBox)sender);
    }

    public void OnLostFocus(object sender, RoutedEventArgs e)
    {
        ((MainWindow)Current.MainWindow).OnLostFocus((ComboBox)sender);
    }

    public void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        FrameworkElement Ctrl = (FrameworkElement)sender;
        if (Tools.FindFirstControl(Ctrl, out ComboBox FirstComboBox))
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, new LineClickedHandler(OnLineClicked), FirstComboBox);
    }

    private delegate void LineClickedHandler(ComboBox firstComboBox);
    private void OnLineClicked(ComboBox firstComboBox)
    {
        firstComboBox.Focus();
    }
    #endregion
}

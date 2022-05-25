namespace PgBrewer;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfLayout;

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
    private void OnListBoxItemMouseMove(object sender, MouseEventArgs args)
    {
        DragDropTools.OnListBoxItemMouseMove(sender, args);
    }

    private void OnListBoxItemDrop(object sender, DragEventArgs args)
    {
        DragDropTools.OnListBoxItemDrop(sender, args);
    }

    private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (sender is ListBox AsListBox && e.OriginalSource == sender && AsListBox.SelectedItem is not null)
        {
            ItemContainerGenerator ItemContainerGenerator = AsListBox.ItemContainerGenerator;
            if (ItemContainerGenerator.ContainerFromItem(AsListBox.SelectedItem) is FrameworkElement AsFrameworkElement)
                AsFrameworkElement.Focus();
        }
    }
    #endregion
}

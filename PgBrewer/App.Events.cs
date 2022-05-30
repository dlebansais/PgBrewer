namespace PgBrewer;

using System.Windows;
using System.Windows.Controls;
using WpfLayout;

/// <summary>
/// Application implementation.
/// </summary>
public partial class App
{
    public void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (sender is ListBox AsListBox && e.OriginalSource == sender && AsListBox.SelectedItem is object SelectedItem)
        {
            AsListBox.ScrollIntoView(SelectedItem);

            if (SelectorTools.ItemToContainer(AsListBox, SelectedItem) is FrameworkElement AsFrameworkElement)
                AsFrameworkElement.Focus();
        }
    }
}

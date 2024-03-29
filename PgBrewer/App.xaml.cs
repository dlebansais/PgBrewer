﻿namespace PgBrewer;

using System.Windows;
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

    #region Drag & Drop
    private void OnListBoxItemMouseMove(object sender, MouseEventArgs args)
    {
        DragDropTools.OnListBoxItemMouseMove(sender, args);
    }

    private void OnListBoxItemDrop(object sender, DragEventArgs args)
    {
        DragDropTools.OnListBoxItemDrop(sender, args);
    }
    #endregion
}

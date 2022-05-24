namespace PgBrewer;

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

/// <summary>
/// Main Window UI.
/// </summary>
public abstract partial class MainWindowUI : Window, INotifyPropertyChanged
{
    #region Init
    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindowUI"/> class.
    /// </summary>
    public MainWindowUI()
    {
        InitializeComponent();
        DataContext = this;
    }
    #endregion

    #region Properties
    public abstract ObservableCollection<PgBrewerPage> PageList { get; }
    public abstract int SelectedPageIndex { get; set; }
    #endregion

    #region Events
    public abstract void OnClosing(object sender, CancelEventArgs e);
    public abstract void OnBack(object sender, RoutedEventArgs e);
    public abstract void OnForward(object sender, RoutedEventArgs e);
    public abstract void OnSave(object sender, RoutedEventArgs e);
    public abstract void OnExport(object sender, RoutedEventArgs e);
    public abstract void OnImport(object sender, RoutedEventArgs e);
    public abstract void OnDelete(object sender, RoutedEventArgs e);
    public abstract void OnDeleteLine(object sender, RoutedEventArgs e);
    #endregion

    #region Implementation of INotifyPropertyChanged
    /// <summary>
    /// Implements the PropertyChanged event.
    /// </summary>
#nullable disable annotations
    public event PropertyChangedEventHandler PropertyChanged;
#nullable restore annotations

    internal void NotifyPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    internal void NotifyThisPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion
}

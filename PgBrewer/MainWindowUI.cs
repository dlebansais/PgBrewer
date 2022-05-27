namespace PgBrewer;

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
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
    public abstract bool IsChanged { get; }
    #endregion

    #region Events
    public abstract void OnMainWindowLoaded(object sender, RoutedEventArgs e);
    public abstract void OnMainWindowClosing(object sender, CancelEventArgs e);
    public abstract void OnDelete(object sender, ExecutedRoutedEventArgs e);
    public abstract void OnSave(object sender, ExecutedRoutedEventArgs e);
    public abstract void OnExport(object sender, ExecutedRoutedEventArgs e);
    public abstract void OnImport(object sender, ExecutedRoutedEventArgs e);
    public abstract void OnBack(object sender, ExecutedRoutedEventArgs e);
    public abstract void OnForward(object sender, ExecutedRoutedEventArgs e);
    public abstract void OnDeleteLine(object sender, ExecutedRoutedEventArgs e);
    #endregion

    #region Storage
    public static async Task<T?> GetItemAsync<T>(string name)
        where T : class, new()
    {
        return await LocalStorage.GetItemAsync<T>(name);
    }

    public static async Task SetItemAsync<T>(string name, T value)
    {
        await LocalStorage.SetItemAsync<T>(name, value);
    }
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

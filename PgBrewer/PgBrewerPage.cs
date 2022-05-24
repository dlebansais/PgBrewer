namespace PgBrewer;

using System.ComponentModel;
using System.Runtime.CompilerServices;

public abstract class PgBrewerPage : INotifyPropertyChanged
{
    #region Init
    public PgBrewerPage(BackForward backForward, bool startSelected)
    {
        BackForward = backForward;
        IsSelected = startSelected;
    }
    #endregion

    #region Properties
    public abstract string Name { get; }
    public abstract int IconId { get; }
    public BackForward BackForward { get; }
    public bool IsSelected { get; private set; }
    #endregion

    #region Client Interface
    public void SetSelected(bool isSelected)
    {
        IsSelected = isSelected;
        NotifyPropertyChanged(nameof(IsSelected));
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

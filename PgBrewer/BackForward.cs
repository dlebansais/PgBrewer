namespace PgBrewer;

using System.ComponentModel;
using System.Runtime.CompilerServices;

public class BackForward
{
    #region Properties
    public bool CanGoBack
    {
        get => CanGoBackInternal;
        set
        {
            if (CanGoBackInternal != value)
            {
                CanGoBackInternal = value;
                NotifyThisPropertyChanged();
            }
        }
    }

    private bool CanGoBackInternal;

    public bool CanGoForward
    {
        get => CanGoForwardInternal;
        set
        {
            if (CanGoForwardInternal != value)
            {
                CanGoForwardInternal = value;
                NotifyThisPropertyChanged();
            }
        }
    }

    private bool CanGoForwardInternal;
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

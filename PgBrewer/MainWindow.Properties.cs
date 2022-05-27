namespace PgBrewer;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;

/// <summary>
/// Main window implementation.
/// </summary>
public partial class MainWindow
{
    public override ObservableCollection<PgBrewerPage> PageList { get; } = new();

    public override int SelectedPageIndex
    {
        get
        {
            return SelectedPageIndexInternal;
        }
        set
        {
            if (SelectedPageIndexInternal != value)
            {
                SelectedPageIndexInternal = value;

                for (int i = 0; i < PageList.Count; i++)
                    PageList[i].SetSelected(i == SelectedPageIndexInternal);
            }
        }
    }

    private int SelectedPageIndexInternal = 0;

    public override bool IsChanged
    {
        get => IsChangedInternal;
    }

    private bool IsChangedInternal;

    private void SetIsChanged(bool value)
    {
        if (!IsPageListInitialized)
            return;

        if (IsChangedInternal != value)
        {
            IsChangedInternal = value;
            NotifyPropertyChanged(nameof(IsChanged));
        }
    }
}

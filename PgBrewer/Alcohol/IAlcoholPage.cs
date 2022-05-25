namespace PgBrewer;

using System.Collections.ObjectModel;

public interface IAlcoholPage
{
    ObservableCollection<Alcohol> AlcoholList { get; }
    int SelectedAlcoholIndex { get; set; }
}

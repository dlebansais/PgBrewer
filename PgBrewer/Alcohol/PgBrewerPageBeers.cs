namespace PgBrewer;

using System.Collections.ObjectModel;

public class PgBrewerPageBeers : PgBrewerPage, IAlcoholPage
{
    #region Init
    public PgBrewerPageBeers(BackForward backForward)
        : base(backForward)
    {
        BasicLager = new Alcoholx4("Basic Lager",
            Component.FruitTier1Four);

        PaleAle = new Alcoholx4x4("Pale Ale",
            Component.FruitTier1Four,
            Component.VeggieTier1);

        Marzen = new Alcoholx4x4x2("Marzen",
            Component.FruitTier1Four,
            Component.VeggieTier1,
            Component.FlavorTier1Two);

        GoblinAle = new Alcoholx3x3x4x3("Goblin Ale",
            Component.FruitTier1Three,
            Component.FruitTier2,
            Component.VeggieTier1,
            Component.FlavorTier1Three);

        OrcishBock = new Alcoholx4x3x4x3("Orcish Bock",
            Component.VeggieTier2,
            Component.FruitTier2,
            Component.MushroomTier3,
            Component.FlavorTier1Three);

        BrownAle = new Alcoholx4x3x4x3("Brown Ale",
            Component.VeggieTier3Beer,
            Component.FruitTier2,
            Component.MushroomTier3,
            Component.FlavorTier2Three);

        HegemonyLager = new Alcoholx4x3x4x3("Hegemony Lager",
            Component.VeggieTier3Beer,
            Component.FruitTier3,
            Component.MushroomTier3,
            Component.FlavorTier2Three);

        DwarvenStout = new Alcoholx4x3x4x3("Dwarven Stout",
            Component.VeggieTier3Beer,
            Component.FruitTier3,
            Component.MushroomTier4,
            Component.FlavorTier3Three);

        AlcoholList = new ObservableCollection<Alcohol>()
        {
            BasicLager,
            PaleAle,
            Marzen,
            GoblinAle,
            OrcishBock,
            BrownAle,
            HegemonyLager,
            DwarvenStout,
        };

        foreach (Alcohol Item in AlcoholList)
            Item.LineSelected += OnLineSelected;

        BasicLager.SetSelected(true);
    }
    #endregion

    #region Properties
    public override string Name { get; } = "Beers";
    public override int IconId { get; } = 5744;

    public Alcoholx4 BasicLager { get; }
    public Alcoholx4x4 PaleAle { get; }
    public Alcoholx4x4x2 Marzen { get; }
    public Alcoholx3x3x4x3 GoblinAle { get; }
    public Alcoholx4x3x4x3 OrcishBock { get; }
    public Alcoholx4x3x4x3 BrownAle { get; }
    public Alcoholx4x3x4x3 HegemonyLager { get; }
    public Alcoholx4x3x4x3 DwarvenStout { get; }
    public ObservableCollection<Alcohol> AlcoholList { get; }

    public int SelectedAlcoholIndex
    {
        get
        {
            return SelectedAlcoholIndexInternal;
        }
        set
        {
            if (SelectedAlcoholIndexInternal != value)
            {
                AlcoholList[SelectedAlcoholIndexInternal].SelectedLine = -1;

                SelectedAlcoholIndexInternal = value;

                for (int i = 0; i < AlcoholList.Count; i++)
                    AlcoholList[i].SetSelected(i == SelectedAlcoholIndexInternal);

                NotifyThisPropertyChanged();
            }
        }
    }

    private int SelectedAlcoholIndexInternal = 0;
    #endregion

    #region Events
    protected void OnLineSelected(Alcohol alcohol, AlcoholLine? alcoholLine)
    {
        BackForward.CanGoBack = alcoholLine is not null && alcohol.Previous != Alcohol.None;
        BackForward.CanGoForward = alcoholLine is not null && alcohol.Next != Alcohol.None;
    }
    #endregion
}

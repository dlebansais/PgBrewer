namespace PgBrewer;

using System.Collections.ObjectModel;

public class PgBrewerPageLiquors : PgBrewerPage, IAlcoholPage
{
    #region Init
    public PgBrewerPageLiquors(BackForward backForward)
        : base(backForward)
    {
        PotatoVodka = new Alcoholx3x4x5x4("Potato Vodka",
            Component.FruitTier1Three,
            Component.VeggieTier1,
            Component.PartsTier1,
            Component.FlavorTier1Four);

        Applejack = new Alcoholx3x4x5x4("Applejack",
            Component.FruitTier1Three,
            Component.VeggieTier2,
            Component.PartsTier1,
            Component.FlavorTier1Four);

        BeetVodka = new Alcoholx3x4x5x4("Beet Vodka",
            Component.FruitTier2,
            Component.VeggieTier1,
            Component.PartsTier1,
            Component.FlavorTier1Four);

        PaleRum = new Alcoholx3x4x5x4("Pale Rum",
            Component.FruitTier2,
            Component.MushroomTier3,
            Component.PartsTier1,
            Component.FlavorTier1Four);

        Whisky = new Alcoholx3x4x5x4("Whisky",
            Component.FruitTier2,
            Component.MushroomTier3,
            Component.PartsTier2,
            Component.FlavorTier1Four);

        Tequila = new Alcoholx3x4x5x4("Tequila",
            Component.FruitTier2,
            Component.MushroomTier3,
            Component.PartsTier2,
            Component.FlavorTier2Four);

        DryGin = new Alcoholx3x4x5x4("Dry Gin",
            Component.FruitTier3,
            Component.MushroomTier4,
            Component.PartsTier2,
            Component.FlavorTier2Four);

        Bourbon = new Alcoholx3x4x5x4("Bourbon",
            Component.FruitTier3,
            Component.MushroomTier4,
            Component.PartsTier3,
            Component.FlavorTier3Four);

        AlcoholList = new ObservableCollection<Alcohol>()
        {
            PotatoVodka,
            Applejack,
            BeetVodka,
            PaleRum,
            Whisky,
            Tequila,
            DryGin,
            Bourbon,
        };

        foreach (Alcohol Item in AlcoholList)
            Item.LineSelected += OnLineSelected;

        PotatoVodka.SetSelected(true);
    }
    #endregion

    #region Properties
    public override string Name { get; } = "Liquors";
    public override int IconId { get; } = 5746;

    public Alcoholx3x4x5x4 PotatoVodka { get; }
    public Alcoholx3x4x5x4 Applejack { get; }
    public Alcoholx3x4x5x4 BeetVodka { get; }
    public Alcoholx3x4x5x4 PaleRum { get; }
    public Alcoholx3x4x5x4 Whisky { get; }
    public Alcoholx3x4x5x4 Tequila { get; }
    public Alcoholx3x4x5x4 DryGin { get; }
    public Alcoholx3x4x5x4 Bourbon { get; }
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

namespace PgBrewer;

using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;

public class PgBrewerPageLiquors : PgBrewerPage, IAlcoholPage
{
    #region Init
    public static async Task<PgBrewerPageLiquors> Create(BackForward backForward)
    {
        PgBrewerPageLiquors Instance = new(backForward);
        await Instance.Init();

        return Instance;
    }

    private PgBrewerPageLiquors(BackForward backForward)
        : base(backForward)
    {
    }

    private async Task Init()
    {
        PotatoVodka = await Alcoholx3x4x5x4.Create("Potato Vodka",
            Component.FruitTier1Three,
            Component.VeggieTier1,
            Component.PartsTier1,
            Component.FlavorTier1Four);

        Applejack = await Alcoholx3x4x5x4.Create("Applejack",
            Component.FruitTier1Three,
            Component.VeggieTier2,
            Component.PartsTier1,
            Component.FlavorTier1Four);

        BeetVodka = await Alcoholx3x4x5x4.Create("Beet Vodka",
            Component.FruitTier2,
            Component.VeggieTier1,
            Component.PartsTier1,
            Component.FlavorTier1Four);

        PaleRum = await Alcoholx3x4x5x4.Create("Pale Rum",
            Component.FruitTier2,
            Component.MushroomTier3,
            Component.PartsTier1,
            Component.FlavorTier1Four);

        Whisky = await Alcoholx3x4x5x4.Create("Whisky",
            Component.FruitTier2,
            Component.MushroomTier3,
            Component.PartsTier2,
            Component.FlavorTier1Four);

        Tequila = await Alcoholx3x4x5x4.Create("Tequila",
            Component.FruitTier2,
            Component.MushroomTier3,
            Component.PartsTier2,
            Component.FlavorTier2Four);

        DryGin = await Alcoholx3x4x5x4.Create("Dry Gin",
            Component.FruitTier3,
            Component.MushroomTier4,
            Component.PartsTier2,
            Component.FlavorTier2Four);

        Bourbon = await Alcoholx3x4x5x4.Create("Bourbon",
            Component.FruitTier3,
            Component.MushroomTier4,
            Component.PartsTier3,
            Component.FlavorTier3Four);

        AlcoholList.Add(PotatoVodka);
        AlcoholList.Add(Applejack);
        AlcoholList.Add(BeetVodka);
        AlcoholList.Add(PaleRum);
        AlcoholList.Add(Whisky);
        AlcoholList.Add(Tequila);
        AlcoholList.Add(DryGin);
        AlcoholList.Add(Bourbon);

        foreach (Alcohol Item in AlcoholList)
            Item.LineSelected += OnLineSelected;

        PotatoVodka.SetSelected(true);
    }
    #endregion

    #region Properties
    public override string Name { get; } = "Liquors";
    public override int IconId { get; } = 5746;

    public Alcoholx3x4x5x4 PotatoVodka { get; private set; } = null!;
    public Alcoholx3x4x5x4 Applejack { get; private set; } = null!;
    public Alcoholx3x4x5x4 BeetVodka { get; private set; } = null!;
    public Alcoholx3x4x5x4 PaleRum { get; private set; } = null!;
    public Alcoholx3x4x5x4 Whisky { get; private set; } = null!;
    public Alcoholx3x4x5x4 Tequila { get; private set; } = null!;
    public Alcoholx3x4x5x4 DryGin { get; private set; } = null!;
    public Alcoholx3x4x5x4 Bourbon { get; private set; } = null!;
    public ObservableCollection<Alcohol> AlcoholList { get; } = new();

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

    #region Client Interface
    public async Task SaveAll()
    {
        await PotatoVodka.Save();
        await Applejack.Save();
        await BeetVodka.Save();
        await PaleRum.Save();
        await Whisky.Save();
        await Tequila.Save();
        await DryGin.Save();
        await Bourbon.Save();
    }

    public void ExportAll(StreamWriter writer)
    {
        PotatoVodka.Export(writer);
        Applejack.Export(writer);
        BeetVodka.Export(writer);
        PaleRum.Export(writer);
        Whisky.Export(writer);
        Tequila.Export(writer);
        DryGin.Export(writer);
        Bourbon.Export(writer);
    }

    public bool ImportAll(StreamReader reader, ref int changeCount)
    {
        if (!PotatoVodka.Import(reader, ref changeCount))
            return false;

        if (!Applejack.Import(reader, ref changeCount))
            return false;

        if (!BeetVodka.Import(reader, ref changeCount))
            return false;

        if (!PaleRum.Import(reader, ref changeCount))
            return false;

        if (!Whisky.Import(reader, ref changeCount))
            return false;

        if (!Tequila.Import(reader, ref changeCount))
            return false;

        if (!DryGin.Import(reader, ref changeCount))
            return false;

        if (!Bourbon.Import(reader, ref changeCount))
            return false;

        return true;
    }
    #endregion
}

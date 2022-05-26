namespace PgBrewer;

using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;

public class PgBrewerPageBeers : PgBrewerPage, IAlcoholPage
{
    #region Init
    public static async Task<PgBrewerPageBeers> Create(BackForward backForward)
    {
        PgBrewerPageBeers Instance = new(backForward);
        await Instance.Init();

        return Instance;
    }

    private PgBrewerPageBeers(BackForward backForward)
        : base(backForward)
    {
    }

    private async Task Init()
    {
        BasicLager = await Alcoholx4.Create("Basic Lager",
            Component.FruitTier1Four);

        PaleAle = await Alcoholx4x4.Create("Pale Ale",
            Component.FruitTier1Four,
            Component.VeggieTier1);

        Marzen = await Alcoholx4x4x2.Create("Marzen",
            Component.FruitTier1Four,
            Component.VeggieTier1,
            Component.FlavorTier1Two);

        GoblinAle = await Alcoholx3x3x4x3.Create("Goblin Ale",
            Component.FruitTier1Three,
            Component.FruitTier2,
            Component.VeggieTier1,
            Component.FlavorTier1Three);

        OrcishBock = await Alcoholx4x3x4x3.Create("Orcish Bock",
            Component.VeggieTier2,
            Component.FruitTier2,
            Component.MushroomTier3,
            Component.FlavorTier1Three);

        BrownAle = await Alcoholx4x3x4x3.Create("Brown Ale",
            Component.VeggieTier3Beer,
            Component.FruitTier2,
            Component.MushroomTier3,
            Component.FlavorTier2Three);

        HegemonyLager = await Alcoholx4x3x4x3.Create("Hegemony Lager",
            Component.VeggieTier3Beer,
            Component.FruitTier3,
            Component.MushroomTier3,
            Component.FlavorTier2Three);

        DwarvenStout = await Alcoholx4x3x4x3.Create("Dwarven Stout",
            Component.VeggieTier3Beer,
            Component.FruitTier3,
            Component.MushroomTier4,
            Component.FlavorTier3Three);

        AlcoholList.Add(BasicLager);
        AlcoholList.Add(PaleAle);
        AlcoholList.Add(Marzen);
        AlcoholList.Add(GoblinAle);
        AlcoholList.Add(OrcishBock);
        AlcoholList.Add(BrownAle);
        AlcoholList.Add(HegemonyLager);
        AlcoholList.Add(DwarvenStout);

        foreach (Alcohol Item in AlcoholList)
            Item.LineSelected += OnLineSelected;

        BasicLager.SetSelected(true);
    }
    #endregion

    #region Properties
    public override string Name { get; } = "Beers";
    public override int IconId { get; } = 5744;

    public Alcoholx4 BasicLager { get; private set; } = null!;
    public Alcoholx4x4 PaleAle { get; private set; } = null!;
    public Alcoholx4x4x2 Marzen { get; private set; } = null!;
    public Alcoholx3x3x4x3 GoblinAle { get; private set; } = null!;
    public Alcoholx4x3x4x3 OrcishBock { get; private set; } = null!;
    public Alcoholx4x3x4x3 BrownAle { get; private set; } = null!;
    public Alcoholx4x3x4x3 HegemonyLager { get; private set; } = null!;
    public Alcoholx4x3x4x3 DwarvenStout { get; private set; } = null!;
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
        await BasicLager.Save();
        await PaleAle.Save();
        await Marzen.Save();
        await GoblinAle.Save();
        await OrcishBock.Save();
        await BrownAle.Save();
        await HegemonyLager.Save();
        await DwarvenStout.Save();
    }

    public void ExportAll(StreamWriter writer)
    {
        BasicLager.Export(writer);
        PaleAle.Export(writer);
        Marzen.Export(writer);
        GoblinAle.Export(writer);
        OrcishBock.Export(writer);
        BrownAle.Export(writer);
        HegemonyLager.Export(writer);
        DwarvenStout.Export(writer);
    }

    public bool ImportAll(StreamReader reader, ref int changeCount)
    {
        if (!BasicLager.Import(reader, ref changeCount))
            return false;

        if (!PaleAle.Import(reader, ref changeCount))
            return false;

        if (!Marzen.Import(reader, ref changeCount))
            return false;

        if (!GoblinAle.Import(reader, ref changeCount))
            return false;

        if (!OrcishBock.Import(reader, ref changeCount))
            return false;

        if (!BrownAle.Import(reader, ref changeCount))
            return false;

        if (!HegemonyLager.Import(reader, ref changeCount))
            return false;

        if (!DwarvenStout.Import(reader, ref changeCount))
            return false;

        return true;
    }
    #endregion
}

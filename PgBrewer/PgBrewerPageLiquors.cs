namespace PgBrewer;

public class PgBrewerPageLiquors : PgBrewerPage
{
    #region Init
    public PgBrewerPageLiquors(BackForward backForward, bool startSelected = false)
        : base(backForward, startSelected)
    {
    }
    #endregion

    #region Properties
    public override string Name { get; } = "Liquors";
    public override int IconId { get; } = 5746;

    public Alcoholx3x4x5x4 PotatoVodka { get; private set; } = new Alcoholx3x4x5x4("Potato Vodka",
        Component.FruitTier1Three,
        Component.VeggieTier1,
        Component.PartsTier1,
        Component.FlavorTier1Four);

    public Alcoholx3x4x5x4 Applejack { get; private set; } = new Alcoholx3x4x5x4("Applejack",
        Component.FruitTier1Three,
        Component.VeggieTier2,
        Component.PartsTier1,
        Component.FlavorTier1Four);

    public Alcoholx3x4x5x4 BeetVodka { get; private set; } = new Alcoholx3x4x5x4("Beet Vodka",
        Component.FruitTier2,
        Component.VeggieTier1,
        Component.PartsTier1,
        Component.FlavorTier1Four);

    public Alcoholx3x4x5x4 PaleRum { get; private set; } = new Alcoholx3x4x5x4("Pale Rum",
        Component.FruitTier2,
        Component.MushroomTier3,
        Component.PartsTier1,
        Component.FlavorTier1Four);

    public Alcoholx3x4x5x4 Whisky { get; private set; } = new Alcoholx3x4x5x4("Whisky",
        Component.FruitTier2,
        Component.MushroomTier3,
        Component.PartsTier2,
        Component.FlavorTier1Four);

    public Alcoholx3x4x5x4 Tequila { get; private set; } = new Alcoholx3x4x5x4("Tequila",
        Component.FruitTier2,
        Component.MushroomTier3,
        Component.PartsTier2,
        Component.FlavorTier2Four);

    public Alcoholx3x4x5x4 DryGin { get; private set; } = new Alcoholx3x4x5x4("Dry Gin",
        Component.FruitTier3,
        Component.MushroomTier4,
        Component.PartsTier2,
        Component.FlavorTier2Four);

    public Alcoholx3x4x5x4 Bourbon { get; private set; } = new Alcoholx3x4x5x4("Bourbon",
        Component.FruitTier3,
        Component.MushroomTier4,
        Component.PartsTier3,
        Component.FlavorTier3Four);

    #endregion
}

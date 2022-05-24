namespace PgBrewer;

using System.Collections.Generic;

public class PgBrewerPageBeers : PgBrewerPage
{
    #region Init
    public PgBrewerPageBeers(BackForward backForward, bool startSelected = false)
        : base(backForward, startSelected)
    {
    }
    #endregion

    #region Properties
    public override string Name { get; } = "Beers";
    public override int IconId { get; } = 5744;

    public Alcoholx4 BasicLager { get; private set; } = new Alcoholx4("Basic Lager",
        Component.FruitTier1Four);

    public Alcoholx4x4 PaleAle { get; private set; } = new Alcoholx4x4("Pale Ale",
        Component.FruitTier1Four,
        Component.VeggieTier1);

    public Alcoholx4x4x2 Marzen { get; private set; } = new Alcoholx4x4x2("Marzen",
        Component.FruitTier1Four,
        Component.VeggieTier1,
        Component.FlavorTier1Two);

    public Alcoholx3x3x4x3 GoblinAle { get; private set; } = new Alcoholx3x3x4x3("Goblin Ale",
        Component.FruitTier1Three,
        Component.FruitTier2,
        Component.VeggieTier1,
        Component.FlavorTier1Three);

    public Alcoholx4x3x4x3 OrcishBock { get; private set; } = new Alcoholx4x3x4x3("Orcish Bock",
        Component.VeggieTier2,
        Component.FruitTier2,
        Component.MushroomTier3,
        Component.FlavorTier1Three);

    public Alcoholx4x3x4x3 BrownAle { get; private set; } = new Alcoholx4x3x4x3("Brown Ale",
        Component.VeggieTier3Beer,
        Component.FruitTier2,
        Component.MushroomTier3,
        Component.FlavorTier2Three);

    public Alcoholx4x3x4x3 HegemonyLager { get; private set; } = new Alcoholx4x3x4x3("Hegemony Lager",
        Component.VeggieTier3Beer,
        Component.FruitTier3,
        Component.MushroomTier3,
        Component.FlavorTier2Three);

    public Alcoholx4x3x4x3 DwarvenStout { get; private set; } = new Alcoholx4x3x4x3("Dwarven Stout",
        Component.VeggieTier3Beer,
        Component.FruitTier3,
        Component.MushroomTier4,
        Component.FlavorTier3Three);
    #endregion
}

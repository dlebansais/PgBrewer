namespace PgBrewer;

using System.Collections.Generic;

public class Component
{
    public static readonly Component RedApple = new Component("Red Apple");
    public static readonly Component Grapes = new Component("Grapes");
    public static readonly Component Orange = new Component("Orange");
    public static readonly Component Guava = new Component("Guava");
    public static readonly Component Banana = new Component("Banana");
    public static readonly Component Lemon = new Component("Lemon");
    public static readonly Component Pear = new Component("Pear");
    public static readonly Component Peach = new Component("Peach");
    public static readonly Component GreenApple = new Component("Green Apple");
    public static readonly Component ParasolMushroomFlakes = new Component("Parasol Mushroom Flakes");
    public static readonly Component MycenaMushroomFlakes = new Component("Mycena Mushroom Flakes");
    public static readonly Component BoletusMushroomFlakes = new Component("Boletus Mushroom Flakes");
    public static readonly Component FieldMushroomFlakes = new Component("Field Mushroom Flakes");
    public static readonly Component Beet = new Component("Beet");
    public static readonly Component Squash = new Component("Squash");
    public static readonly Component Broccoli = new Component("Broccoli");
    public static readonly Component Carrot = new Component("Carrot");
    public static readonly Component BlusherMushroomFlakes = new Component("Blusher Mushroom Flakes");
    public static readonly Component MilkCapMushroomPowder = new Component("Milk Cap Mushroom Powder");
    public static readonly Component BloodMushroomPowder = new Component("Blood Mushroom Powder");
    public static readonly Component CoralMushroomPowder = new Component("Coral Mushroom Powder");
    public static readonly Component GroxmaxPowder = new Component("Groxmax Powder");
    public static readonly Component PorciniMushroomFlakes = new Component("Porcini Mushroom Flakes");
    public static readonly Component BlackFootMorelFlakes = new Component("Black Foot Morel Flakes");
    public static readonly Component BoarTusk = new Component("Boar Tusk");
    public static readonly Component CatEyeball = new Component("Cat Eyeball");
    public static readonly Component SnailSinew = new Component("Snail Sinew");
    public static readonly Component RatTail = new Component("Rat Tail");
    public static readonly Component BasicFishScale = new Component("Basic Fish Scale");
    public static readonly Component WolfTeeth = new Component("Wolf Teeth");
    public static readonly Component PantherTail = new Component("Panther Tail");
    public static readonly Component DeinonychusClaw = new Component("Deinonychus Claw");
    public static readonly Component RabbitsFoot = new Component("Rabbit's Foot");
    public static readonly Component BearGallbladder = new Component("Bear Gallbladder");
    public static readonly Component CockatriceBeak = new Component("Cockatrice Beak");
    public static readonly Component WormTooth = new Component("Worm Tooth");
    public static readonly Component Ectoplasm = new Component("Ectoplasm");
    public static readonly Component PowderedMammal = new Component("Powdered Mammal");
    public static readonly Component BarghestFlesh = new Component("Barghest Flesh");
    public static readonly Component Oregano = new Component("Oregano");
    public static readonly Component MandrakeRoot = new Component("Mandrake Root");
    public static readonly Component Peppercorns = new Component("Peppercorns");
    public static readonly Component Grass = new Component("Grass");
    public static readonly Component Cinnamon = new Component("Cinnamon");
    public static readonly Component MuntokPeppercorns = new Component("Muntok Peppercorns");
    public static readonly Component Seaweed = new Component("Seaweed");
    public static readonly Component MyconianJelly = new Component("Myconian Jelly");
    public static readonly Component Mint = new Component("Mint");
    public static readonly Component Honey = new Component("Honey");
    public static readonly Component JuniperBerries = new Component("Juniper Berries");
    public static readonly Component Almonds = new Component("Almonds");
    public static readonly Component LargeStrawberry = new Component("Large Strawberry");
    public static readonly Component GreenPepper = new Component("Green Pepper");
    public static readonly Component RedPepper = new Component("Red Pepper");
    public static readonly Component Molasses = new Component("Molasses");
    public static readonly Component Corn = new Component("Corn");

    public static readonly List<Component> FruitTier1Three = new List<Component>() { RedApple, Grapes, Orange };
    public static readonly List<Component> FruitTier1Four = new List<Component>() { RedApple, Grapes, Orange, LargeStrawberry };
    public static readonly List<Component> FruitTier2 = new List<Component>() { Guava, Banana, Lemon };
    public static readonly List<Component> FruitTier3 = new List<Component>() { Pear, Peach, GreenApple };
    public static readonly List<Component> VeggieTier1 = new List<Component>() { ParasolMushroomFlakes, MycenaMushroomFlakes, BoletusMushroomFlakes, FieldMushroomFlakes };
    public static readonly List<Component> VeggieTier2 = new List<Component>() { Beet, Squash, Broccoli, Carrot };
    public static readonly List<Component> VeggieTier3Beer = new List<Component>() { GreenPepper, RedPepper, Molasses, Corn };
    public static readonly List<Component> MushroomTier3 = new List<Component>() { FieldMushroomFlakes, BlusherMushroomFlakes, MilkCapMushroomPowder, BloodMushroomPowder };
    public static readonly List<Component> MushroomTier4 = new List<Component>() { CoralMushroomPowder, GroxmaxPowder, PorciniMushroomFlakes, BlackFootMorelFlakes };
    public static readonly List<Component> PartsTier1 = new List<Component>() { BoarTusk, CatEyeball, SnailSinew, RatTail, BasicFishScale };
    public static readonly List<Component> PartsTier2 = new List<Component>() { WolfTeeth, PantherTail, DeinonychusClaw, RabbitsFoot, BearGallbladder };
    public static readonly List<Component> PartsTier3 = new List<Component>() { CockatriceBeak, WormTooth, Ectoplasm, PowderedMammal, BarghestFlesh };
    public static readonly List<Component> FlavorTier1Two = new List<Component>() { Oregano, MandrakeRoot };
    public static readonly List<Component> FlavorTier1Three = new List<Component>() { Oregano, MandrakeRoot, Peppercorns };
    public static readonly List<Component> FlavorTier1Four = new List<Component>() { Oregano, MandrakeRoot, Peppercorns, Grass };
    public static readonly List<Component> FlavorTier2Three = new List<Component>() { Cinnamon, MuntokPeppercorns, Seaweed };
    public static readonly List<Component> FlavorTier2Four = new List<Component>() { Cinnamon, MuntokPeppercorns, Seaweed, MyconianJelly };
    public static readonly List<Component> FlavorTier3Three = new List<Component>() { Mint, Honey, JuniperBerries };
    public static readonly List<Component> FlavorTier3Four = new List<Component>() { Mint, Honey, JuniperBerries, Almonds };

    #region Init
    public Component(string name)
    {
        Name = name;
    }
    #endregion

    #region Properties
    public string Name { get; }
    #endregion

    #region Debugging
    public override string ToString()
    {
        return Name;
    }
    #endregion
}

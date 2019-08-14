namespace PgBrew
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly string AssociationSettingName = "Associations";
        private static readonly string GuiSettingName = "GUI";

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            LoadAssociations();
            LoadGUI();
            IsChanged = false;
        }

        private void LoadAssociations()
        {
            List<int> AssociationIndexes = DataArchive.GetIndexList(AssociationSettingName, AssociationList.Count);
            for (int i = 0; i < AssociationList.Count; i++)
                AssociationList[i].AssociationIndex = AssociationIndexes[i];
        }

        private void LoadGUI()
        {
            List<int> Coordinates = DataArchive.GetIndexList(GuiSettingName, 4);

            if (Coordinates[0] >= 0)
                Left = Coordinates[0];

            if (Coordinates[1] >= 0)
                Top = Coordinates[1];

            if (Coordinates[2] >= 0)
                Width = Coordinates[2];

            if (Coordinates[3] >= 0)
                Height = Coordinates[3];

            if (Coordinates[2] >= 0 && Coordinates[3] >= 0)
                SizeToContent = SizeToContent.Manual;
        }

        public static bool IsChanged { get; set; }

        public static Component RedApple { get; } = new Component("Red Apple");
        public static Component Grapes { get; } = new Component("Grapes");
        public static Component Orange { get; } = new Component("Orange");
        public static Component Guava { get; } = new Component("Guava");
        public static Component Banana { get; } = new Component("Banana");
        public static Component Lemon { get; } = new Component("Lemon");
        public static Component Pear { get; } = new Component("Pear");
        public static Component Peach { get; } = new Component("Peach");
        public static Component GreenApple { get; } = new Component("GreenApple");

        public static Component ParasolMushroomFlakes { get; } = new Component("Parasol Mushroom Flakes");
        public static Component MycenaMushroomFlakes { get; } = new Component("Mycena Mushroom Flakes");
        public static Component BoletusMushroomFlakes { get; } = new Component("Boletus Mushroom Flakes");
        public static Component FieldMushroomFlakes { get; } = new Component("Field Mushroom Flakes");
        public static Component Beet { get; } = new Component("Beet");
        public static Component Squash { get; } = new Component("Squash");
        public static Component Broccoli { get; } = new Component("Broccoli");
        public static Component Carrot { get; } = new Component("Carrot");
        public static Component BlusherMushroomFlakes { get; } = new Component("Blusher Mushroom Flakes");
        public static Component MilkCapMushroomPowder { get; } = new Component("Milk Cap Mushroom Powder");
        public static Component BloodMushroomPowder { get; } = new Component("Blood Mushroom Powder");
        public static Component CoralMushroomPowder { get; } = new Component("Coral Mushroom Powder");
        public static Component GroxmaxPowder { get; } = new Component("Groxmax Powder");
        public static Component PorciniMushroomFlakes { get; } = new Component("Porcini Mushroom Flakes");
        public static Component BlackFootMorelFlakes { get; } = new Component("Black Foot Morel Flakes");

        public static Component BoarTusk { get; } = new Component("Boar Tusk");
        public static Component CatEyeball { get; } = new Component("Cat Eyeball");
        public static Component SnailSinew { get; } = new Component("Snail Sinew");
        public static Component RatTail { get; } = new Component("Rat Tail");
        public static Component BasicFishScale { get; } = new Component("Basic Fish Scale");
        public static Component WolfTeeth { get; } = new Component("Wolf Teeth");
        public static Component PantherTail { get; } = new Component("Panther Tail");
        public static Component DeinonychusClaw { get; } = new Component("Deinonychus Claw");
        public static Component RabbitsFoot { get; } = new Component("Rabbit's Foot");
        public static Component BearGallbladder { get; } = new Component("Bear Gallbladder");
        public static Component CockatriceBeak { get; } = new Component("Cockatrice Beak");
        public static Component WormTooth { get; } = new Component("Worm Tooth");
        public static Component Ectoplasm { get; } = new Component("Ectoplasm");
        public static Component PowderedMammal { get; } = new Component("Powdered Mammal");
        public static Component BarghestFlesh { get; } = new Component("Barghest Flesh");

        public static Component Oregano { get; } = new Component("Oregano");
        public static Component MandrakeRoot { get; } = new Component("Mandrake Root");
        public static Component Peppercorns { get; } = new Component("Peppercorns");
        public static Component Grass { get; } = new Component("Grass");
        public static Component Cinnamon { get; } = new Component("Cinnamon");
        public static Component MuntokPeppercorns { get; } = new Component("Muntok Peppercorns");
        public static Component Seaweed { get; } = new Component("Seaweed");
        public static Component MyconianJelly { get; } = new Component("Myconian Jelly");
        public static Component Mint { get; } = new Component("Mint");
        public static Component Honey { get; } = new Component("Honey");
        public static Component JuniperBerries { get; } = new Component("Juniper Berries");
        public static Component Almonds { get; } = new Component("Almonds");

        public static Component Strawberry { get; } = new Component("Strawberry");
        public static Component GreenPepper { get; } = new Component("Green Pepper");
        public static Component RedPepper { get; } = new Component("Red Pepper");
        public static Component Molasses { get; } = new Component("Molasses");
        public static Component Corn { get; } = new Component("Corn");

        public Alcoholx4 BasicLager { get; private set; } = new Alcoholx4("Basic Lager", 
            new List<Component>()
            {
                RedApple,
                Grapes,
                Orange,
                Strawberry,
            });

        public Alcoholx4x4 PaleAle { get; private set; } = new Alcoholx4x4("Pale Ale",
            new List<Component>()
            {
                RedApple,
                Grapes,
                Orange,
                Strawberry,
            },
            new List<Component>()
            {
                ParasolMushroomFlakes,
                MycenaMushroomFlakes,
                BoletusMushroomFlakes,
                FieldMushroomFlakes,
            });

        public Alcoholx4x4x2 Marzen { get; private set; } = new Alcoholx4x4x2("Marzen",
            new List<Component>()
            {
                RedApple,
                Grapes,
                Orange,
                Strawberry,
            },
            new List<Component>()
            {
                ParasolMushroomFlakes,
                MycenaMushroomFlakes,
                BoletusMushroomFlakes,
                FieldMushroomFlakes,
            },
            new List<Component>()
            {
                Oregano,
                MandrakeRoot,
            });

        public Alcoholx3x3x4x3 GoblinAle { get; private set; } = new Alcoholx3x3x4x3("Goblin Ale",
            new List<Component>()
            {
                RedApple,
                Grapes,
                Orange,
            },
            new List<Component>()
            {
                Guava,
                Banana,
                Lemon,
            },
            new List<Component>()
            {
                ParasolMushroomFlakes,
                MycenaMushroomFlakes,
                BoletusMushroomFlakes,
                FieldMushroomFlakes,
            },
            new List<Component>()
            {
                Oregano,
                MandrakeRoot,
                Peppercorns,
            });

        public Alcoholx4x3x4x3 OrcishBock { get; private set; } = new Alcoholx4x3x4x3("Orcish Bock",
            new List<Component>()
            {
                Beet,
                Squash,
                Broccoli,
                Carrot,
            },
            new List<Component>()
            {
                Guava,
                Banana,
                Lemon,
            },
            new List<Component>()
            {
                FieldMushroomFlakes,
                BlusherMushroomFlakes,
                MilkCapMushroomPowder,
                BloodMushroomPowder,
            },
            new List<Component>()
            {
                Oregano,
                MandrakeRoot,
                Peppercorns,
            });

        public Alcoholx4x3x4x3 BrownAle { get; private set; } = new Alcoholx4x3x4x3("Brown Ale",
            new List<Component>()
            {
                GreenPepper,
                RedPepper,
                Molasses,
                Corn,
            },
            new List<Component>()
            {
                Guava,
                Banana,
                Lemon,
            },
            new List<Component>()
            {
                FieldMushroomFlakes,
                BlusherMushroomFlakes,
                MilkCapMushroomPowder,
                BloodMushroomPowder,
            },
            new List<Component>()
            {
                Cinnamon,
                MuntokPeppercorns,
                Seaweed,
            });

        public Alcoholx4x3x4x3 HegemonyLager { get; private set; } = new Alcoholx4x3x4x3("Hegemony Lager",
            new List<Component>()
            {
                GreenPepper,
                RedPepper,
                Molasses,
                Corn,
            },
            new List<Component>()
            {
                Pear,
                Peach,
                GreenApple,
            },
            new List<Component>()
            {
                FieldMushroomFlakes,
                BlusherMushroomFlakes,
                MilkCapMushroomPowder,
                BloodMushroomPowder,
            },
            new List<Component>()
            {
                Cinnamon,
                MuntokPeppercorns,
                Seaweed,
            });

        public Alcoholx4x3x4x3 DwarvenStout { get; private set; } = new Alcoholx4x3x4x3("Dwarven Stout",
            new List<Component>()
            {
                GreenPepper,
                RedPepper,
                Molasses,
                Corn,
            },
            new List<Component>()
            {
                Pear,
                Peach,
                GreenApple,
            },
            new List<Component>()
            {
                CoralMushroomPowder,
                GroxmaxPowder,
                PorciniMushroomFlakes,
                BlackFootMorelFlakes,
            },
            new List<Component>()
            {
                Mint,
                Honey,
                JuniperBerries,
            });

        private static List<Component> FruitTier1 = new List<Component>() { RedApple, Grapes, Orange };
        private static List<Component> FruitTier2 = new List<Component>() { Guava, Banana, Lemon };
        private static List<Component> FruitTier3 = new List<Component>() { Pear, Peach, GreenApple };
        private static List<Component> VeggieTier1 = new List<Component>() { ParasolMushroomFlakes, MycenaMushroomFlakes, BoletusMushroomFlakes, FieldMushroomFlakes };
        private static List<Component> VeggieTier2 = new List<Component>() { Beet, Squash,Broccoli, Carrot };
        private static List<Component> VeggieTier3Beer = new List<Component>() { GreenPepper, RedPepper, Molasses, Corn };
        private static List<Component> VeggieTier3Liquor = new List<Component>() { FieldMushroomFlakes, BlusherMushroomFlakes, MilkCapMushroomPowder, BloodMushroomPowder };
        private static List<Component> VeggieTier4 = new List<Component>() { CoralMushroomPowder, GroxmaxPowder, PorciniMushroomFlakes, BlackFootMorelFlakes };
        private static List<Component> PartsTier1 = new List<Component>() { BoarTusk, CatEyeball, SnailSinew, RatTail, BasicFishScale };
        private static List<Component> PartsTier2 = new List<Component>() { WolfTeeth, PantherTail, DeinonychusClaw, RabbitsFoot, BearGallbladder };
        private static List<Component> PartsTier3 = new List<Component>() { CockatriceBeak, WormTooth, Ectoplasm, PowderedMammal, BarghestFlesh };
        private static List<Component> FlavorTier1 = new List<Component>() { Oregano, MandrakeRoot, Peppercorns, Grass };
        private static List<Component> FlavorTier2 = new List<Component>() { Cinnamon, MuntokPeppercorns, Seaweed, MyconianJelly };
        private static List<Component> FlavorTier3 = new List<Component>() { Mint, Honey, JuniperBerries, Almonds };

        public List<ComponentAssociation> AssociationList { get; } = new List<ComponentAssociation>()
        {
            new ComponentAssociation(RedApple, FruitTier2),
            new ComponentAssociation(Grapes, FruitTier2),
            new ComponentAssociation(Orange, FruitTier2),
            new ComponentAssociation(Guava, FruitTier3),
            new ComponentAssociation(Banana, FruitTier3),
            new ComponentAssociation(Lemon, FruitTier3),
            new ComponentAssociation(ParasolMushroomFlakes, VeggieTier2),
            new ComponentAssociation(MycenaMushroomFlakes, VeggieTier2),
            new ComponentAssociation(BoletusMushroomFlakes, VeggieTier2),
            new ComponentAssociation(FieldMushroomFlakes, VeggieTier2),
            new ComponentAssociation(Beet, VeggieTier3Beer),
            new ComponentAssociation(Squash, VeggieTier3Beer),
            new ComponentAssociation(Broccoli, VeggieTier3Beer),
            new ComponentAssociation(Carrot, VeggieTier3Beer),
            new ComponentAssociation(Beet, VeggieTier3Liquor),
            new ComponentAssociation(Squash, VeggieTier3Liquor),
            new ComponentAssociation(Broccoli, VeggieTier3Liquor),
            new ComponentAssociation(Carrot, VeggieTier3Liquor),
            new ComponentAssociation(FieldMushroomFlakes, VeggieTier4),
            new ComponentAssociation(BlusherMushroomFlakes, VeggieTier4),
            new ComponentAssociation(MilkCapMushroomPowder, VeggieTier4),
            new ComponentAssociation(BloodMushroomPowder, VeggieTier4),
            new ComponentAssociation(BoarTusk, PartsTier2),
            new ComponentAssociation(CatEyeball, PartsTier2),
            new ComponentAssociation(SnailSinew, PartsTier2),
            new ComponentAssociation(RatTail, PartsTier2),
            new ComponentAssociation(BasicFishScale, PartsTier2),
            new ComponentAssociation(WolfTeeth, PartsTier3),
            new ComponentAssociation(PantherTail, PartsTier3),
            new ComponentAssociation(DeinonychusClaw, PartsTier3),
            new ComponentAssociation(RabbitsFoot, PartsTier3),
            new ComponentAssociation(BearGallbladder, PartsTier3),
            new ComponentAssociation(Oregano, FlavorTier2),
            new ComponentAssociation(MandrakeRoot, FlavorTier2),
            new ComponentAssociation(Peppercorns, FlavorTier2),
            new ComponentAssociation(Grass, FlavorTier2),
            new ComponentAssociation(Cinnamon, FlavorTier3),
            new ComponentAssociation(MuntokPeppercorns, FlavorTier3),
            new ComponentAssociation(Seaweed, FlavorTier3),
            new ComponentAssociation(MyconianJelly, FlavorTier3),
        };

        private void OnClosing(object sender, CancelEventArgs e)
        {
            if (IsChanged)
            {
                MessageBoxResult Answer = MessageBox.Show("Save changes before exit?", "Closing", MessageBoxButton.YesNoCancel);

                switch (Answer)
                {
                    case MessageBoxResult.Yes:
                        SaveAll();
                        break;

                    case MessageBoxResult.No:
                        break;

                    default:
                    case MessageBoxResult.Cancel:
                        e.Cancel = true;
                        break;
                }
            }

            if (!e.Cancel)
                SaveGUI();
        }

        private void SaveAll()
        {
            BasicLager.Save();
            PaleAle.Save();
            Marzen.Save();
            GoblinAle.Save();
            OrcishBock.Save();
            BrownAle.Save();
            HegemonyLager.Save();
            DwarvenStout.Save();

            SaveAssociations();
            SaveGUI();
        }

        private void SaveAssociations()
        {
            List<int> IndexList = new List<int>();
            for (int i = 0; i < AssociationList.Count; i++)
                IndexList.Add(AssociationList[i].AssociationIndex);

            DataArchive.SetIndexList(AssociationSettingName, IndexList);
        }

        private void SaveGUI()
        {
            if (WindowState != WindowState.Normal)
                return;

            List<int> Coordinates = new List<int>();
            Coordinates.Add((int)Left);
            Coordinates.Add((int)Top);
            Coordinates.Add((int)ActualWidth);
            Coordinates.Add((int)ActualHeight);

            DataArchive.SetIndexList(GuiSettingName, Coordinates);
        }

        public void Recalculate()
        {
            Recalculate(OrcishBock, BrownAle);
            Recalculate(BrownAle, HegemonyLager);
            Recalculate(HegemonyLager, DwarvenStout);
        }

        public void Recalculate(Alcoholx4x3x4x3 previous, Alcoholx4x3x4x3 next)
        {

        }
    }
}

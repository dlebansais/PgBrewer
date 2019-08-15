namespace PgBrew
{
    using Microsoft.Win32;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        #region Constants
        private static readonly string AssociationSettingName = "Association";
        private static readonly string GuiSettingName = "GUI";
        #endregion

        #region Init
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            LoadAssociations();
            LoadGUI();
            LoadIcons();
            _IsChanged = false;

            Loaded += OnLoaded;
        }

        private void LoadAssociations()
        {
            LoadAssociations(AssociationFruit1);
            LoadAssociations(AssociationFruit2);
            LoadAssociations(AssociationVeggie1);
            LoadAssociations(AssociationVeggie2Beer);
            LoadAssociations(AssociationVeggie2Liquor);
            LoadAssociations(AssociationMushroom3);
            LoadAssociations(AssociationParts1);
            LoadAssociations(AssociationParts2);
            LoadAssociations(AssociationFlavor1Beer);
            LoadAssociations(AssociationFlavor1Liquor);
            LoadAssociations(AssociationFlavor2Beer);
            LoadAssociations(AssociationFlavor2Liquor);
        }

        private void LoadAssociations(ComponentAssociationCollection associationList)
        {
            List<int> AssociationIndexes = DataArchive.GetIndexList($"{AssociationSettingName}{associationList.Name}", associationList.Count);
            for (int i = 0; i < associationList.Count; i++)
                associationList[i].AssociationIndex = AssociationIndexes[i];

            AssociationTable.Add(associationList);
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

        private void LoadIcons()
        {
            try
            {
                string UserRootFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string ApplicationFolder = Path.Combine(UserRootFolder, "PgJsonParse");
                string VersionCacheFolder = Path.Combine(ApplicationFolder, "Versions");

                if (!Directory.Exists(VersionCacheFolder))
                    return;

                string FinalFolder = null;
                string SharedFolder = Path.Combine(ApplicationFolder, "Shared Icons");

                if (Directory.Exists(SharedFolder))
                    FinalFolder = SharedFolder;
                else
                {
                    string[] VersionFolders = Directory.GetDirectories(VersionCacheFolder);
                    int LastVersion = -1;

                    foreach (string Folder in VersionFolders)
                    {
                        if (int.TryParse(Path.GetFileName(Folder), out int FolderVersion))
                            if (LastVersion < FolderVersion)
                                LastVersion = FolderVersion;
                    }

                    if (LastVersion > 0)
                        FinalFolder = Path.Combine(VersionCacheFolder, LastVersion.ToString());
                }

                if (FinalFolder != null)
                {
                    IconBeer = new BitmapImage(new Uri(Path.Combine(FinalFolder, "icon_5744.png")));
                    IconLiquor = new BitmapImage(new Uri(Path.Combine(FinalFolder, "icon_5746.png")));
                    IconSettings = new BitmapImage(new Uri(Path.Combine(FinalFolder, "icon_5476.png")));
                }
            }
            catch
            {
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            Recalculate();
        }
        #endregion

        #region Properties
        public ImageSource IconBeer { get; private set; }
        public ImageSource IconLiquor { get; private set; }
        public ImageSource IconSettings { get; private set; }

        public bool IsChanged
        {
            get { return _IsChanged; }
            private set
            {
                if (_IsChanged != value)
                {
                    _IsChanged = value;
                    NotifyThisPropertyChanged();
                }
            }
        }
        private bool _IsChanged;
        #endregion

        #region Alcohols
        public static Component RedApple { get; } = new Component("Red Apple");
        public static Component Grapes { get; } = new Component("Grapes");
        public static Component Orange { get; } = new Component("Orange");
        public static Component Guava { get; } = new Component("Guava");
        public static Component Banana { get; } = new Component("Banana");
        public static Component Lemon { get; } = new Component("Lemon");
        public static Component Pear { get; } = new Component("Pear");
        public static Component Peach { get; } = new Component("Peach");
        public static Component GreenApple { get; } = new Component("Green Apple");
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

        private static List<Component> FruitTier1Three = new List<Component>() { RedApple, Grapes, Orange };
        private static List<Component> FruitTier1Four = new List<Component>() { RedApple, Grapes, Orange, Strawberry };
        private static List<Component> FruitTier2 = new List<Component>() { Guava, Banana, Lemon };
        private static List<Component> FruitTier3 = new List<Component>() { Pear, Peach, GreenApple };
        private static List<Component> VeggieTier1 = new List<Component>() { ParasolMushroomFlakes, MycenaMushroomFlakes, BoletusMushroomFlakes, FieldMushroomFlakes };
        private static List<Component> VeggieTier2 = new List<Component>() { Beet, Squash, Broccoli, Carrot };
        private static List<Component> VeggieTier3Beer = new List<Component>() { GreenPepper, RedPepper, Molasses, Corn };
        private static List<Component> MushroomTier3 = new List<Component>() { FieldMushroomFlakes, BlusherMushroomFlakes, MilkCapMushroomPowder, BloodMushroomPowder };
        private static List<Component> MushroomTier4 = new List<Component>() { CoralMushroomPowder, GroxmaxPowder, PorciniMushroomFlakes, BlackFootMorelFlakes };
        private static List<Component> PartsTier1 = new List<Component>() { BoarTusk, CatEyeball, SnailSinew, RatTail, BasicFishScale };
        private static List<Component> PartsTier2 = new List<Component>() { WolfTeeth, PantherTail, DeinonychusClaw, RabbitsFoot, BearGallbladder };
        private static List<Component> PartsTier3 = new List<Component>() { CockatriceBeak, WormTooth, Ectoplasm, PowderedMammal, BarghestFlesh };
        private static List<Component> FlavorTier1Two = new List<Component>() { Oregano, MandrakeRoot };
        private static List<Component> FlavorTier1Three = new List<Component>() { Oregano, MandrakeRoot, Peppercorns };
        private static List<Component> FlavorTier1Four = new List<Component>() { Oregano, MandrakeRoot, Peppercorns, Grass };
        private static List<Component> FlavorTier2Three = new List<Component>() { Cinnamon, MuntokPeppercorns, Seaweed };
        private static List<Component> FlavorTier2Four = new List<Component>() { Cinnamon, MuntokPeppercorns, Seaweed, MyconianJelly };
        private static List<Component> FlavorTier3Three = new List<Component>() { Mint, Honey, JuniperBerries };
        private static List<Component> FlavorTier3Four = new List<Component>() { Mint, Honey, JuniperBerries, Almonds };

        public Alcoholx4 BasicLager { get; private set; } = new Alcoholx4("Basic Lager",
            FruitTier1Four);

        public Alcoholx4x4 PaleAle { get; private set; } = new Alcoholx4x4("Pale Ale",
            FruitTier1Four,
            VeggieTier1);

        public Alcoholx4x4x2 Marzen { get; private set; } = new Alcoholx4x4x2("Marzen",
            FruitTier1Four,
            VeggieTier1,
            FlavorTier1Two);

        public Alcoholx3x3x4x3 GoblinAle { get; private set; } = new Alcoholx3x3x4x3("Goblin Ale",
            FruitTier1Three,
            FruitTier2,
            VeggieTier1,
            FlavorTier1Three);

        public Alcoholx4x3x4x3 OrcishBock { get; private set; } = new Alcoholx4x3x4x3("Orcish Bock",
            VeggieTier2,
            FruitTier2,
            MushroomTier3,
            FlavorTier1Three);

        public Alcoholx4x3x4x3 BrownAle { get; private set; } = new Alcoholx4x3x4x3("Brown Ale",
            VeggieTier3Beer,
            FruitTier2,
            MushroomTier3,
            FlavorTier2Three);

        public Alcoholx4x3x4x3 HegemonyLager { get; private set; } = new Alcoholx4x3x4x3("Hegemony Lager",
            VeggieTier3Beer,
            FruitTier3,
            MushroomTier3,
            FlavorTier2Three);

        public Alcoholx4x3x4x3 DwarvenStout { get; private set; } = new Alcoholx4x3x4x3("Dwarven Stout",
            VeggieTier3Beer,
            FruitTier3,
            MushroomTier4,
            FlavorTier3Three);

        public Alcoholx3x4x5x4 PotatoVodka { get; private set; } = new Alcoholx3x4x5x4("Potato Vodka",
            FruitTier1Three,
            VeggieTier1,
            PartsTier1,
            FlavorTier1Four);

        public Alcoholx3x4x5x4 Applejack { get; private set; } = new Alcoholx3x4x5x4("Applejack",
            FruitTier1Three,
            VeggieTier2,
            PartsTier1,
            FlavorTier1Four);

        public Alcoholx3x4x5x4 BeetVodka { get; private set; } = new Alcoholx3x4x5x4("Beet Vodka",
            FruitTier2,
            VeggieTier1,
            PartsTier1,
            FlavorTier1Four);

        public Alcoholx3x4x5x4 PaleRum { get; private set; } = new Alcoholx3x4x5x4("Pale Rum",
            FruitTier2,
            MushroomTier3,
            PartsTier1,
            FlavorTier1Four);

        public Alcoholx3x4x5x4 Whisky { get; private set; } = new Alcoholx3x4x5x4("Whisky",
            FruitTier2,
            MushroomTier3,
            PartsTier2,
            FlavorTier1Four);

        public Alcoholx3x4x5x4 Tequila { get; private set; } = new Alcoholx3x4x5x4("Tequila",
            FruitTier2,
            MushroomTier3,
            PartsTier2,
            FlavorTier2Four);

        public Alcoholx3x4x5x4 DryGin { get; private set; } = new Alcoholx3x4x5x4("Dry Gin",
            FruitTier3,
            MushroomTier4,
            PartsTier2,
            FlavorTier2Four);

        public Alcoholx3x4x5x4 Bourbon { get; private set; } = new Alcoholx3x4x5x4("Bourbon",
            FruitTier3,
            MushroomTier4,
            PartsTier3,
            FlavorTier3Four);

        public ComponentAssociationCollection AssociationFruit1 { get; } = new ComponentAssociationCollection("Fruit1", new List<ComponentAssociation>()
        {
            new ComponentAssociation(RedApple, FruitTier2),
            new ComponentAssociation(Grapes, FruitTier2),
            new ComponentAssociation(Orange, FruitTier2),
        });

        public ComponentAssociationCollection AssociationFruit2 { get; } = new ComponentAssociationCollection("Fruit2", new List<ComponentAssociation>()
        {
            new ComponentAssociation(Guava, FruitTier3),
            new ComponentAssociation(Banana, FruitTier3),
            new ComponentAssociation(Lemon, FruitTier3),
        });

        public ComponentAssociationCollection AssociationVeggie1 { get; } = new ComponentAssociationCollection("Veggie1", new List<ComponentAssociation>()
        {
            new ComponentAssociation(ParasolMushroomFlakes, VeggieTier2),
            new ComponentAssociation(MycenaMushroomFlakes, VeggieTier2),
            new ComponentAssociation(BoletusMushroomFlakes, VeggieTier2),
            new ComponentAssociation(FieldMushroomFlakes, VeggieTier2),
        });

        public ComponentAssociationCollection AssociationVeggie2Beer { get; } = new ComponentAssociationCollection("Veggie2Beer", new List<ComponentAssociation>()
        {
            new ComponentAssociation(Beet, VeggieTier3Beer),
            new ComponentAssociation(Squash, VeggieTier3Beer),
            new ComponentAssociation(Broccoli, VeggieTier3Beer),
            new ComponentAssociation(Carrot, VeggieTier3Beer),
        });

        public ComponentAssociationCollection AssociationVeggie2Liquor { get; } = new ComponentAssociationCollection("Veggie2Liquor", new List<ComponentAssociation>()
        {
            new ComponentAssociation(Beet, MushroomTier3),
            new ComponentAssociation(Squash, MushroomTier3),
            new ComponentAssociation(Broccoli, MushroomTier3),
            new ComponentAssociation(Carrot, MushroomTier3),
        });

        public ComponentAssociationCollection AssociationMushroom3 { get; } = new ComponentAssociationCollection("Mushroom3", new List<ComponentAssociation>()
        {
            new ComponentAssociation(FieldMushroomFlakes, MushroomTier4),
            new ComponentAssociation(BlusherMushroomFlakes, MushroomTier4),
            new ComponentAssociation(MilkCapMushroomPowder, MushroomTier4),
            new ComponentAssociation(BloodMushroomPowder, MushroomTier4),
        });

        public ComponentAssociationCollection AssociationParts1 { get; } = new ComponentAssociationCollection("Parts1", new List<ComponentAssociation>()
        {
            new ComponentAssociation(BoarTusk, PartsTier2),
            new ComponentAssociation(CatEyeball, PartsTier2),
            new ComponentAssociation(SnailSinew, PartsTier2),
            new ComponentAssociation(RatTail, PartsTier2),
            new ComponentAssociation(BasicFishScale, PartsTier2),
        });

        public ComponentAssociationCollection AssociationParts2 { get; } = new ComponentAssociationCollection("Parts2", new List<ComponentAssociation>()
        {
            new ComponentAssociation(WolfTeeth, PartsTier3),
            new ComponentAssociation(PantherTail, PartsTier3),
            new ComponentAssociation(DeinonychusClaw, PartsTier3),
            new ComponentAssociation(RabbitsFoot, PartsTier3),
            new ComponentAssociation(BearGallbladder, PartsTier3),
        });

        public ComponentAssociationCollection AssociationFlavor1Beer { get; } = new ComponentAssociationCollection("Flavor1Beer", new List<ComponentAssociation>()
        {
            new ComponentAssociation(Oregano, FlavorTier2Three),
            new ComponentAssociation(MandrakeRoot, FlavorTier2Three),
            new ComponentAssociation(Peppercorns, FlavorTier2Three),
        });

        public ComponentAssociationCollection AssociationFlavor1Liquor { get; } = new ComponentAssociationCollection("Flavor1Liquor", new List<ComponentAssociation>()
        {
            new ComponentAssociation(Oregano, FlavorTier2Four),
            new ComponentAssociation(MandrakeRoot, FlavorTier2Four),
            new ComponentAssociation(Peppercorns, FlavorTier2Four),
            new ComponentAssociation(Grass, FlavorTier2Four),
        });

        public ComponentAssociationCollection AssociationFlavor2Beer { get; } = new ComponentAssociationCollection("Flavor2Beer", new List<ComponentAssociation>()
        {
            new ComponentAssociation(Cinnamon, FlavorTier3Three),
            new ComponentAssociation(MuntokPeppercorns, FlavorTier3Three),
            new ComponentAssociation(Seaweed, FlavorTier3Three),
        });

        public ComponentAssociationCollection AssociationFlavor2Liquor { get; } = new ComponentAssociationCollection("Flavor2Liquor", new List<ComponentAssociation>()
        {
            new ComponentAssociation(Cinnamon, FlavorTier3Four),
            new ComponentAssociation(MuntokPeppercorns, FlavorTier3Four),
            new ComponentAssociation(Seaweed, FlavorTier3Four),
            new ComponentAssociation(MyconianJelly, FlavorTier3Four),
        });

        public List<ComponentAssociationCollection> AssociationTable { get; } = new List<ComponentAssociationCollection>();
        #endregion

        #region Events
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

        private void OnSave(object sender, RoutedEventArgs e)
        {
            SaveAll();
            IsChanged = false;
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
            PotatoVodka.Save();
            Applejack.Save();
            BeetVodka.Save();
            PaleRum.Save();
            Whisky.Save();
            Tequila.Save();
            DryGin.Save();
            Bourbon.Save();

            SaveAssociations();
            SaveGUI();
        }

        private void SaveAssociations()
        {
            foreach (ComponentAssociationCollection AssociationList in AssociationTable)
            {
                List<int> IndexList = new List<int>();
                for (int i = 0; i < AssociationList.Count; i++)
                    IndexList.Add(AssociationList[i].AssociationIndex);

                DataArchive.SetIndexList($"{AssociationSettingName}{AssociationList.Name}", IndexList);
            }
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

        private void OnDelete(object sender, RoutedEventArgs e)
        {
            ComponentAssociation Association = (sender as Button).DataContext as ComponentAssociation;
            Association.AssociationIndex = -1;
        }

        private void OnExport(object sender, RoutedEventArgs e)
        {
            SaveFileDialog Dlg = new SaveFileDialog();
            Dlg.Filter = "CSV file (*.csv)|*.csv";
            bool? Continue = Dlg.ShowDialog(this);

            if (Continue.HasValue && Continue.Value)
            {
                MessageBoxResult Answer = MessageBox.Show("Include calculated recipes? If you answer 'No', only confirmed recipes will be exported.", "Export", MessageBoxButton.YesNo, MessageBoxImage.Question);
                bool IsCalculatedIncluded = Answer == MessageBoxResult.Yes;

                OnExport(Dlg.FileName, IsCalculatedIncluded);
            }
        }

        private void OnExport(string FileName, bool isCalculatedIncluded)
        {
            try
            {
                using (FileStream Stream = new FileStream(FileName, FileMode.Create, FileAccess.Write))
                {
                    using (StreamWriter Writer = new StreamWriter(Stream, Encoding.UTF8))
                    {
                        OnExport(Writer, isCalculatedIncluded);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void OnExport(StreamWriter writer, bool isCalculatedIncluded)
        {
            ExportAssociations(writer);

            BasicLager.Export(writer, isCalculatedIncluded);
            PaleAle.Export(writer, isCalculatedIncluded);
            Marzen.Export(writer, isCalculatedIncluded);
            GoblinAle.Export(writer, isCalculatedIncluded);
            OrcishBock.Export(writer, isCalculatedIncluded);
            BrownAle.Export(writer, isCalculatedIncluded);
            HegemonyLager.Export(writer, isCalculatedIncluded);
            DwarvenStout.Export(writer, isCalculatedIncluded);
            PotatoVodka.Export(writer, isCalculatedIncluded);
            Applejack.Export(writer, isCalculatedIncluded);
            BeetVodka.Export(writer, isCalculatedIncluded);
            PaleRum.Export(writer, isCalculatedIncluded);
            Whisky.Export(writer, isCalculatedIncluded);
            Tequila.Export(writer, isCalculatedIncluded);
            DryGin.Export(writer, isCalculatedIncluded);
            Bourbon.Export(writer, isCalculatedIncluded);
        }

        private void ExportAssociations(StreamWriter writer)
        {
            writer.WriteLine("Associations");
            writer.WriteLine();

            foreach (ComponentAssociationCollection AssociationList in AssociationTable)
            {
                writer.WriteLine(AssociationList.Name);

                foreach (ComponentAssociation Association in AssociationList)
                {
                    if (Association.AssociationIndex >= 0)
                        writer.WriteLine($"{Association.Component.Name};{Association.ChoiceList[Association.AssociationIndex]}");
                    else
                        writer.WriteLine($"{Association.Component.Name};");
                }

                writer.WriteLine();
            }

            writer.WriteLine();
        }
        #endregion

        #region Client Interface
        public void Recalculate()
        {
            OrcishBock.ClearCalculateIndexes();
            BrownAle.ClearCalculateIndexes();
            HegemonyLager.ClearCalculateIndexes();
            DwarvenStout.ClearCalculateIndexes();
            PotatoVodka.ClearCalculateIndexes();
            Applejack.ClearCalculateIndexes();
            BeetVodka.ClearCalculateIndexes();
            PaleRum.ClearCalculateIndexes();
            Whisky.ClearCalculateIndexes();
            Tequila.ClearCalculateIndexes();
            DryGin.ClearCalculateIndexes();
            Bourbon.ClearCalculateIndexes();

            RecalculateBottomToTop(OrcishBock, BrownAle, AssociationVeggie2Beer, null, null, AssociationFlavor1Beer);
            RecalculateBottomToTop(BrownAle, HegemonyLager, null, AssociationFruit2, null, null);
            RecalculateBottomToTop(HegemonyLager, DwarvenStout, null, null, AssociationMushroom3, AssociationFlavor2Beer);
            RecalculateBottomToTop(PotatoVodka, Applejack, null, AssociationVeggie1, null, null);
            RecalculateBottomToTop(Applejack, BeetVodka, AssociationFruit1, null, null, null);
            RecalculateBottomToTop(BeetVodka, PaleRum, null, null, null, null);
            RecalculateBottomToTop(PaleRum, Whisky, null, null, AssociationParts1, null);
            RecalculateBottomToTop(Whisky, Tequila, null, null, null, AssociationFlavor1Liquor);
            RecalculateBottomToTop(Tequila, DryGin, AssociationFruit2, AssociationMushroom3, null, null);
            RecalculateBottomToTop(DryGin, Bourbon, null, null, AssociationParts2, AssociationFlavor2Liquor);

            RecalculateTopToBottom(Bourbon, DryGin, null, null, AssociationParts2, AssociationFlavor2Liquor);
            RecalculateTopToBottom(DryGin, Tequila, AssociationFruit2, AssociationMushroom3, null, null);
            RecalculateTopToBottom(Tequila, Whisky, null, null, null, AssociationFlavor1Liquor);
            RecalculateTopToBottom(Whisky, PaleRum, null, null, AssociationParts1, null);
            RecalculateTopToBottom(PaleRum, BeetVodka, null, null, null, null);
            RecalculateTopToBottom(BeetVodka, Applejack, AssociationFruit1, null, null, null);
            RecalculateTopToBottom(Applejack, PotatoVodka, null, AssociationVeggie1, null, null);
            RecalculateTopToBottom(DwarvenStout, HegemonyLager, null, null, AssociationMushroom3, AssociationFlavor2Beer);
            RecalculateTopToBottom(HegemonyLager, BrownAle, null, AssociationFruit2, null, null);
            RecalculateTopToBottom(BrownAle, OrcishBock, AssociationVeggie2Beer, null, null, AssociationFlavor1Beer);

            OrcishBock.RecalculateMismatchCount();
            BrownAle.RecalculateMismatchCount();
            HegemonyLager.RecalculateMismatchCount();
            DwarvenStout.RecalculateMismatchCount();
            PotatoVodka.RecalculateMismatchCount();
            Applejack.RecalculateMismatchCount();
            BeetVodka.RecalculateMismatchCount();
            PaleRum.RecalculateMismatchCount();
            Whisky.RecalculateMismatchCount();
            Tequila.RecalculateMismatchCount();
            DryGin.RecalculateMismatchCount();
            Bourbon.RecalculateMismatchCount();
        }

        public void RecalculateBottomToTop(IFourComponentsAlcohol previous, IFourComponentsAlcohol next, ComponentAssociationCollection associationList1, ComponentAssociationCollection associationList2, ComponentAssociationCollection associationList3, ComponentAssociationCollection associationList4)
        {
            for (int PreviousLineIndex = 0; PreviousLineIndex < previous.Lines.Count; PreviousLineIndex++)
            {
                IFourComponentsAlcoholLine Line = previous.Lines[PreviousLineIndex] as IFourComponentsAlcoholLine;
                int BestIndex = Line.BestIndex;
                if (BestIndex >= 0)
                {
                    int NextIndex1 = GetPreviousToNextIndex(associationList1, Line.Index1);
                    int NextIndex2 = GetPreviousToNextIndex(associationList2, Line.Index2);
                    int NextIndex3 = GetPreviousToNextIndex(associationList3, Line.Index3);
                    int NextIndex4 = GetPreviousToNextIndex(associationList4, Line.Index4);

                    if (NextIndex1 >= 0 && NextIndex2 >= 0 && NextIndex3 >= 0 && NextIndex4 >= 0)
                    {
                        int NextLineIndex = (NextIndex1 * 3 * 4 * 3) + (NextIndex2 * 4 * 3) + (NextIndex3 * 3) + NextIndex4;
                        next.Lines[NextLineIndex].CalculatedIndex = BestIndex;
                    }
                }
            }
        }

        public void RecalculateTopToBottom(IFourComponentsAlcohol next, IFourComponentsAlcohol previous, ComponentAssociationCollection associationList1, ComponentAssociationCollection associationList2, ComponentAssociationCollection associationList3, ComponentAssociationCollection associationList4)
        {
            for (int NextLineIndex = 0; NextLineIndex < next.Lines.Count; NextLineIndex++)
            {
                IFourComponentsAlcoholLine Line = next.Lines[NextLineIndex] as IFourComponentsAlcoholLine;
                int BestIndex = Line.BestIndex;
                if (BestIndex >= 0)
                {
                    int PreviousIndex1 = GetNextToPreviousIndex(associationList1, Line.Index1);
                    int PreviousIndex2 = GetNextToPreviousIndex(associationList2, Line.Index2);
                    int PreviousIndex3 = GetNextToPreviousIndex(associationList3, Line.Index3);
                    int PreviousIndex4 = GetNextToPreviousIndex(associationList4, Line.Index4);

                    if (PreviousIndex1 >= 0 && PreviousIndex2 >= 0 && PreviousIndex3 >= 0 && PreviousIndex4 >= 0)
                    {
                        int PreviousLineIndex = (PreviousIndex1 * 3 * 4 * 3) + (PreviousIndex2 * 4 * 3) + (PreviousIndex3 * 3) + PreviousIndex4;
                        previous.Lines[PreviousLineIndex].CalculatedIndex = BestIndex;
                    }
                }
            }
        }

        private int GetPreviousToNextIndex(ComponentAssociationCollection associationList, int previousIndex)
        {
            int NextIndex;

            if (associationList == null)
                NextIndex = previousIndex;
            else if (associationList[previousIndex].AssociationIndex >= 0)
                NextIndex = associationList[previousIndex].AssociationIndex;
            else
                NextIndex = -1;

            return NextIndex;
        }

        private int GetNextToPreviousIndex(ComponentAssociationCollection associationList, int nextIndex)
        {
            int PreviousIndex;

            if (associationList == null)
                PreviousIndex = nextIndex;
            else
            {
                PreviousIndex = -1;

                for (int i = 0; i < associationList.Count; i++)
                    if (associationList[i].AssociationIndex == nextIndex)
                    {
                        PreviousIndex = i;
                        break;
                    }
            }

            return PreviousIndex;
        }

        public static void SetChanged()
        {
            MainWindow Window = App.Current.MainWindow as MainWindow;
            Window.IsChanged = true;
        }
        #endregion

        #region Implementation of INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        public void NotifyThisPropertyChanged([CallerMemberName] string PropertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
        #endregion
    }
}

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
        public static Component Strawberry { get; } = new Component("Strawberry");
        public static Component ParasolMushroomFlakes { get; } = new Component("Parasol Mushroom Flakes");
        public static Component MycenaMushroomFlakes { get; } = new Component("Mycena Mushroom Flakes");
        public static Component BoletusMushroomFlakes { get; } = new Component("Boletus Mushroom Flakes");
        public static Component FieldMushroomFlakes { get; } = new Component("Field Mushroom Flakes");
        public static Component Oregano { get; } = new Component("Oregano");
        public static Component MandrakeRoot { get; } = new Component("Mandrake Root");
        public static Component Guava { get; } = new Component("Guava");
        public static Component Banana { get; } = new Component("Banana");
        public static Component Lemon { get; } = new Component("Lemon");
        public static Component Peppercorns { get; } = new Component("Peppercorns");
        public static Component Beet { get; } = new Component("Beet");
        public static Component Squash { get; } = new Component("Squash");
        public static Component Broccoli { get; } = new Component("Broccoli");
        public static Component Carrot { get; } = new Component("Carrot");
        public static Component BlusherMushroomFlakes { get; } = new Component("Blusher Mushroom Flakes");
        public static Component MilkCapMushroomPowder { get; } = new Component("Milk Cap Mushroom Powder");
        public static Component BloodMushroomPowder { get; } = new Component("Blood Mushroom Powder");

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

        private static List<Component> FruitTier1 = new List<Component>() { RedApple, Grapes, Orange };
        private static List<Component> FruitTier2 = new List<Component>() { Guava, Banana, Lemon };

        public List<ComponentAssociation> AssociationList { get; } = new List<ComponentAssociation>()
        {
            new ComponentAssociation(RedApple, FruitTier2),
            new ComponentAssociation(Grapes, FruitTier2),
            new ComponentAssociation(Orange, FruitTier2),
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
    }
}

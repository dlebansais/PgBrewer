namespace PgBrewer;

using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

public class PgBrewerPageSettings : PgBrewerPage
{
    #region Init
    public PgBrewerPageSettings(Settings settings, BackForward backForward)
        : base(backForward)
    {
        LoadAssociations(settings);
    }
    #endregion

    #region Properties
    public override string Name { get; } = "Settings";
    public override int IconId { get; } = 5476;

    public ComponentAssociationCollection AssociationFruit1 { get; } = new ComponentAssociationCollection("Fruit1", new List<ComponentAssociation>()
    {
        new ComponentAssociation(Component.RedApple, Component.FruitTier2),
        new ComponentAssociation(Component.Grapes, Component.FruitTier2),
        new ComponentAssociation(Component.Orange, Component.FruitTier2),
    });

    public ComponentAssociationCollection AssociationFruit2 { get; } = new ComponentAssociationCollection("Fruit2", new List<ComponentAssociation>()
    {
        new ComponentAssociation(Component.Guava, Component.FruitTier3),
        new ComponentAssociation(Component.Banana, Component.FruitTier3),
        new ComponentAssociation(Component.Lemon, Component.FruitTier3),
    });

    public ComponentAssociationCollection AssociationVeggie1 { get; } = new ComponentAssociationCollection("Veggie1", new List<ComponentAssociation>()
    {
        new ComponentAssociation(Component.ParasolMushroomFlakes, Component.VeggieTier2),
        new ComponentAssociation(Component.MycenaMushroomFlakes, Component.VeggieTier2),
        new ComponentAssociation(Component.BoletusMushroomFlakes, Component.VeggieTier2),
        new ComponentAssociation(Component.FieldMushroomFlakes, Component.VeggieTier2),
    });

    public ComponentAssociationCollection AssociationVeggie2Beer { get; } = new ComponentAssociationCollection("Veggie2Beer", new List<ComponentAssociation>()
    {
        new ComponentAssociation(Component.Beet, Component.VeggieTier3Beer),
        new ComponentAssociation(Component.Squash, Component.VeggieTier3Beer),
        new ComponentAssociation(Component.Broccoli, Component.VeggieTier3Beer),
        new ComponentAssociation(Component.Carrot, Component.VeggieTier3Beer),
    });

    public ComponentAssociationCollection AssociationVeggie2Liquor { get; } = new ComponentAssociationCollection("Veggie2Liquor", new List<ComponentAssociation>()
    {
        new ComponentAssociation(Component.Beet, Component.MushroomTier3),
        new ComponentAssociation(Component.Squash, Component.MushroomTier3),
        new ComponentAssociation(Component.Broccoli, Component.MushroomTier3),
        new ComponentAssociation(Component.Carrot, Component.MushroomTier3),
    });

    public ComponentAssociationCollection AssociationMushroom3 { get; } = new ComponentAssociationCollection("Mushroom3", new List<ComponentAssociation>()
    {
        new ComponentAssociation(Component.FieldMushroomFlakes, Component.MushroomTier4),
        new ComponentAssociation(Component.BlusherMushroomFlakes, Component.MushroomTier4),
        new ComponentAssociation(Component.MilkCapMushroomPowder, Component.MushroomTier4),
        new ComponentAssociation(Component.BloodMushroomPowder, Component.MushroomTier4),
    });

    public ComponentAssociationCollection AssociationParts1 { get; } = new ComponentAssociationCollection("Parts1", new List<ComponentAssociation>()
    {
        new ComponentAssociation(Component.BoarTusk, Component.PartsTier2),
        new ComponentAssociation(Component.CatEyeball, Component.PartsTier2),
        new ComponentAssociation(Component.SnailSinew, Component.PartsTier2),
        new ComponentAssociation(Component.RatTail, Component.PartsTier2),
        new ComponentAssociation(Component.BasicFishScale, Component.PartsTier2),
    });

    public ComponentAssociationCollection AssociationParts2 { get; } = new ComponentAssociationCollection("Parts2", new List<ComponentAssociation>()
    {
        new ComponentAssociation(Component.WolfTeeth, Component.PartsTier3),
        new ComponentAssociation(Component.PantherTail, Component.PartsTier3),
        new ComponentAssociation(Component.DeinonychusClaw, Component.PartsTier3),
        new ComponentAssociation(Component.RabbitsFoot, Component.PartsTier3),
        new ComponentAssociation(Component.BearGallbladder, Component.PartsTier3),
    });

    public ComponentAssociationCollection AssociationFlavor1Beer { get; } = new ComponentAssociationCollection("Flavor1Beer", new List<ComponentAssociation>()
    {
        new ComponentAssociation(Component.Oregano, Component.FlavorTier2Three),
        new ComponentAssociation(Component.MandrakeRoot, Component.FlavorTier2Three),
        new ComponentAssociation(Component.Peppercorns, Component.FlavorTier2Three),
    });

    public ComponentAssociationCollection AssociationFlavor1Liquor { get; } = new ComponentAssociationCollection("Flavor1Liquor", new List<ComponentAssociation>()
    {
        new ComponentAssociation(Component.Oregano, Component.FlavorTier2Four),
        new ComponentAssociation(Component.MandrakeRoot, Component.FlavorTier2Four),
        new ComponentAssociation(Component.Peppercorns, Component.FlavorTier2Four),
        new ComponentAssociation(Component.Grass, Component.FlavorTier2Four),
    });

    public ComponentAssociationCollection AssociationFlavor2Beer { get; } = new ComponentAssociationCollection("Flavor2Beer", new List<ComponentAssociation>()
    {
        new ComponentAssociation(Component.Cinnamon, Component.FlavorTier3Three),
        new ComponentAssociation(Component.MuntokPeppercorns, Component.FlavorTier3Three),
        new ComponentAssociation(Component.Seaweed, Component.FlavorTier3Three),
    });

    public ComponentAssociationCollection AssociationFlavor2Liquor { get; } = new ComponentAssociationCollection("Flavor2Liquor", new List<ComponentAssociation>()
    {
        new ComponentAssociation(Component.Cinnamon, Component.FlavorTier3Four),
        new ComponentAssociation(Component.MuntokPeppercorns, Component.FlavorTier3Four),
        new ComponentAssociation(Component.Seaweed, Component.FlavorTier3Four),
        new ComponentAssociation(Component.MyconianJelly, Component.FlavorTier3Four),
    });
    #endregion

    #region Implementation
    public void LoadAssociations(Settings settings)
    {
        LoadAssociations(settings, AssociationFruit1);
        LoadAssociations(settings, AssociationFruit2);
        LoadAssociations(settings, AssociationVeggie1);
        LoadAssociations(settings, AssociationVeggie2Beer);
        LoadAssociations(settings, AssociationVeggie2Liquor);
        LoadAssociations(settings, AssociationMushroom3);
        LoadAssociations(settings, AssociationParts1);
        LoadAssociations(settings, AssociationParts2);
        LoadAssociations(settings, AssociationFlavor1Beer);
        LoadAssociations(settings, AssociationFlavor1Liquor);
        LoadAssociations(settings, AssociationFlavor2Beer);
        LoadAssociations(settings, AssociationFlavor2Liquor);
    }

    private void LoadAssociations(Settings settings, ComponentAssociationCollection associationList)
    {
        List<int> AssociationIndexes = DataArchive.GetIndexList(settings, $"{AssociationSettingName}{associationList.Name}", associationList.Count);
        for (int i = 0; i < associationList.Count; i++)
            associationList[i].AssociationIndex = AssociationIndexes[i];

        AssociationTable.Add(associationList);
    }

    public void SaveAssociations(Settings settings)
    {
        foreach (ComponentAssociationCollection AssociationList in AssociationTable)
        {
            List<int> IndexList = new List<int>();
            for (int i = 0; i < AssociationList.Count; i++)
                IndexList.Add(AssociationList[i].AssociationIndex);

            DataArchive.SetIndexList(settings, $"{AssociationSettingName}{AssociationList.Name}", IndexList);
        }
    }

    public void ExportAssociations(StreamWriter writer)
    {
        writer.WriteLine("Associations");
        writer.WriteLine();

        foreach (ComponentAssociationCollection AssociationList in AssociationTable)
        {
            writer.WriteLine(AssociationList.Name);

            foreach (ComponentAssociation Association in AssociationList)
            {
                string AssociationName = Association.Component.Name;

                if (Association.AssociationIndex >= 0)
                    writer.WriteLine($"{AssociationName};{Association.ChoiceList[Association.AssociationIndex]}");
                else
                    writer.WriteLine($"{AssociationName};");
            }

            writer.WriteLine();
        }
    }

    public bool ImportAssociations(StreamReader reader, ref int changeCount)
    {
        if (reader.ReadLine() != "Associations")
            return false;

        reader.ReadLine();

        foreach (ComponentAssociationCollection AssociationList in AssociationTable)
        {
            if (AssociationList.Name != reader.ReadLine())
                return false;

            foreach (ComponentAssociation Association in AssociationList)
            {
                string AssociationString = reader.ReadLine()!;
                string AssociationName = Association.Component.Name;
                AssociationName += ";";

                if (!AssociationString.StartsWith(AssociationName))
                    return false;

                AssociationString = AssociationString.Substring(AssociationName.Length);
                if (AssociationString.Length > 0)
                {
                    int SelectedIndex = -1;
                    for (int i = 0; i < Association.ChoiceList.Count; i++)
                    {
                        Component Choice = Association.ChoiceList[i];
                        if (Choice.ToString() == AssociationString)
                        {
                            SelectedIndex = i;
                            break;
                        }
                    }

                    if (SelectedIndex < 0)
                        return false;

                    if (Association.AssociationIndex != SelectedIndex)
                    {
                        Association.AssociationIndex = SelectedIndex;
                        changeCount++;
                    }
                }
                else if (Association.AssociationIndex >= 0)
                {
                    Association.AssociationIndex = -1;
                    changeCount++;
                }
            }

            reader.ReadLine();
        }

        reader.ReadLine();

        return true;
    }

    private static readonly string AssociationSettingName = "Association-";
    private List<ComponentAssociationCollection> AssociationTable { get; } = new();
    #endregion
}

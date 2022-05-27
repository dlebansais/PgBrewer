namespace PgBrewer;

using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

/// <summary>
/// Main window implementation.
/// </summary>
public partial class MainWindow
{
    public BackForward BackForward { get; } = new();
    public PgBrewerPageBeers PageBeers { get; private set; } = null!;
    public PgBrewerPageLiquors PageLiquors { get; private set; } = null!;
    public PgBrewerPageSettings PageSettings { get; private set; } = null!;

    private static readonly string AssociationSettingName = "Association";

    public async Task InitializePages()
    {
        PageBeers = await PgBrewerPageBeers.Create(BackForward);
        PageLiquors = await PgBrewerPageLiquors.Create(BackForward);
        PageSettings = new PgBrewerPageSettings(BackForward);

        await LoadAssociations();

        PageList.Add(PageBeers);
        PageList.Add(PageLiquors);
        PageList.Add(PageSettings);
    }

    private async Task LoadAssociations()
    {
        await LoadAssociations(PageSettings.AssociationFruit1);
        await LoadAssociations(PageSettings.AssociationFruit2);
        await LoadAssociations(PageSettings.AssociationVeggie1);
        await LoadAssociations(PageSettings.AssociationVeggie2Beer);
        await LoadAssociations(PageSettings.AssociationVeggie2Liquor);
        await LoadAssociations(PageSettings.AssociationMushroom3);
        await LoadAssociations(PageSettings.AssociationParts1);
        await LoadAssociations(PageSettings.AssociationParts2);
        await LoadAssociations(PageSettings.AssociationFlavor1Beer);
        await LoadAssociations(PageSettings.AssociationFlavor1Liquor);
        await LoadAssociations(PageSettings.AssociationFlavor2Beer);
        await LoadAssociations(PageSettings.AssociationFlavor2Liquor);
    }

    private async Task LoadAssociations(ComponentAssociationCollection associationList)
    {
        List<int> AssociationIndexes = await DataArchive.GetIndexList($"{AssociationSettingName}{associationList.Name}", associationList.Count);
        for (int i = 0; i < associationList.Count; i++)
            associationList[i].AssociationIndex = AssociationIndexes[i];

        AssociationTable.Add(associationList);
    }

    public async Task SaveAll()
    {
        await PageBeers.SaveAll();
        await PageLiquors.SaveAll();

        await SaveAssociations();
    }

    private async Task SaveAssociations()
    {
        foreach (ComponentAssociationCollection AssociationList in AssociationTable)
        {
            List<int> IndexList = new List<int>();
            for (int i = 0; i < AssociationList.Count; i++)
                IndexList.Add(AssociationList[i].AssociationIndex);

            await DataArchive.SetIndexList($"{AssociationSettingName}{AssociationList.Name}", IndexList);
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

        writer.WriteLine();

        PageBeers.ExportAll(writer);
        PageLiquors.ExportAll(writer);
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

        if (!PageBeers.ImportAll(reader, ref changeCount))
            return false;

        if (!PageLiquors.ImportAll(reader, ref changeCount))
            return false;

        return true;
    }

    public List<ComponentAssociationCollection> AssociationTable { get; } = new();
}

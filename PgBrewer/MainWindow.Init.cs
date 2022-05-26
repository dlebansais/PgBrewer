namespace PgBrewer;

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

/// <summary>
/// Main window implementation.
/// </summary>
public partial class MainWindow
{
    private void LoadAssociations()
    {
        foreach (PgBrewerPage Page in PageList)
            if (Page is PgBrewerPageSettings AsPageSettings)
            {
                LoadAssociations(AsPageSettings.AssociationFruit1);
                LoadAssociations(AsPageSettings.AssociationFruit2);
                LoadAssociations(AsPageSettings.AssociationVeggie1);
                LoadAssociations(AsPageSettings.AssociationVeggie2Beer);
                LoadAssociations(AsPageSettings.AssociationVeggie2Liquor);
                LoadAssociations(AsPageSettings.AssociationMushroom3);
                LoadAssociations(AsPageSettings.AssociationParts1);
                LoadAssociations(AsPageSettings.AssociationParts2);
                LoadAssociations(AsPageSettings.AssociationFlavor1Beer);
                LoadAssociations(AsPageSettings.AssociationFlavor1Liquor);
                LoadAssociations(AsPageSettings.AssociationFlavor2Beer);
                LoadAssociations(AsPageSettings.AssociationFlavor2Liquor);
            }
    }

    private void LoadAssociations(ComponentAssociationCollection associationList)
    {
        List<int> AssociationIndexes = DataArchive.GetIndexList($"{AssociationSettingName}{associationList.Name}", associationList.Count);
        for (int i = 0; i < associationList.Count; i++)
            associationList[i].AssociationIndex = AssociationIndexes[i];

        AssociationTable.Add(associationList);
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        Alcohol.Chain(PageBeers.OrcishBock, PageBeers.BrownAle, new List<ComponentAssociationCollection>() { PageSettings.AssociationVeggie2Beer, ComponentAssociationCollection.None, ComponentAssociationCollection.None, PageSettings.AssociationFlavor1Beer });
        Alcohol.Chain(PageBeers.BrownAle, PageBeers.HegemonyLager, new List<ComponentAssociationCollection>() { ComponentAssociationCollection.None, PageSettings.AssociationFruit2, ComponentAssociationCollection.None, ComponentAssociationCollection.None });
        Alcohol.Chain(PageBeers.HegemonyLager, PageBeers.DwarvenStout, new List<ComponentAssociationCollection>() { ComponentAssociationCollection.None, ComponentAssociationCollection.None, PageSettings.AssociationMushroom3, PageSettings.AssociationFlavor2Beer });

        Alcohol.Chain(PageLiquors.PotatoVodka, PageLiquors.Applejack, new List<ComponentAssociationCollection>() { ComponentAssociationCollection.None, PageSettings.AssociationVeggie1, ComponentAssociationCollection.None, ComponentAssociationCollection.None });
        Alcohol.Chain(PageLiquors.Applejack, PageLiquors.BeetVodka, new List<ComponentAssociationCollection>() { PageSettings.AssociationFruit1, ComponentAssociationCollection.None, ComponentAssociationCollection.None, ComponentAssociationCollection.None });
        Alcohol.Chain(PageLiquors.BeetVodka, PageLiquors.PaleRum, new List<ComponentAssociationCollection>() { ComponentAssociationCollection.None, ComponentAssociationCollection.None, ComponentAssociationCollection.None, ComponentAssociationCollection.None });
        Alcohol.Chain(PageLiquors.PaleRum, PageLiquors.Whisky, new List<ComponentAssociationCollection>() { ComponentAssociationCollection.None, ComponentAssociationCollection.None, PageSettings.AssociationParts1, ComponentAssociationCollection.None });
        Alcohol.Chain(PageLiquors.Whisky, PageLiquors.Tequila, new List<ComponentAssociationCollection>() { ComponentAssociationCollection.None, ComponentAssociationCollection.None, ComponentAssociationCollection.None, PageSettings.AssociationFlavor1Liquor });
        Alcohol.Chain(PageLiquors.Tequila, PageLiquors.DryGin, new List<ComponentAssociationCollection>() { PageSettings.AssociationFruit2, PageSettings.AssociationMushroom3, ComponentAssociationCollection.None, ComponentAssociationCollection.None });
        Alcohol.Chain(PageLiquors.DryGin, PageLiquors.Bourbon, new List<ComponentAssociationCollection>() { ComponentAssociationCollection.None, ComponentAssociationCollection.None, PageSettings.AssociationParts2, PageSettings.AssociationFlavor2Liquor });

        Recalculate();
    }
}

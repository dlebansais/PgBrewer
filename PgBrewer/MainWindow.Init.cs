namespace PgBrewer;

using System.Collections.Generic;
using System.Windows;

/// <summary>
/// Main window implementation.
/// </summary>
public partial class MainWindow
{
    public override async void OnMainWindowLoaded(object sender, RoutedEventArgs e)
    {
        await InitializePages();

        PageBeers.SetSelected(true);

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

        IsPageListInitialized = true;
    }

    private bool IsPageListInitialized;
}

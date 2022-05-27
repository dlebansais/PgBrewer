namespace PgBrewer;

using System.Collections.Generic;
using System.Windows;

/// <summary>
/// Main window implementation.
/// </summary>
public partial class MainWindow
{
    private async void OnLoaded(object sender, RoutedEventArgs e)
    {
        await Globals.Initialize();

        PageList.Add(Globals.PageBeers);
        PageList.Add(Globals.PageLiquors);
        PageList.Add(Globals.PageSettings);
        Globals.PageBeers.SetSelected(true);

        Alcohol.Chain(Globals.PageBeers.OrcishBock, Globals.PageBeers.BrownAle, new List<ComponentAssociationCollection>() { Globals.PageSettings.AssociationVeggie2Beer, ComponentAssociationCollection.None, ComponentAssociationCollection.None, Globals.PageSettings.AssociationFlavor1Beer });
        Alcohol.Chain(Globals.PageBeers.BrownAle, Globals.PageBeers.HegemonyLager, new List<ComponentAssociationCollection>() { ComponentAssociationCollection.None, Globals.PageSettings.AssociationFruit2, ComponentAssociationCollection.None, ComponentAssociationCollection.None });
        Alcohol.Chain(Globals.PageBeers.HegemonyLager, Globals.PageBeers.DwarvenStout, new List<ComponentAssociationCollection>() { ComponentAssociationCollection.None, ComponentAssociationCollection.None, Globals.PageSettings.AssociationMushroom3, Globals.PageSettings.AssociationFlavor2Beer });

        Alcohol.Chain(Globals.PageLiquors.PotatoVodka, Globals.PageLiquors.Applejack, new List<ComponentAssociationCollection>() { ComponentAssociationCollection.None, Globals.PageSettings.AssociationVeggie1, ComponentAssociationCollection.None, ComponentAssociationCollection.None });
        Alcohol.Chain(Globals.PageLiquors.Applejack, Globals.PageLiquors.BeetVodka, new List<ComponentAssociationCollection>() { Globals.PageSettings.AssociationFruit1, ComponentAssociationCollection.None, ComponentAssociationCollection.None, ComponentAssociationCollection.None });
        Alcohol.Chain(Globals.PageLiquors.BeetVodka, Globals.PageLiquors.PaleRum, new List<ComponentAssociationCollection>() { ComponentAssociationCollection.None, ComponentAssociationCollection.None, ComponentAssociationCollection.None, ComponentAssociationCollection.None });
        Alcohol.Chain(Globals.PageLiquors.PaleRum, Globals.PageLiquors.Whisky, new List<ComponentAssociationCollection>() { ComponentAssociationCollection.None, ComponentAssociationCollection.None, Globals.PageSettings.AssociationParts1, ComponentAssociationCollection.None });
        Alcohol.Chain(Globals.PageLiquors.Whisky, Globals.PageLiquors.Tequila, new List<ComponentAssociationCollection>() { ComponentAssociationCollection.None, ComponentAssociationCollection.None, ComponentAssociationCollection.None, Globals.PageSettings.AssociationFlavor1Liquor });
        Alcohol.Chain(Globals.PageLiquors.Tequila, Globals.PageLiquors.DryGin, new List<ComponentAssociationCollection>() { Globals.PageSettings.AssociationFruit2, Globals.PageSettings.AssociationMushroom3, ComponentAssociationCollection.None, ComponentAssociationCollection.None });
        Alcohol.Chain(Globals.PageLiquors.DryGin, Globals.PageLiquors.Bourbon, new List<ComponentAssociationCollection>() { ComponentAssociationCollection.None, ComponentAssociationCollection.None, Globals.PageSettings.AssociationParts2, Globals.PageSettings.AssociationFlavor2Liquor });

        Recalculate();

        IsPageListInitialized = true;
    }

    private bool IsPageListInitialized;
}

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

    public async Task InitializePages()
    {
        Settings Settings = await DataArchive.ReadSettings();

        PageBeers = await PgBrewerPageBeers.Create(Settings, BackForward);
        PageLiquors = await PgBrewerPageLiquors.Create(Settings, BackForward);
        PageSettings = new PgBrewerPageSettings(Settings, BackForward);

        PageList.Add(PageBeers);
        PageList.Add(PageLiquors);
        PageList.Add(PageSettings);
    }

    public async Task SaveAll()
    {
        Settings Settings = new();
        PageBeers.SaveAll(Settings);
        PageLiquors.SaveAll(Settings);
        PageSettings.SaveAssociations(Settings);

        await DataArchive.WriteSettings(Settings);
    }
}

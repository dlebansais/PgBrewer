namespace PgBrewer;

using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

public class Alcoholx4 : Alcohol
{
    public static async Task<Alcoholx4> Create(string name, List<Component> componentList1)
    {
        Debug.Assert(componentList1.Count == 4);

        Alcoholx4 Instance = new(name, componentList1);
        await Instance.Init();

        return Instance;
    }

    private Alcoholx4(string name, List<Component> componentList1)
        : base(name)
    {
        ComponentList1 = componentList1;
    }

    private async Task Init()
    {
        await ReadEffectList();
        List<int> Indexes = await DataArchive.GetIndexList(Name, ComponentList1.Count);

        for (int ComponentIndex1 = 0; ComponentIndex1 < ComponentList1.Count; ComponentIndex1++)
        {
            int EffectIndex = ComponentIndex1;
            Lines.Add(new Alcoholx4Line(this, Indexes[EffectIndex], ComponentIndex1));
        }

        FillSectionLines();
    }

    public List<Component> ComponentList1 { get; }
}

namespace PgBrewer;

using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

public class Alcoholx4x4 : Alcohol
{
    public static async Task<Alcoholx4x4> Create(string name, List<Component> componentList1, List<Component> componentList2)
    {
        Debug.Assert(componentList1.Count == 4);
        Debug.Assert(componentList2.Count == 4);

        Alcoholx4x4 Instance = new(name, componentList1, componentList2);
        await Instance.Init();

        return Instance;
    }

    private Alcoholx4x4(string name, List<Component> componentList1, List<Component> componentList2)
        : base(name)
    {
        ComponentList1 = componentList1;
        ComponentList2 = componentList2;

        Multiplier1 = 4;
    }

    private async Task Init()
    {
        List<int> Indexes = await DataArchive.GetIndexList(Name, ComponentList1.Count * ComponentList2.Count);

        for (int ComponentIndex1 = 0; ComponentIndex1 < ComponentList1.Count; ComponentIndex1++)
            for (int ComponentIndex2 = 0; ComponentIndex2 < ComponentList2.Count; ComponentIndex2++)
            {
                int EffectIndex = (ComponentIndex1 * Multiplier1) + ComponentIndex2;
                Lines.Add(new Alcoholx4x4Line(this, Indexes[EffectIndex], ComponentIndex1, ComponentIndex2));
            }
    }

    public List<Component> ComponentList1 { get; }
    public List<Component> ComponentList2 { get; }
    public int Multiplier1 { get; }
}

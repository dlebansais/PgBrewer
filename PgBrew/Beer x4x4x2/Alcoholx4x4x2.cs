namespace PgBrew
{
    using System.Collections.Generic;
    using System.Diagnostics;

    public class Alcoholx4x4x2 : Alcohol
    {
        public Alcoholx4x4x2(string name, List<Component> componentList1, List<Component> componentList2, List<Component> componentList3)
            : base(name)
        {
            Debug.Assert(componentList1.Count == 4);
            Debug.Assert(componentList2.Count == 4);
            Debug.Assert(componentList3.Count == 2);

            ComponentList1 = componentList1;
            ComponentList2 = componentList2;
            ComponentList3 = componentList3;

            List<int> Indexes = DataArchive.GetIndexList(Name, ComponentList1.Count * ComponentList2.Count * ComponentList3.Count);
            int EffectIndex = 0;

            for (int ComponentIndex1 = 0; ComponentIndex1 < ComponentList1.Count; ComponentIndex1++)
                for (int ComponentIndex2 = 0; ComponentIndex2 < ComponentList2.Count; ComponentIndex2++)
                    for (int ComponentIndex3 = 0; ComponentIndex3 < ComponentList3.Count; ComponentIndex3++)
                    {
                        Lines.Add(new Alcoholx4x4x2Line(this, Indexes[EffectIndex++], ComponentIndex1, ComponentIndex2, ComponentIndex3));
                    }
        }

        public List<Component> ComponentList1 { get; }
        public List<Component> ComponentList2 { get; }
        public List<Component> ComponentList3 { get; }
    }
}

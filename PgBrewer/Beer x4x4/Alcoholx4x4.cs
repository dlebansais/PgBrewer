namespace PgBrewer
{
    using System.Collections.Generic;
    using System.Diagnostics;

    public class Alcoholx4x4 : Alcohol
    {
        public Alcoholx4x4(string name, List<Component> componentList1, List<Component> componentList2)
            : base(name)
        {
            Debug.Assert(componentList1.Count == 4);
            Debug.Assert(componentList2.Count == 4);

            ComponentList1 = componentList1;
            ComponentList2 = componentList2;

            Multiplier1 = 4;

            List<int> Indexes = DataArchive.GetIndexList(Name, ComponentList1.Count * ComponentList2.Count);
            int EffectIndex = 0;

            for (int ComponentIndex1 = 0; ComponentIndex1 < ComponentList1.Count; ComponentIndex1++)
                for (int ComponentIndex2 = 0; ComponentIndex2 < ComponentList2.Count; ComponentIndex2++)
                {
                    EffectIndex = (ComponentIndex1 * Multiplier1) + ComponentIndex2;
                    Lines.Add(new Alcoholx4x4Line(this, Indexes[EffectIndex], ComponentIndex1, ComponentIndex2));
                }
        }

        public List<Component> ComponentList1 { get; }
        public List<Component> ComponentList2 { get; }
        public int Multiplier1 { get; }
    }
}

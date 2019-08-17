namespace PgBrew
{
    using System.Collections.Generic;
    using System.Diagnostics;

    public class Alcoholx3x4x5x4 : Alcohol, IFourComponentsAlcohol
    {
        public Alcoholx3x4x5x4(string name, List<Component> componentList1, List<Component> componentList2, List<Component> componentList3, List<Component> componentList4)
            : base(name)
        {
            Debug.Assert(componentList1.Count == 3);
            Debug.Assert(componentList2.Count == 4);
            Debug.Assert(componentList3.Count == 5);
            Debug.Assert(componentList4.Count == 4);

            ComponentList1 = componentList1;
            ComponentList2 = componentList2;
            ComponentList3 = componentList3;
            ComponentList4 = componentList4;

            Multiplier1 = 4 * 5 * 4;
            Multiplier2 = 5 * 4;
            Multiplier3 = 4;

            List<int> Indexes = DataArchive.GetIndexList(Name, ComponentList1.Count * ComponentList2.Count * ComponentList3.Count * ComponentList4.Count);
            int EffectIndex = 0;

            for (int ComponentIndex1 = 0; ComponentIndex1 < ComponentList1.Count; ComponentIndex1++)
                for (int ComponentIndex2 = 0; ComponentIndex2 < ComponentList2.Count; ComponentIndex2++)
                    for (int ComponentIndex3 = 0; ComponentIndex3 < ComponentList3.Count; ComponentIndex3++)
                        for (int ComponentIndex4 = 0; ComponentIndex4 < ComponentList4.Count; ComponentIndex4++)
                        {
                            EffectIndex = (ComponentIndex1 * Multiplier1) + (ComponentIndex2 * Multiplier2) + (ComponentIndex3 * Multiplier3) + ComponentIndex4;
                            Lines.Add(new Alcoholx3x4x5x4Line(this, Indexes[EffectIndex], ComponentIndex1, ComponentIndex2, ComponentIndex3, ComponentIndex4));
                        }
        }

        public List<Component> ComponentList1 { get; }
        public List<Component> ComponentList2 { get; }
        public List<Component> ComponentList3 { get; }
        public List<Component> ComponentList4 { get; }
        public int Multiplier1 { get; }
        public int Multiplier2 { get; }
        public int Multiplier3 { get; }
    }
}

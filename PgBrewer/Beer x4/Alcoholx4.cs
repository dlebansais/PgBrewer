namespace PgBrewer
{
    using System.Collections.Generic;
    using System.Diagnostics;

    public class Alcoholx4 : Alcohol
    {
        public Alcoholx4(string name, List<Component> componentList1)
            : base(name)
        {
            Debug.Assert(componentList1.Count == 4);

            ComponentList1 = componentList1;

            List<int> Indexes = DataArchive.GetIndexList(Name, ComponentList1.Count);
            int EffectIndex = 0;

            for (int ComponentIndex1 = 0; ComponentIndex1 < ComponentList1.Count; ComponentIndex1++)
            {
                EffectIndex = ComponentIndex1;
                Lines.Add(new Alcoholx4Line(this, Indexes[EffectIndex], ComponentIndex1));
            }
        }

        public List<Component> ComponentList1 { get; }
    }
}

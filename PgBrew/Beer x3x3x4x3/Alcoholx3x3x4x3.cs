namespace PgBrew
{
    using System.Collections.Generic;
    using System.Diagnostics;

    public class Alcoholx3x3x4x3
    {
        public Alcoholx3x3x4x3(string name, List<string> componentList1, List<string> componentList2, List<string> componentList3, List<string> componentList4)
        {
            Debug.Assert(componentList1.Count == 3);
            Debug.Assert(componentList2.Count == 3);
            Debug.Assert(componentList3.Count == 4);
            Debug.Assert(componentList4.Count == 3);

            Name = name;
            ComponentList1 = componentList1;
            ComponentList2 = componentList2;
            ComponentList3 = componentList3;
            ComponentList4 = componentList4;
            EffectList = DataArchive.ReadEffectList(name);

            List<int> Indexes = DataArchive.GetIndexList(Name, ComponentList1.Count * ComponentList2.Count * ComponentList3.Count * ComponentList4.Count);
            int EffectIndex = 0;

            for (int ComponentIndex1 = 0; ComponentIndex1 < ComponentList1.Count; ComponentIndex1++)
                for (int ComponentIndex2 = 0; ComponentIndex2 < ComponentList2.Count; ComponentIndex2++)
                    for (int ComponentIndex3 = 0; ComponentIndex3 < ComponentList3.Count; ComponentIndex3++)
                        for (int ComponentIndex4 = 0; ComponentIndex4 < ComponentList4.Count; ComponentIndex4++)
                        {
                            Lines.Add(new Alcoholx3x3x4x3Line(this, ComponentIndex1, ComponentIndex2, ComponentIndex3, ComponentIndex4, Indexes[EffectIndex++]));
                        }
        }

        public string Name { get; }
        public List<string> ComponentList1 { get; }
        public List<string> ComponentList2 { get; }
        public List<string> ComponentList3 { get; }
        public List<string> ComponentList4 { get; }
        public List<string> EffectList { get; } = new List<string>();

        public List<Alcoholx3x3x4x3Line> Lines { get; } = new List<Alcoholx3x3x4x3Line>();

        public void Save()
        {
            List<int> Indexes = new List<int>();

            for (int i = 0; i < Lines.Count; i++)
                Indexes.Add(Lines[i].EffectIndex);

            DataArchive.SetIndexList(Name, Indexes);
        }
    }
}

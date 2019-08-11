namespace PgBrew
{
    using System.Collections.Generic;
    using System.Diagnostics;

    public class Alcoholx4x4
    {
        public Alcoholx4x4(string name, List<string> componentList1, List<string> componentList2)
        {
            Name = name;
            ComponentList1 = componentList1;
            ComponentList2 = componentList2;
            EffectList = DataArchive.ReadEffectList(name);

            List<int> Indexes = DataArchive.GetIndexList(Name, ComponentList1.Count * ComponentList2.Count);
            int EffectIndex = 0;

            for (int ComponentIndex1 = 0; ComponentIndex1 < ComponentList1.Count; ComponentIndex1++)
                for (int ComponentIndex2 = 0; ComponentIndex2 < ComponentList2.Count; ComponentIndex2++)
                {
                    Lines.Add(new Alcoholx4x4Line(this, ComponentIndex1, ComponentIndex2, Indexes[EffectIndex++]));
                }
        }

        public string Name { get; }
        public List<string> ComponentList1 { get; }
        public List<string> ComponentList2 { get; }
        public List<string> EffectList { get; } = new List<string>();

        public List<Alcoholx4x4Line> Lines { get; } = new List<Alcoholx4x4Line>();

        public void Save()
        {
            List<int> Indexes = new List<int>();

            for (int i = 0; i < Lines.Count; i++)
                Indexes.Add(Lines[i].EffectIndex);

            DataArchive.SetIndexList(Name, Indexes);
        }
    }
}

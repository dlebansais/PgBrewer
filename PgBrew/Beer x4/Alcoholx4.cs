namespace PgBrew
{
    using System.Collections.Generic;
    using System.Diagnostics;

    public class Alcoholx4
    {
        public Alcoholx4(string name, List<string> componentList1)
        {
            Name = name;
            ComponentList1 = componentList1;
            EffectList = DataArchive.ReadEffectList(name);

            List<int> Indexes = DataArchive.GetIndexList(Name, ComponentList1.Count);
            int EffectIndex = 0;

            for (int ComponentIndex1 = 0; ComponentIndex1 < ComponentList1.Count; ComponentIndex1++)
            {
                Lines.Add(new Alcoholx4Line(this, ComponentIndex1, Indexes[EffectIndex++]));
            }
        }

        public string Name { get; }
        public List<string> ComponentList1 { get; }
        public List<string> EffectList { get; } = new List<string>();

        public List<Alcoholx4Line> Lines { get; } = new List<Alcoholx4Line>();

        public void Save()
        {
            List<int> Indexes = new List<int>();

            for (int i = 0; i < Lines.Count; i++)
                Indexes.Add(Lines[i].EffectIndex);

            DataArchive.SetIndexList(Name, Indexes);
        }
    }
}

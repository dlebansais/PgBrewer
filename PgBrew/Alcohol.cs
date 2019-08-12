namespace PgBrew
{
    using System.Collections.Generic;

    public class Alcohol
    {
        public Alcohol(string name)
        {
            Name = name;
            EffectList = DataArchive.ReadEffectList(name);
        }

        public string Name { get; }
        public List<string> EffectList { get; } = new List<string>();
        public AlcoholLineCollection Lines { get; } = new AlcoholLineCollection();

        public void Save()
        {
            List<int> Indexes = new List<int>();

            for (int i = 0; i < Lines.Count; i++)
                Indexes.Add(Lines[i].EffectIndex);

            DataArchive.SetIndexList(Name, Indexes);
        }
    }
}

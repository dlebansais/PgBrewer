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
        public List<Effect> EffectList { get; } = new List<Effect>();
        public AlcoholLineCollection Lines { get; } = new AlcoholLineCollection();

        public void Save()
        {
            List<int> Indexes = new List<int>();

            for (int i = 0; i < Lines.Count; i++)
                Indexes.Add(Lines[i].EffectIndex);

            DataArchive.SetIndexList(Name, Indexes);
        }

        public void ClearCalculateIndexes()
        {
            foreach (AlcoholLine Line in Lines)
                Line.ClearCalculateIndex();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}

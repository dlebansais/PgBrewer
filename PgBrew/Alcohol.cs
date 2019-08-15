namespace PgBrew
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class Alcohol : INotifyPropertyChanged
    {
        public Alcohol(string name)
        {
            Name = name;
            EffectList = DataArchive.ReadEffectList(name);
        }

        public string Name { get; }
        public List<Effect> EffectList { get; } = new List<Effect>();
        public AlcoholLineCollection Lines { get; } = new AlcoholLineCollection();

        public int MismatchCount
        {
            get { return _MismatchCount; }
            set
            {
                if (_MismatchCount != value)
                {
                    _MismatchCount = value;
                    NotifyThisPropertyChanged();
                }
            }
        }
        private int _MismatchCount;

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

        public void RecalculateMismatchCount()
        {
            int NewCount = 0;

            foreach (AlcoholLine Line in Lines)
                if (!Line.IsMatching)
                    NewCount++;

            MismatchCount = NewCount;
        }

        public void Export(StreamWriter writer, bool isCalculatedIncluded)
        {
            writer.WriteLine(Name);

            foreach (AlcoholLine Line in Lines)
            {
                string ExportedComponents = Line.GetExportedComponents();
                int Index = isCalculatedIncluded ? Line.BestIndex : Line.EffectIndex;

                if (Index >= 0)
                    writer.WriteLine($"{ExportedComponents};{EffectList[Index].Text}");
                else
                    writer.WriteLine($"{ExportedComponents};");
            }

            writer.WriteLine();
        }

        #region Implementation of INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        public void NotifyThisPropertyChanged([CallerMemberName] string PropertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
        #endregion

        public override string ToString()
        {
            return Name;
        }
    }
}

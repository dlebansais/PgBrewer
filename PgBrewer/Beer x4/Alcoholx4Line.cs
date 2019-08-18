namespace PgBrewer
{
    using System.ComponentModel;

    public class Alcoholx4Line : AlcoholLine, INotifyPropertyChanged
    {
        public Alcoholx4Line(Alcoholx4 owner, int effectIndex, int index1)
            : base(owner, effectIndex)
        {
            Index1 = index1;
        }

        public int Index1 { get; }

        public string Component1 { get { return ((Alcoholx4)Owner).ComponentList1[Index1].Name; } }

        public override string GetExportedComponents()
        {
            return $"{Component1};;;";
        }

        public override string ToString()
        {
            return $"{Component1}";
        }
    }
}

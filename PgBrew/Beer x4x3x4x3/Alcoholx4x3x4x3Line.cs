namespace PgBrew
{
    using System.ComponentModel;

    public class Alcoholx4x3x4x3Line : AlcoholLine, INotifyPropertyChanged
    {
        public Alcoholx4x3x4x3Line(Alcoholx4x3x4x3 owner, int effectIndex, int index1, int index2, int index3, int index4)
            : base(owner, effectIndex)
        {
            Index1 = index1;
            Index2 = index2;
            Index3 = index3;
            Index4 = index4;
        }

        public int Index1 { get; }
        public int Index2 { get; }
        public int Index3 { get; }
        public int Index4 { get; }

        public string Component1 { get { return ((Alcoholx4x3x4x3)Owner).ComponentList1[Index1].Name; } }
        public string Component2 { get { return ((Alcoholx4x3x4x3)Owner).ComponentList2[Index2].Name; } }
        public string Component3 { get { return ((Alcoholx4x3x4x3)Owner).ComponentList3[Index3].Name; } }
        public string Component4 { get { return ((Alcoholx4x3x4x3)Owner).ComponentList3[Index4].Name; } }
    }
}

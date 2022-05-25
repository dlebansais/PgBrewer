namespace PgBrewer;

using System.ComponentModel;

public class Alcoholx4x4x2Line : AlcoholLine, INotifyPropertyChanged
{
    public Alcoholx4x4x2Line(Alcoholx4x4x2 owner, int effectIndex, int index1, int index2, int index3)
        : base(owner, effectIndex)
    {
        Index1 = index1;
        Index2 = index2;
        Index3 = index3;
    }

    public int Index1 { get; }
    public int Index2 { get; }
    public int Index3 { get; }

    public string Component1 { get { return ((Alcoholx4x4x2)Owner).ComponentList1[Index1].Name; } }
    public string Component2 { get { return ((Alcoholx4x4x2)Owner).ComponentList2[Index2].Name; } }
    public string Component3 { get { return ((Alcoholx4x4x2)Owner).ComponentList3[Index3].Name; } }

    public override string GetExportedComponents()
    {
        return $"{Component1};{Component2};{Component3};";
    }

    public override string ToString()
    {
        return $"{Component1}, {Component2}, {Component3}";
    }
}

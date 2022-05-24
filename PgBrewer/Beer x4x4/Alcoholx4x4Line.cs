namespace PgBrewer;

using System.ComponentModel;

public class Alcoholx4x4Line : AlcoholLine, INotifyPropertyChanged
{
    public Alcoholx4x4Line(Alcoholx4x4 owner, int effectIndex, int index1, int index2)
        : base(owner, effectIndex)
    {
        Index1 = index1;
        Index2 = index2;
    }

    public int Index1 { get; }
    public int Index2 { get; }

    public string Component1 { get { return ((Alcoholx4x4)Owner).ComponentList1[Index1].Name; } }
    public string Component2 { get { return ((Alcoholx4x4)Owner).ComponentList2[Index2].Name; } }

    public override string GetExportedComponents()
    {
        return $"{Component1};{Component2};;";
    }

    public override string ToString()
    {
        return $"{Component1}, {Component2}";
    }
}

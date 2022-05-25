namespace PgBrewer;

public interface IFourComponentsAlcoholLine
{
    int Index1 { get; }
    int Index2 { get; }
    int Index3 { get; }
    int Index4 { get; }
    string Component1 { get; }
    string Component2 { get; }
    string Component3 { get; }
    string Component4 { get; }
    int BestIndex { get; }
}

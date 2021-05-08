namespace PgBrewer
{
    using System.Collections.Generic;

    public interface IFourComponentsAlcohol
    {
        List<Component> ComponentList1 { get; }
        List<Component> ComponentList2 { get; }
        List<Component> ComponentList3 { get; }
        List<Component> ComponentList4 { get; }
        int Multiplier1 { get; }
        int Multiplier2 { get; }
        int Multiplier3 { get; }
        AlcoholLineCollection Lines { get; }
    }
}

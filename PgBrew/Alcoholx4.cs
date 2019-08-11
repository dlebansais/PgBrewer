namespace PgBrew
{
    using System.Collections.Generic;
    using System.Diagnostics;

    public class Alcoholx4
    {
        static Alcoholx4()
        {
            List<string> BasicLagerTable = DataArchive.BasicLagerTable;

            foreach (string Effect in BasicLagerTable)
                EffectList.Add(Effect);

            Debug.Assert(ComponentList1.Count == EffectList.Count);
        }

        public static List<string> ComponentList1 = new List<string>()
        {
            "Red Apple",
            "Grapes",
            "Orange",
            "Strawberry",
        };

        public static List<string> EffectList { get; } = new List<string>();

        public Alcoholx4()
        {
            int EffectIndex = 0;

            for (int ComponentIndex1 = 0; ComponentIndex1 < ComponentList1.Count; ComponentIndex1++)
            {
                Lines.Add(new Alcoholx4Line(this, ComponentIndex1, EffectIndex++));
            }
        }

        public List<Alcoholx4Line> Lines { get; } = new List<Alcoholx4Line>();
    }
}

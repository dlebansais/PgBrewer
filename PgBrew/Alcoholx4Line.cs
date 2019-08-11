namespace PgBrew
{
    public class Alcoholx4Line
    {
        public Alcoholx4Line(Alcoholx4 owner, int index1, int effectIndex)
        {
            Owner = owner;
            Index1 = index1;
            EffectIndex = effectIndex;
        }

        public Alcoholx4 Owner { get; }
        public int Index1 { get; }
        public int EffectIndex { get; set; }

        public string Component1 { get { return Alcoholx4.ComponentList1[Index1]; } }
        public string Effect { get { return Alcoholx4.EffectList[EffectIndex]; } }
    }
}

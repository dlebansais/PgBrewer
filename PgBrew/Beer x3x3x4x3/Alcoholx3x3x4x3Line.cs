namespace PgBrew
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class Alcoholx3x3x4x3Line : INotifyPropertyChanged
    {
        public Alcoholx3x3x4x3Line(Alcoholx3x3x4x3 owner, int index1, int index2, int index3, int index4, int effectIndex)
        {
            Owner = owner;
            Index1 = index1;
            Index2 = index2;
            Index3 = index3;
            Index4 = index4;
            _EffectIndex = effectIndex;
        }

        public Alcoholx3x3x4x3 Owner { get; }
        public int Index1 { get; }
        public int Index2 { get; }
        public int Index3 { get; }
        public int Index4 { get; }
        public int EffectIndex
        {
            get { return _EffectIndex; }
            set
            {
                if (_EffectIndex != value)
                {
                    _EffectIndex = value;
                    MainWindow.IsChanged = true;

                    NotifyThisPropertyChanged();
                }
            }
        }
        private int _EffectIndex;

        public string Component1 { get { return Owner.ComponentList1[Index1]; } }
        public string Component2 { get { return Owner.ComponentList2[Index2]; } }
        public string Component3 { get { return Owner.ComponentList3[Index3]; } }
        public string Component4 { get { return Owner.ComponentList3[Index4]; } }
        public string Effect { get { return EffectIndex >= 0 && EffectIndex < Owner.EffectList.Count ? Owner.EffectList[EffectIndex] : null; } }

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
    }
}

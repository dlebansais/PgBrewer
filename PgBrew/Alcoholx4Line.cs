namespace PgBrew
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class Alcoholx4Line : INotifyPropertyChanged
    {
        public Alcoholx4Line(Alcoholx4 owner, int index1, int effectIndex)
        {
            Owner = owner;
            Index1 = index1;
            _EffectIndex = effectIndex;
        }

        public Alcoholx4 Owner { get; }
        public int Index1 { get; }
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
        public string Effect { get { return EffectIndex >= 0 ? Owner.EffectList[EffectIndex] : null; } }

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

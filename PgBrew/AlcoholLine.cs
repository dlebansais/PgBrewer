namespace PgBrew
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class AlcoholLine : INotifyPropertyChanged
    {
        public AlcoholLine(Alcohol owner, int effectIndex)
        {
            Owner = owner;
            _EffectIndex = effectIndex;
        }

        public Alcohol Owner { get; }

        public int EffectIndex
        {
            get { return _EffectIndex; }
            set
            {
                if (_EffectIndex != value)
                {
                    _EffectIndex = value;
                    MainWindow.SetChanged();

                    NotifyThisPropertyChanged();
                }
            }
        }
        private int _EffectIndex;

        public string Effect { get { return EffectIndex >= 0 && EffectIndex < Owner.EffectList.Count ? Owner.EffectList[EffectIndex] : null; } }

        public int CalculatedIndex { get; set; }
        public string CalculatedEffect
        {
            get
            {
                if (EffectIndex >= 0)
                    return null;

                return CalculatedIndex >= 0 && CalculatedIndex < Owner.EffectList.Count ? Owner.EffectList[CalculatedIndex] : null;
            }
        }

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

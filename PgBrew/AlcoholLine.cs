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
                    MainWindow.IsChanged = true;

                    NotifyThisPropertyChanged();
                }
            }
        }
        private int _EffectIndex;

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

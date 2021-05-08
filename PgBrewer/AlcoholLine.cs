namespace PgBrewer
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public abstract class AlcoholLine : INotifyPropertyChanged
    {
        #region Init
        public AlcoholLine(Alcohol owner, int effectIndex)
        {
            Owner = owner;
            EffectIndexInternal = effectIndex;
            CalculatedIndexInternal = -1;
        }
        #endregion

        #region Properties
        public Alcohol Owner { get; }

        public int EffectIndex
        {
            get => EffectIndexInternal;
            set
            {
                if (EffectIndexInternal != value)
                {
                    EffectIndexInternal = value;
                    MainWindow.SetChanged();

                    NotifyThisPropertyChanged();

                    MainWindow Window = (MainWindow)App.Current.MainWindow;
                    Window.Recalculate();
                }
            }
        }

        private int EffectIndexInternal;

        public Effect? Effect { get { return EffectIndex >= 0 && EffectIndex < Owner.EffectList.Count ? Owner.EffectList[EffectIndex] : null; } }

        public int CalculatedIndex
        {
            get => CalculatedIndexInternal;
            set
            {
                if (CalculatedIndexInternal != value)
                {
                    CalculatedIndexInternal = value;
                    NotifyPropertyChanged(nameof(CalculatedEffect));
                    NotifyPropertyChanged(nameof(IsMatching));
                }
            }
        }

        private int CalculatedIndexInternal;

        public Effect? CalculatedEffect
        {
            get
            {
                if (EffectIndex >= 0)
                    return null;

                return CalculatedIndexInternal >= 0 && CalculatedIndexInternal < Owner.EffectList.Count ? Owner.EffectList[CalculatedIndexInternal] : null;
            }
        }

        public int BestIndex { get { return EffectIndexInternal >= 0 ? EffectIndexInternal : CalculatedIndexInternal >= 0 ? CalculatedIndexInternal : -1; } }

        public bool IsMatching { get { return EffectIndexInternal < 0 || CalculatedIndexInternal < 0 || EffectIndexInternal == CalculatedIndexInternal; } }
        #endregion

        #region Client Interface
        public void ClearCalculateIndex()
        {
            CalculatedIndex = -1;
        }

        public abstract string GetExportedComponents();
        #endregion

        #region Implementation of INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void NotifyThisPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}

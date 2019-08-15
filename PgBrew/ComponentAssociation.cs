namespace PgBrew
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class ComponentAssociation : INotifyPropertyChanged
    {
        #region Init
        public ComponentAssociation(Component component, List<Component> choiceList)
        {
            Component = component;
            ChoiceList = choiceList;

            Debug.Assert(ChoiceList.Count == 0 || ChoiceList.Count >= 2);

            _AssociationIndex = -1;
        }
        #endregion

        #region Properties
        public Component Component { get; }
        public List<Component> ChoiceList { get; }

        public int AssociationIndex
        {
            get { return _AssociationIndex; }
            set
            {
                if (_AssociationIndex != value)
                {
                    _AssociationIndex = value;
                    MainWindow.SetChanged();

                    NotifyThisPropertyChanged();
                }
            }
        }
        private int _AssociationIndex;
        #endregion

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

        #region Debugging
        public override string ToString()
        {
            return _AssociationIndex >= 0 ? $"{Component} -> {ChoiceList[_AssociationIndex]}" : $"{Component} -> Unknown";
        }
        #endregion
    }
}

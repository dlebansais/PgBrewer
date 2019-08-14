namespace PgBrew
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class ComponentAssociation : INotifyPropertyChanged
    {
        public ComponentAssociation(Component component, List<Component> choiceList)
        {
            Component = component;
            ChoiceList = choiceList;

            Debug.Assert(ChoiceList.Count == 0 || ChoiceList.Count >= 2);

            _AssociationIndex = -1;
        }

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

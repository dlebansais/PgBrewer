namespace PgBrewer
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

            AssociationIndexInternal = -1;
        }
        #endregion

        #region Properties
        public Component Component { get; }
        public List<Component> ChoiceList { get; }

        public int AssociationIndex
        {
            get => AssociationIndexInternal;
            set
            {
                if (AssociationIndexInternal != value)
                {
                    AssociationIndexInternal = value;
                    MainWindow.SetChanged();

                    NotifyThisPropertyChanged();
                }
            }
        }

        private int AssociationIndexInternal;
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

        #region Debugging
        public override string ToString()
        {
            return AssociationIndexInternal >= 0 ? $"{Component} -> {ChoiceList[AssociationIndexInternal]}" : $"{Component} -> Unknown";
        }
        #endregion
    }
}

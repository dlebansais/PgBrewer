namespace PgBrewer
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public partial class Alcoholx4x3x4x3Control : UserControl, INotifyPropertyChanged
    {
        public Alcoholx4x3x4x3Control()
        {
            InitializeComponent();
        }

        private void OnDelete(object sender, RoutedEventArgs e)
        {
            Alcoholx4x3x4x3Line Line = (sender as Button).DataContext as Alcoholx4x3x4x3Line;
            Line.EffectIndex = -1;
        }

        private void OnGotFocus(object sender, RoutedEventArgs e)
        {
            (App.Current.MainWindow as MainWindow).OnGotFocus(sender as ComboBox);
        }

        private void OnLostFocus(object sender, RoutedEventArgs e)
        {
            (App.Current.MainWindow as MainWindow).OnLostFocus(sender as ComboBox);
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

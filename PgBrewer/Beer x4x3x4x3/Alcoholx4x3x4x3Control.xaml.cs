namespace PgBrewer
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Threading;

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

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement Ctrl = sender as FrameworkElement;
            if (Tools.FindFirstControl(Ctrl, out ComboBox FirstComboBox))
                Dispatcher.BeginInvoke(DispatcherPriority.Normal, new LineClickedHandler(OnLineClicked), FirstComboBox);
        }

        private bool FindFirstComboBox(FrameworkElement ctrl, out ComboBox firstComboBox)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(ctrl); i++)
                if (VisualTreeHelper.GetChild(ctrl, i) is FrameworkElement Child)
                    if (Child is ComboBox AsComboBox)
                    {
                        firstComboBox = AsComboBox;
                        return true;
                    }
                    else if (FindFirstComboBox(Child, out firstComboBox))
                        return true;

            firstComboBox = null;
            return false;
        }

        private delegate void LineClickedHandler(ComboBox firstComboBox);
        private void OnLineClicked(ComboBox firstComboBox)
        {
            firstComboBox.Focus();
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

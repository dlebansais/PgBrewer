namespace PgBrewer
{
    using System.Windows;
    using System.Windows.Controls;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;
    using System.Windows.Threading;
    using System.Windows.Media;

    public class AlcoholControl : UserControl, INotifyPropertyChanged
    {
        #region Events
        protected void OnDelete(object sender, RoutedEventArgs e)
        {
            AlcoholLine Line = (sender as Button).DataContext as AlcoholLine;
            Line.EffectIndex = -1;
        }

        protected void OnGotFocus(object sender, RoutedEventArgs e)
        {
            (App.Current.MainWindow as MainWindow).OnGotFocus(sender as ComboBox);
        }

        protected void OnLostFocus(object sender, RoutedEventArgs e)
        {
            (App.Current.MainWindow as MainWindow).OnLostFocus(sender as ComboBox);
        }

        protected void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement Ctrl = sender as FrameworkElement;
            if (Tools.FindFirstControl(Ctrl, out ComboBox FirstComboBox))
                Dispatcher.BeginInvoke(DispatcherPriority.Normal, new LineClickedHandler(OnLineClicked), FirstComboBox);
        }

        protected bool FindFirstComboBox(FrameworkElement ctrl, out ComboBox firstComboBox)
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

        protected delegate void LineClickedHandler(ComboBox firstComboBox);
        protected void OnLineClicked(ComboBox firstComboBox)
        {
            firstComboBox.Focus();
        }
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
    }
}

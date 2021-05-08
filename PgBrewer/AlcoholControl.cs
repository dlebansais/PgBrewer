namespace PgBrewer
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Threading;
    using Contracts;

    public class AlcoholControl : UserControl, INotifyPropertyChanged
    {
        #region Events
        protected void OnDelete(object sender, RoutedEventArgs e)
        {
            AlcoholLine Line = (AlcoholLine)((Button)sender).DataContext;
            Line.EffectIndex = -1;
        }

        protected void OnGotFocus(object sender, RoutedEventArgs e)
        {
            ((MainWindow)App.Current.MainWindow).OnGotFocus((ComboBox)sender);
        }

        protected void OnLostFocus(object sender, RoutedEventArgs e)
        {
            ((MainWindow)App.Current.MainWindow).OnLostFocus((ComboBox)sender);
        }

        protected void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement Ctrl = (FrameworkElement)sender;
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

            Contract.Unused(out firstComboBox);
            return false;
        }

        protected delegate void LineClickedHandler(ComboBox firstComboBox);
        protected void OnLineClicked(ComboBox firstComboBox)
        {
            firstComboBox.Focus();
        }
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

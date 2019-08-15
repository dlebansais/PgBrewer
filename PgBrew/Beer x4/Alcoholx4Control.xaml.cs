﻿namespace PgBrew
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public partial class Alcoholx4Control : UserControl, INotifyPropertyChanged
    {
        public Alcoholx4Control()
        {
            InitializeComponent();
        }

        private void OnDelete(object sender, RoutedEventArgs e)
        {
            Alcoholx4Line Line = (sender as Button).DataContext as Alcoholx4Line;
            Line.EffectIndex = -1;
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

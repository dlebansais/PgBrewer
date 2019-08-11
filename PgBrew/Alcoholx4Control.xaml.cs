namespace PgBrew
{
    using System.Windows;
    using System.Windows.Controls;

    public partial class Alcoholx4Control : UserControl
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
    }
}

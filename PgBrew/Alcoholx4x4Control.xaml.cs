namespace PgBrew
{
    using System.Windows;
    using System.Windows.Controls;

    public partial class Alcoholx4x4Control : UserControl
    {
        public Alcoholx4x4Control()
        {
            InitializeComponent();
        }

        private void OnDelete(object sender, RoutedEventArgs e)
        {
            Alcoholx4x4Line Line = (sender as Button).DataContext as Alcoholx4x4Line;
            Line.EffectIndex = -1;
        }
    }
}

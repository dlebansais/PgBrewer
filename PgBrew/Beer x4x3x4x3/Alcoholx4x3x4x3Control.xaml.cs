namespace PgBrew
{
    using System.Windows;
    using System.Windows.Controls;

    public partial class Alcoholx4x3x4x3Control : UserControl
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
    }
}

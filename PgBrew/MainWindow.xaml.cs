namespace PgBrew
{
    using System.Windows;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        public Alcoholx4 BasicLager { get; private set; } = new Alcoholx4();
    }
}

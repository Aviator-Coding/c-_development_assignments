using System.Windows;

namespace EinsendeAufgaben_03
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ActionDeleteAll(object sender, RoutedEventArgs e)
        {
            tbContent.Clear();
            tbTitle.Clear();
            lblStatus.Content = $"Status: Alles Geloescht";
        }

        private void ActionBeenden(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }
    }
}

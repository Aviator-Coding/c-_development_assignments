using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Aufgabe_29_Datenbank
{
    /// <summary>
    /// Interaction logic for LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : Window
    {
        public LoginScreen()
        {
            InitializeComponent();
        }

        private void BtnClose(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void LClearUsername(object sender, RoutedEventArgs e)
        {
            LUsername.Text = "";
        }

        private void LClearPassword(object sender, RoutedEventArgs e)
        {
            
        }

        private void BtnLoginClick(object sender, RoutedEventArgs e)
        {
            if (LUsername.Text != "Demo")
            {
                MessageBox.Show("Der Benutzername ist uns nicht bekannt");
                return;
            }

            if (PPassowrd.Password.ToString() != "Demo")
            {
                MessageBox.Show("Die Login Daten sind nicht korrekt");
                return;
            }

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            mainWindow.Dispatcher.ShutdownFinished += mainWindow.ShutDownEvent;
            this.Close();
        }
    }
}

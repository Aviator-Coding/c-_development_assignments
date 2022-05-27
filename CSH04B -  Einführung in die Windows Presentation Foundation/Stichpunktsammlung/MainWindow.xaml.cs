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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Stichpunktsammlung
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

        private void uebernehmen_Click(object sender, RoutedEventArgs e)
        {
            listBox.Items.Add(textBox.Text);
        }

        private void moveUp_Click(object sender, RoutedEventArgs e)
        {
            if (listBox.SelectedIndex == -1)
            {
                MessageBox.Show("Es wurde Kein Stichpunkt ausgewaehlt verschieben ist dadurch nicht moeglich.", 
                                "Bitte einen Stichounkt auswaehlen", 
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
                return;
            }
            int index = listBox.SelectedIndex;
            if (index == 0)
                index = listBox.Items.Count - 1;
            else index--;

            Verschieben(index);
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            if(listBox.SelectedIndex == -1)
            {
                MessageBox.Show("Es wurde Kein Stichpunkt ausgewaehlt loeschen ist dadurch nicht moeglich.",
                "Bitte einen Stichounkt auswaehlen",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
                return;
            }
            listBox.Items.RemoveAt(listBox.SelectedIndex);
        }

        private void moveDown_Click(object sender, RoutedEventArgs e)
        {
            if (listBox.SelectedIndex == -1)
            {
                MessageBox.Show("Es wurde Kein Stichpunkt ausgewaehlt verschieben ist dadurch nicht moeglich.",
                                "Bitte einen Stichounkt auswaehlen",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
                return;
            }
            int index = listBox.SelectedIndex;
            if (index == listBox.Items.Count - 1)
                index = 0;
            else index++;
            Verschieben(index);
        }

        private void Verschieben(int index)
        {
            Object item = listBox.SelectedItem;
            listBox.Items.Remove(item);
            listBox.Items.Insert(index, item);
            listBox.SelectedIndex = index;
        }

        private void beenden_Click(object sender, RoutedEventArgs e)
        {
            Close();
            //Application.Current.Shutdown();
        }
    }
}

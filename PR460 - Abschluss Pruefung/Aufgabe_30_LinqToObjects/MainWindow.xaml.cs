using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Odbc;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Documents.DocumentStructures;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Aufgabe_30_LinqToObjects.DataModel;


namespace Aufgabe_30_LinqToObjects
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            foreach (string table in Settings.DbTables)
            {
                dTable.Items.Add(table);
            }
            dTable.SelectedIndex = 0;
            SelectFrame();


            MysqlConnection.OdbcConnection.Open();

        }

        private void SelectFrame()
        {
            FrameData.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            switch (dTable.SelectedIndex)
            {
                case 0:
                    FrameData.Content = new PageAbteilung();
                    break;
                case 1:
                    FrameData.Content = new PageAngestellter();
                    break;
                case 2:
                    FrameData.Content = new PageArtikel();
                    break;
                //case 3:
                //    FrameData.Content = new PageBestellposition();
               //     break;
               // case 4:
               //     FrameData.Content = new PageBestellung();
               //     break;
                case 3:
                    FrameData.Content = new PageKontakt();
                    break;
                case 4:
                    FrameData.Content = new PageKunde();
                    break;
                case 5:
                    FrameData.Content = new PageLieferant();
                    break;
                case 6:
                    FrameData.Content = new PagePerson();
                    break;
            }
        }

        private void click_btnAktiv(object sender, RoutedEventArgs e)
        {
            SelectFrame();
        }


    }


}

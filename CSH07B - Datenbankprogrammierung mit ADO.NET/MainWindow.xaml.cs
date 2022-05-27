using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
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

namespace Lektion3_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        OdbcConnection odbcConnection;
        public MainWindow()
        {
            InitializeComponent();
            odbcConnection = new OdbcConnection("DRIVER={MySQL ODBC 8.0 ANSI Driver};SERVER=localhost;PORT=3306;DATABASE=dbdemo2;UID=demo-user");
            PersonenAbrufen();
        }

        private void PersonenAbrufen()
        {
            string sqlPersonen = "SELECT * FROM personen";
            DataSet dataSet = new DataSet();
            try
            {
                OdbcDataAdapter odbcDataAdapter = new OdbcDataAdapter(sqlPersonen, odbcConnection);
                odbcDataAdapter.Fill(dataSet, "personenTable");
                cbPersonen.DisplayMemberPath = "nachname";
                cbPersonen.SelectedValuePath = "id";
                cbPersonen.ItemsSource = dataSet.Tables["personenTable"].DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.GetType()}{Environment.NewLine} {ex.Message}, Datenbankfehler");
            }
        }

        private void BestellungenShow()
        {
            string sqlBestellungen = "SELECT k.id, p.vorname, p.nachname, b.datum, bp.menge," +
 "a.name, a.beschreibung FROM kunden k, bestellungen b, artikel a," +
 "bestellpositionen bp, personen p WHERE p.id=? " +
 " AND k.personID = p.id AND k.id = b.kundenID AND b.id= bp.bestellID AND bp.artikelID = a.id ORDER BY b.datum";
            DataSet dataSet = new DataSet();
            try
            {
                OdbcCommand odbcCommand = new OdbcCommand(sqlBestellungen, odbcConnection);
                odbcCommand.Parameters.AddWithValue("@k.id", Int32.Parse(cbPersonen.SelectedValue.ToString()));
                OdbcDataAdapter odbcDataAdapter = new OdbcDataAdapter(odbcCommand);
                odbcDataAdapter.Fill(dataSet, "tableBestellungen");
              //  lbBestellungen.DisplayMemberPath = "name";
                lbBestellungen.ItemsSource = dataSet.Tables["tableBestellungen"].DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.GetType()}{Environment.NewLine} {ex.Message}, Datenbankfehler");
            }
        }

        private void selectedItem_Changed(object sender, SelectionChangedEventArgs e)
        {
            BestellungenShow();
        }

        private void Button_click_lieferant(object sender, RoutedEventArgs e)
        {
            Lieferanten lieferant = new Lieferanten();
            lieferant.Show();
        }

        private void Button_click_close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnArtikel_Click(object sender, RoutedEventArgs e)
        {
            Artikel artikel = new Artikel();
            artikel.Show();
        }
    }
}

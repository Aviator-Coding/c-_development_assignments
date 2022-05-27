using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Diagnostics;
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

namespace Aufgabe_29_Datenbank
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private OdbcConnection odbcConnection;

        public MainWindow()
        {
            InitializeComponent();
            odbcConnection = new OdbcConnection("DRIVER={MySQL ODBC 8.0 ANSI Driver};DATABASE=dbdemo2;UID=demo-user");
            odbcConnection.Open();
            Debug.WriteLine("Open MYSQL Connecting");
            getArtikel();
            getKunden();
            getBestellungen();
            getBestellungenVonKunden();
        }

        private void getArtikel()
        {
            DataSet dataSet = new DataSet();
            try
            {
                OdbcDataAdapter odbcDataAdapter = new OdbcDataAdapter("SELECT * FROM artikel", odbcConnection);
                odbcDataAdapter.Fill(dataSet, "artikelTable");
                dgArtikel.SelectedValuePath = "id";
                dgArtikel.ItemsSource = dataSet.Tables["artikelTable"].DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.GetType()}{Environment.NewLine} {ex.Message}, Datenbankfehler");
            }
        }

        private void getKunden()
        {
            DataSet dataSet = new DataSet();
            try
            {
                OdbcDataAdapter odbcDataAdapter = new OdbcDataAdapter("SELECT * FROM kunden", odbcConnection);
                odbcDataAdapter.Fill(dataSet, "kundenTable");
                dgKunden.SelectedValuePath = "id";
                dgKunden.ItemsSource = dataSet.Tables["kundenTable"].DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.GetType()}{Environment.NewLine} {ex.Message}, Datenbankfehler");
            }
        }

        private void getBestellungen()
        {
            DataSet dataSet = new DataSet();
            try
            {
                OdbcDataAdapter odbcDataAdapter = new OdbcDataAdapter("SELECT * FROM bestellungen", odbcConnection);
                odbcDataAdapter.Fill(dataSet, "bestellungenTable");
                dgBestellungen.SelectedValuePath = "id";
                dgBestellungen.ItemsSource = dataSet.Tables["bestellungenTable"].DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.GetType()}{Environment.NewLine} {ex.Message}, Datenbankfehler");
            }
        }

        private void getBestellungenVonKunden()
        {
            DataSet dataSet = new DataSet();
            try
            {
                OdbcDataAdapter odbcDataAdapter = new OdbcDataAdapter(@"
                SELECT
                bestellungen.id as bestellungsID,
                bestellpositionen.menge as bestellMenge,
                kunden.firma as kunde,
                bestellungen.datum as bestellDatum,
                artikel.`name` as artikelName,
                artikel.beschreibung as artikelBeschreibung,
                artikel.preis as artikelPreis,
                artikel.farbe as artikelFarbe,
                artikel.groesse as artikelGroesse
                FROM
                    bestellpositionen
                INNER JOIN
                bestellungen
                    ON
                bestellpositionen.bestellID = bestellungen.id
                INNER JOIN
                artikel
                    ON
                bestellpositionen.artikelID = artikel.id
                INNER JOIN
                kunden
                    ON
                bestellungen.kundenID = kunden.id", odbcConnection);
                odbcDataAdapter.Fill(dataSet, "bestellungenKundenTable");
                dgBestellungenVonKunden.SelectedValuePath = "id";
                dgBestellungenVonKunden.ItemsSource = dataSet.Tables["bestellungenKundenTable"].DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.GetType()}{Environment.NewLine} {ex.Message}, Datenbankfehler");
            }
        }

        public void ShutDownEvent(object sender, EventArgs e)
        {
            odbcConnection.Close();
            Debug.WriteLine("Closing MYSQL Connecting");
        }
    }
}

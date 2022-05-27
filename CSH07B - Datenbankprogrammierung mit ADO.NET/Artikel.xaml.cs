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
using System.Windows.Shapes;

namespace Lektion3_2
{
    /// <summary>
    /// Interaction logic for Artikel.xaml
    /// </summary>
    public partial class Artikel : Window
    {
        OdbcConnection odbcConnection;
        public Artikel()
        {
            InitializeComponent();
            odbcConnection = new OdbcConnection("DRIVER={MySQL ODBC 8.0 ANSI Driver};SERVER=localhost;PORT=3306;DATABASE=dbdemo2;UID=demo-user");
            GetArtikel();
        }

        public void GetArtikel()
        {
            DataSet dataSet = new DataSet();
            try
            {
                OdbcDataAdapter odbcDataAdapter = new OdbcDataAdapter("SELECT * FROM artikel", odbcConnection);
                odbcDataAdapter.Fill(dataSet, "artikelTable");
                cbArtikel.SelectedValuePath = "id";
                cbArtikel.ItemsSource = dataSet.Tables["artikelTable"].DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.GetType()}{Environment.NewLine} {ex.Message}, Datenbankfehler");
            }
        }

        private bool CheckInputFields()
        {
            if (tbName.Text.Equals("")) { MessageBox.Show("Bitte eine Name eingeben"); return false; };
            if (tbBeschreibung.Text.Equals("")) { MessageBox.Show("Bitte eine BEschreibung eingeben"); return false; };
            if (tbGroesse.Text.Equals("")) { MessageBox.Show("Bitte eine Groesse eingeben"); return false; };
            if (tbFarbe.Text.Equals("")) { MessageBox.Show("Bitte eine Farbe eingeben"); return false; };
            if (tbMenge.Text.Equals("")) { MessageBox.Show("Bitte eine Menge eingeben"); return false; };
            if (tbPreis.Text.Equals("")) { MessageBox.Show("Bitte eine Preis eingeben"); return false; };
            return true;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (cbArtikel.SelectedValue == null)
            {
                MessageBox.Show("Keinen Eintrag zum Löschen ausgewählt");
            }
            else
            {
                try
                {
                    MessageBoxResult messageBoxResult = MessageBox.Show("Wollen Sie wirklich den Artiekl aus der Datenbank löschen?", "Bitte bestätigen Sie den Löschvorgang", MessageBoxButton.YesNo);
                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        string sqlDelete = "DELETE FROM bestellpositionen WHERE artikelID=?";
                        OdbcCommand odbcCommand = new OdbcCommand(sqlDelete, odbcConnection);
                        odbcConnection.Open();
                        odbcCommand.Parameters.AddWithValue("@id", Int32.Parse(cbArtikel.SelectedValue.ToString()));
                        odbcCommand.ExecuteNonQuery();

                        sqlDelete = "DELETE FROM artikel WHERE id=?";
                        odbcCommand = new OdbcCommand(sqlDelete, odbcConnection);
                        odbcCommand.Parameters.AddWithValue("@id", Int32.Parse(cbArtikel.SelectedValue.ToString()));
                        odbcCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.GetType() + Environment.NewLine + ex.Message, "Datenbankfeheler");
                }
                finally
                {
                    odbcConnection.Close();
                    GetArtikel();
                }
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

            if (CheckInputFields())
            {
                try
                {
                    string sqlInsert = "INSERT INTO artikel SET name=? , beschreibung=? , groesse=? , farbe=? , menge=? , preis=?";
                    OdbcCommand odbcCommand = new OdbcCommand(sqlInsert, odbcConnection);
                    odbcConnection.Open();
                    odbcCommand.Parameters.AddWithValue("@name", tbName.Text);
                    odbcCommand.Parameters.AddWithValue("@beschreibung", tbBeschreibung.Text);
                    odbcCommand.Parameters.AddWithValue("@groesse", tbGroesse.Text);
                    odbcCommand.Parameters.AddWithValue("@farbe", tbFarbe.Text);
                    odbcCommand.Parameters.AddWithValue("@menge", tbMenge.Text);
                    odbcCommand.Parameters.AddWithValue("@preis", tbPreis.Text);
                    odbcCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.GetType() + Environment.NewLine + ex.Message, "Datenbankfehler");
                }
                finally
                {
                    odbcConnection.Close();
                    GetArtikel();
                }
            }
        }

        private void cbArtikel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Console.WriteLine(cbArtikel.SelectedValue.ToString());
            //ArtikelData();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (cbArtikel.SelectedValue == null)
            {
                MessageBox.Show("Bitte erst einen Artikel auswaehlen");
                return;
            }

            if (cbArtikel.SelectedValue.Equals(""))
            {
                MessageBox.Show("Bitte erst einen Artikel auswaehlen");
                return;
            }

            if (CheckInputFields())
            {
                try
                {
                    string sqlInsert = "UPDATE artikel SET name=? , beschreibung=? , groesse=? , farbe=? , menge=? , preis=? WHERE id=?";
                    OdbcCommand odbcCommand = new OdbcCommand(sqlInsert, odbcConnection);
                    odbcConnection.Open();
                    odbcCommand.Parameters.AddWithValue("@name", tbName.Text);
                    odbcCommand.Parameters.AddWithValue("@beschreibung", tbBeschreibung.Text);
                    odbcCommand.Parameters.AddWithValue("@groesse", tbGroesse.Text);
                    odbcCommand.Parameters.AddWithValue("@farbe", tbFarbe.Text);
                    odbcCommand.Parameters.AddWithValue("@menge", tbMenge.Text);
                    odbcCommand.Parameters.AddWithValue("@preis", tbPreis.Text);
                    odbcCommand.Parameters.AddWithValue("@id", Int32.Parse(cbArtikel.SelectedValue.ToString()));
                    odbcCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.GetType() + Environment.NewLine + ex.Message, "Datenbankfehler");
                }
                finally
                {
                    odbcConnection.Close();
                    GetArtikel();
                }
            }
        }
    }
}

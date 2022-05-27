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
    /// Interaction logic for Lieferanten.xaml
    /// </summary>
    public partial class Lieferanten : Window
    {
        OdbcConnection odbcConnection;

        public Lieferanten()
        {
            InitializeComponent();
            odbcConnection = new OdbcConnection("DRIVER={MySQL ODBC 8.0 ANSI Driver};SERVER=localhost;PORT=3306;DATABASE=dbdemo2;UID=demo-user");
            //odbcConnection.ConnectionTimeout = 20;
            //odbcConnection.Open();

            ShowLieferanten();
            ShowPersonen();
        }

        private void ShowLieferanten()
        {
            try
            {
                string sqlLieferanten = "SELECT * FROM lieferanten";
                OdbcDataAdapter odbcDataAdapter = new OdbcDataAdapter(sqlLieferanten, odbcConnection);
                DataSet dataSet = new DataSet();
                odbcDataAdapter.Fill(dataSet, "lieferantenTable");
                lbLieferanten.DisplayMemberPath = "firma";
                lbLieferanten.SelectedValuePath = "id";
                lbLieferanten.ItemsSource = dataSet.Tables["lieferantenTable"].DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetType() + Environment.NewLine + ex.Message, "Datenbankfehler");
            }
        }

        private void ShowPersonen()
        {
            try
            {
                string sqlLieferanten = "SELECT p.vorname,p.nachname FROM personen p , kunden k WHERE p.id = k.personID";
                OdbcDataAdapter odbcDataAdapter = new OdbcDataAdapter(sqlLieferanten, odbcConnection);
                DataSet dataSet = new DataSet();
                odbcDataAdapter.Fill(dataSet, "personenTable");
                //cbPersonen.DisplayMemberPath = "nachname";
                //cbPersonen.SelectedValuePath = "id";
                cbPersonen.ItemsSource = dataSet.Tables["personenTable"].DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetType() + Environment.NewLine + ex.Message, "Datenbankfehler");
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (tbFirma.Text.Equals(""))
            {
                MessageBox.Show("Bitte einen Firmennamen eingeben");
            }
            else
            {
                try
                {
                    string sqlInsert = "INSERT INTO lieferanten VALUES (NULL, ? , ?) ";
                    OdbcCommand odbcCommand = new OdbcCommand(sqlInsert, odbcConnection);
                    odbcConnection.Open();
                    odbcCommand.Parameters.AddWithValue("@personenID", Int32.Parse(cbPersonen.SelectedValue.ToString()));
                    odbcCommand.Parameters.AddWithValue("@firma", tbFirma.Text);
                    odbcCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.GetType() + Environment.NewLine + ex.Message, "Datenbankfehler");
                }
                finally
                {
                    odbcConnection.Close();
                    ShowLieferanten();
                }
            }
        }

        private void btnLoeschen_Click(object sender, RoutedEventArgs e)
        {
            if (lbLieferanten.SelectedValue == null)
            {
                MessageBox.Show("Keinen Eintrag zum Löschen ausgewählt");
            }
            else
            {
                try
                {
                    MessageBoxResult messageBoxResult = MessageBox.Show("Wollen Sie wirklich den Lieferanten aus der Datenbank löschen?", "Bitte bestätigen Sie den Löschvorgang", MessageBoxButton.YesNo);
                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        string sqlDelete = "DELETE FROM lieferanten WHERE id=?";
                        OdbcCommand odbcCommand = new OdbcCommand(sqlDelete, odbcConnection);
                        odbcConnection.Open();
                        odbcCommand.Parameters.AddWithValue("@id", Int32.Parse(lbLieferanten.SelectedValue.ToString()));
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
                    ShowLieferanten();
                }
            }
        }

        private void selectedItem_Changed(object sender, RoutedEventArgs e) { }

        private void btnZurueck_Click(object sender, RoutedEventArgs e) { }

    }
}

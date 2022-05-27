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

namespace Aufgabe_30_LinqToObjects.DataModel
{
    /// <summary>
    /// Interaction logic for PageArtikel.xaml
    /// </summary>
    public partial class PageArtikel : Page
    {
        public PageArtikel()
        {
            InitializeComponent();
            GetData();
        }

        public void GetData()
        {
            DataSet dataSet = new DataSet();
            try
            {
                OdbcDataAdapter odbcDataAdapter = new OdbcDataAdapter("SELECT * FROM artikel", MysqlConnection.OdbcConnection);
                odbcDataAdapter.Fill(dataSet, "tabledata");
                DgDataGrid.SelectedValuePath = "id";
                DgDataGrid.ItemsSource = dataSet.Tables["tabledata"].DefaultView;


            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.GetType()}{Environment.NewLine} {ex.Message}, Datenbankfehler");
            }
        }

        public void InsertUpdateDelete(OP op)
        {
            OdbcCommand obdCommand = new OdbcCommand($"", MysqlConnection.OdbcConnection);
            switch (op)
            {
                case OP.insert:
                    obdCommand = new OdbcCommand($@"INSERT INTO artikel 
                                                            SET name='{TName.Text}', 
                                                                beschreibung='{TBeschreibung.Text}',
                                                                groesse='{TGroesse.Text}',
                                                                farbe='{TFarbe.Text}',
                                                                menge='{TMenge.Text}'
                                                                preis={TPreis.Text}", MysqlConnection.OdbcConnection);
                    break;

                case OP.update:
                    obdCommand = new OdbcCommand($@"UPDATE artikel 
                                                            SET name='{TName.Text}', 
                                                                beschreibung='{TBeschreibung.Text}',
                                                                groesse='{TGroesse.Text}',
                                                                farbe='{TFarbe.Text}',
                                                                menge='{TMenge.Text}'
                                                                preis={TPreis.Text} WHERE id={Tid.Text}", MysqlConnection.OdbcConnection);
                    break;

                case OP.delete:
                    obdCommand = new OdbcCommand($"DELETE FROM artikel WHERE id={Tid.Text}", MysqlConnection.OdbcConnection);
                    break;
            }

            try
            {
                obdCommand.ExecuteNonQuery();
                GetData();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        public void FillForm()
        {
            try
            {
                DataRowView row = (DataRowView)DgDataGrid.SelectedItems[0];
                Tid.Text = row["id"].ToString();
                TName.Text = row["name"].ToString();
                TBeschreibung.Text = row["beschreibung"].ToString();
                TGroesse.Text = row["groesse"].ToString();
                TFarbe.Text = row["farbe"].ToString();
                TMenge.Text = row["menge"].ToString();
                TPreis.Text = row["preis"].ToString();
            }
            catch (Exception e)
            {

            }
        }

        private void BtnInsert_OnClick(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Sind Sie Sicher das Sie den Eintrag hinzufuegen wollen?", "Hinzufuegen?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                InsertUpdateDelete(OP.insert);
            }
        }

        private void BtnUpdate_OnClick(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Sind Sie Sicher das Sie den Eintrag Aktualisieren wollen?", "Aktualisieren?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                InsertUpdateDelete(OP.update);
            }
        }

        private void BtnDelete_OnClick(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Sind Sie Sicher das Sie den Eintrag Loeschen wollen?", "Loeschen?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                InsertUpdateDelete(OP.delete);
            }
        }

        private void DgDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            FillForm();
        }
    }
}
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
    /// Interaction logic for PageKunde.xaml
    /// </summary>
    public partial class PageKunde : Page
    {
        public PageKunde()
        {
            InitializeComponent();
            GetData();
        }

        public void GetData()
        {
            DataSet dataSet = new DataSet();
            try
            {
                OdbcDataAdapter odbcDataAdapter = new OdbcDataAdapter("SELECT * FROM kunden", MysqlConnection.OdbcConnection);
                odbcDataAdapter.Fill(dataSet, "tabledata");
                DgDataGrid.SelectedValuePath = "id";
                DgDataGrid.ItemsSource = dataSet.Tables["tabledata"].DefaultView;


            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.GetType()}{Environment.NewLine} {ex.Message}, Datenbankfehler");
            }

            DataTable dt = new DataTable();
            try
            {
                OdbcDataAdapter odbcDataAdapter =
                    new OdbcDataAdapter("SELECT * FROM personen", MysqlConnection.OdbcConnection);
                odbcDataAdapter.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    Person item = new Person();
                    item.Vorname = row["vorname"].ToString();
                    item.Nachname = row["nachname"].ToString();
                    item.Value = Int32.Parse(row["id"].ToString());

                    TPersonID.Items.Add(item);
                }
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
                    obdCommand = new OdbcCommand($@"INSERT INTO kunden SET firma='{TFirma.Text}', personID='{TPersonID.SelectedIndex}'", MysqlConnection.OdbcConnection);
                    break;

                case OP.update:
                    obdCommand = new OdbcCommand($@"UPDATE kunden SET firma='{TFirma.Text}', personID='{TPersonID.SelectedIndex }' WHERE id={TId.Text}", MysqlConnection.OdbcConnection);
                    break;

                case OP.delete:
                    obdCommand = new OdbcCommand($"DELETE FROM kunden WHERE id={TId.Text}", MysqlConnection.OdbcConnection);
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
                TId.Text = row["id"].ToString();
                TFirma.Text = row["firma"].ToString();
                TPersonID.SelectedIndex = Int32.Parse(row["personID"].ToString());
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
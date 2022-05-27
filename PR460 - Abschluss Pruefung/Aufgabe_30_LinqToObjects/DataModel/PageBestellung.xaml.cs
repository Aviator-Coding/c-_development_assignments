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
    /// Interaction logic for PageBestellung.xaml
    /// </summary>
    public partial class PageBestellung : Page
    {
        public PageBestellung()
        {
            InitializeComponent();
            GetData();
        }

        public void GetData()
        {
            DataSet dataSet = new DataSet();
            try
            {
                OdbcDataAdapter odbcDataAdapter = new OdbcDataAdapter("SELECT * FROM bestellungen", MysqlConnection.OdbcConnection);
                odbcDataAdapter.Fill(dataSet, "tabledata");
                dgDataGrid.SelectedValuePath = "id";
                dgDataGrid.ItemsSource = dataSet.Tables["tabledata"].DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.GetType()}{Environment.NewLine} {ex.Message}, Datenbankfehler");
            }
        }
    }
}


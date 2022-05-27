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

namespace Aufgabe_30_LinqToObjects.DataModel
{
    /// <summary>
    /// Interaction logic for PageAngestellter.xaml
    /// </summary>
    public partial class PageAngestellter : Page
    {
        public PageAngestellter()
        {
            InitializeComponent();
            GetData();
        }

        public void GetData()
    {
        DataSet dataSet = new DataSet();
        try
        {
            OdbcDataAdapter odbcDataAdapter = new OdbcDataAdapter("SELECT * FROM angestellte", MysqlConnection.OdbcConnection);
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
                if (TAusscheideDatum.SelectedDate == null)
                {
                    obdCommand = new OdbcCommand($@"INSERT INTO angestellte 
                                                      SET abtID={TabtID.Text}, 
                                                          versicherungsnr={Tversicherungsnr.Text},
                                                          gehalt ={TGehalt.Text},
                                                          einstellungsdatum ='{TEinstellungsDatum.DisplayDate.Date.ToString("yyyy-MM-dd H:mm:ss")}',
                                                          geburtsdatum ='{TGeburtsDatum.DisplayDate.Date.ToString("yyyy-MM-dd H:mm:ss")}',
                                                          geschlecht ='{TGeschlecht.Text}',
                                                          personID={TPersonID.SelectedIndex}", MysqlConnection.OdbcConnection);
                }
                else
                {
                    obdCommand = new OdbcCommand($@"INSERT INTO angestellte 
                                                      SET abtID={TabtID.Text}, 
                                                          versicherungsnr={Tversicherungsnr.Text},
                                                          gehalt ={TGehalt.Text},
                                                          einstellungsdatum ='{TEinstellungsDatum.DisplayDate.Date.ToString("yyyy-MM-dd H:mm:ss")}',
                                                          ausscheidedatum ='{TAusscheideDatum.DisplayDate.Date.ToString("yyyy-MM-dd H:mm:ss")}',
                                                          geburtsdatum ='{TGeburtsDatum.DisplayDate.Date.ToString("yyyy-MM-dd H:mm:ss")}',
                                                          geschlecht ='{TGeschlecht.Text}',
                                                          personID={TPersonID.SelectedIndex}", MysqlConnection.OdbcConnection);
                    }
                break;

            case OP.update:
                    if (TAusscheideDatum.SelectedDate == null)
                    {
                        obdCommand = new OdbcCommand($@"UPDATE angestellte 
                                                      SET abtID={TabtID.Text}, 
                                                          versicherungsnr={Tversicherungsnr.Text},
                                                          gehalt ={TGehalt.Text},
                                                          einstellungsdatum ='{TEinstellungsDatum.DisplayDate.Date.ToString("yyyy-MM-dd H:mm:ss")}',
                                                          geburtsdatum ='{TGeburtsDatum.DisplayDate.Date.ToString("yyyy-MM-dd H:mm:ss")}',
                                                          geschlecht ='{TGeschlecht.Text}',
                                                          personID={TPersonID.SelectedIndex}
                                                          WHERE id={Tid.Text}", MysqlConnection.OdbcConnection);
                    }
                    else
                    {
                        obdCommand = new OdbcCommand($@"UPDATE angestellte 
                                                      SET abtID={TabtID.Text}, 
                                                          versicherungsnr={Tversicherungsnr.Text},
                                                          gehalt ={TGehalt.Text},
                                                          einstellungsdatum ='{TEinstellungsDatum.DisplayDate.Date.ToString("yyyy-MM-dd H:mm:ss")}',
                                                          ausscheidedatum ='{TAusscheideDatum.DisplayDate.Date.ToString("yyyy-MM-dd H:mm:ss")}',
                                                          geburtsdatum ='{TGeburtsDatum.DisplayDate.Date.ToString("yyyy-MM-dd H:mm:ss")}',
                                                          geschlecht ='{TGeschlecht.Text}',
                                                          personID={TPersonID.SelectedIndex}
                                                          WHERE id={Tid.Text}", MysqlConnection.OdbcConnection);
                    }
                    break;

            case OP.delete:
                obdCommand = new OdbcCommand($"DELETE FROM angestellte WHERE id={Tid.Text}", MysqlConnection.OdbcConnection);
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
            
            Tversicherungsnr.Text = row["versicherungsnr"].ToString();
            TGehalt.Text = row["gehalt"].ToString();
            TEinstellungsDatum.SelectedDate = DateTime.Parse(row["einstellungsdatum"].ToString());
            if (row["ausscheidedatum"].ToString() != String.Empty)
            {
                TAusscheideDatum.SelectedDate = DateTime.Parse(row["ausscheidedatum"].ToString());
                }
            else
            {
                TAusscheideDatum.SelectedDate = null;
            }
            TGeburtsDatum.SelectedDate = DateTime.Parse(row["geburtsdatum"].ToString());
            TGeschlecht.Text = row["geschlecht"].ToString();
            TabtID.Text = row["abtID"].ToString();
            TPersonID.SelectedIndex = Int32.Parse(row["personID"].ToString());

            }
        catch (Exception e)
        {
            Debug.WriteLine(e.Message);
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

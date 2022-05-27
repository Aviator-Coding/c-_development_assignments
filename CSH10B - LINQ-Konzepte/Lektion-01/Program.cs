using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lektion_01
{
    class Program
    {
        private OdbcConnection odbcConnection;
        private List<string> cityList;
        OdbcDataReader AccessDb(string sqlString)
        {
           // odbcConnection = new OdbcConnection("DRIVER={MySQL ODBC 8.0 ANSI Driver};DATABASE=dbdemo2;UID=demo-user;PWD=demo-user;");
            odbcConnection = new OdbcConnection("DRIVER={MySQL ODBC 8.0 ANSI Driver};DATABASE=dbdemo2;UID=demo-user;");
            odbcConnection.Open();
            OdbcCommand command =
            new OdbcCommand(sqlString, odbcConnection);
            return command.ExecuteReader();
        }
        public void ReadCities()
        {
            string sqlString = "SELECT DISTINCT(ort) FROM personen";
            OdbcDataReader reader = this.AccessDb(sqlString);
            cityList = new List<string>();
            while (reader.Read())
            {
                cityList.Add(reader.GetString(0));
            }
            odbcConnection.Close();
        }
        static void Main(string[] args)
        {
            Program pr = new Program();
            pr.ReadCities();
            foreach (var  city in pr.cityList)
            {
                Console.Write($"\"{city}\",");
                
            }
            Console.WriteLine();
            Console.ReadLine();
        }
    }
}




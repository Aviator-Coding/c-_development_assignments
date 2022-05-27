using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aufgabe_30_LinqToObjects
{
    class Settings
    {
        public static string ConnectionString = "DRIVER={MySQL ODBC 8.0 ANSI Driver};DATABASE=dbdemo2;UID=demo-user";
        public static string[] DbTables = new[] { "Abteilung", "Angestellter", "Artikel", "Kontakt", "Kunde", "Lieferant", "Person" };
    }

    class MysqlConnection
    {
        public static OdbcConnection OdbcConnection = new OdbcConnection(Settings.ConnectionString);
    }

    public enum OP
    {
        insert,
        update,
        delete
    }
}

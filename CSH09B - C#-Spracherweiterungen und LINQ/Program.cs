using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ESA
{
    public static class EnumerationErweiterung
    {
        public static void ArtikeDescription(this Artikel art)
        {
            Console.WriteLine($"[ArtikeDescription Erweiterungsmethode] {art.Name} kostet {art.Menge * art.Preis}");
        }
    }

    class Person
    {
        public uint ID { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public string Strasse { get; set; }
        public int Hausnummer { get; set; }
        public string Ort { get; set; }
        public string Land { get; set; }
        public int PLZ { get; set; }
        public long Telefon { get; set; }
    }

    public class Artikel
    {
        public uint ID { get; set; }
        public string Name { get; set; }
        public string Beschreibung { get; set; }
        public string Groesse { get; set; }
        public string Farbe { get; set; }
        public int Menge { get; set; }
        public float Preis { get; set; }
    }
    class Program
    {
        private OdbcConnection odbcConnection;
        private IList<Person> personList = new List<Person>();
        private IList<Artikel> artikelList = new List<Artikel>();
        private IList<Artikel> artikelListErweiterungsMethode = new List<Artikel>();
        private enum SwitchReason { Aufgabe1, Aufgabe2, Aufgabe3, Aufgabe4, Aufgabe5 }

        private Dictionary<string, int> Bankleitzahl = new Dictionary<string, int>();

        private void SwitchMethod(SwitchReason reason)
        {
            switch (reason)
            {
                case SwitchReason.Aufgabe1:
                    Console.WriteLine($"################################");
                    Console.WriteLine($"#           Aufgabe 1          #");
                    Console.WriteLine($"################################");
                    this.Aufgabe01();
                    Console.WriteLine();
                    break;
                case SwitchReason.Aufgabe2:
                    Console.WriteLine($"################################");
                    Console.WriteLine($"#           Aufgabe 2          #");
                    Console.WriteLine($"################################");
                    this.StoreBankleitzahlen();
                    this.PrintBankleitzahlen();
                    Console.WriteLine();
                    break;
                case SwitchReason.Aufgabe3:
                    Console.WriteLine($"################################");
                    Console.WriteLine($"#           Aufgabe 3          #");
                    Console.WriteLine($"################################");
                    this.Aufgabe03();
                    Console.WriteLine();
                    break;
                case SwitchReason.Aufgabe4:
                    Console.WriteLine($"################################");
                    Console.WriteLine($"#           Aufgabe 4          #");
                    Console.WriteLine($"################################");
                    this.MostExpensiveArticle();
                    Console.WriteLine();
                    break;
                case SwitchReason.Aufgabe5:
                    Console.WriteLine($"################################");
                    Console.WriteLine($"#           Aufgabe 5          #");
                    Console.WriteLine($"################################");
                    this.Aufgabe05();
                    Console.WriteLine();
                    break;
            }
        }

        //#########################################
        //                Aufgabe 1               #   
        //#########################################
        private void Aufgabe01()
        {
            artikelListErweiterungsMethode.Add(new Artikel
            {
                Name = "TestArtikel",
                Preis = 10.25f,
                Menge = 10,
            });
            Console.WriteLine($"aufruf der Erweiterungs Methode");
            artikelListErweiterungsMethode[0].ArtikeDescription();
        }

        //#########################################
        //                Aufgabe 2               #   
        //#########################################

        private void StoreBankleitzahlen()
        {
            Bankleitzahl.Add("Bundesbank", 10000000);
            Bankleitzahl.Add("Postbank Ndl der Deutsche Bank", 10010010);
            Bankleitzahl.Add("OLINDA Zweigniederlassung Deutschland", 10010123);
            Bankleitzahl.Add("Klarna Bank German Branch", 10010300);
            Bankleitzahl.Add("Aareal Bank", 10010424);
            Bankleitzahl.Add("Afone Paiement", 10010500);
        }

        private void PrintBankleitzahlen()
        {
            Console.WriteLine($"explizites Iterieren:");
            IEnumerator<KeyValuePair<string, int>> enumerator = Bankleitzahl.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Console.WriteLine($"Key: {enumerator.Current.Key}, Value {enumerator.Current.Value}");
            }

            Console.WriteLine($"Foreach Loop:");
            foreach (KeyValuePair<string, int> bank in Bankleitzahl)
            {
                Console.WriteLine($"Key: {bank.Key}, Value {bank.Value}");
            }
        }

        //#########################################
        //                Aufgabe 3               #   
        //#########################################
        private OdbcDataReader GetPersonFromDB()
        {
            odbcConnection = new OdbcConnection("DRIVER={MySQL ODBC 8.0 ANSI Driver};DATABASE=dbdemo2;UID=demo-user");
            odbcConnection.Open();
            OdbcCommand odbcCommand = new OdbcCommand("SELECT id,vorname,nachname,strasse,hausnummer,ort,land,plz,telefon FROM personen", odbcConnection);
            return odbcCommand.ExecuteReader();
        }

        private void ReadPersonenFromDB()
        {
            OdbcDataReader reader = this.GetPersonFromDB();
            while (reader.Read())
            {
                personList.Add(new Person
                {
                    ID = (uint)reader.GetInt32(0),
                    Vorname = reader.GetString(1),
                    Nachname = reader.GetString(2),
                    Strasse = reader.GetString(3),
                    Hausnummer = reader.GetInt32(4),
                    Ort = reader.GetString(5),
                    Land = reader.GetString(6),
                    PLZ = reader.GetInt32(7),
                    Telefon = reader.GetInt64(8)
                });
            }
            reader.Close();
        }

        private void Aufgabe03()
        {
            this.ReadPersonenFromDB();
            Console.WriteLine(@"a) Formulieren Sie die Abfrage nach Personen, deren Vorname „John“ lautet und deren Ort „Burlington“ ist, als LINQ - Ausdruck:");
            Console.WriteLine();
            var resulta = from p in this.personList orderby p.Nachname ascending where p.Vorname == "John" && p.Ort == "Burlington" select p;
            foreach (Person person in resulta)
            {
                Console.WriteLine($"{person.Vorname} {person.Nachname} in {person.Ort}");
            }
            Console.WriteLine();

            Console.WriteLine(@"b) Formulieren Sie die Abfrage nach Personen, deren Nachname „Cara“ oder „Lull“ lautet, als LINQ-Ausdruck:");
            Console.WriteLine();
            var resultb = from p in this.personList orderby p.Nachname ascending where p.Nachname == "Cara" || p.Nachname == "Lull" select p;
            foreach (Person person in resultb)
            {
                Console.WriteLine($"{person.Vorname} {person.Nachname} in {person.Ort}");
            }
            Console.WriteLine();
            List<string> ort = new List<string> {"Belmont", "Powell"};
            Console.WriteLine(@"c) Formulieren Sie die Abfrage nach Personen, deren Nachname „Cara“ oder „Lull“ lautet, als LINQ-Ausdruck:");
            Console.WriteLine();
            var resultc = from p in this.personList orderby p.Nachname ascending where ort.Contains(p.Ort) select p;
            foreach (Person person in resultc)
            {
                Console.WriteLine($"{person.Vorname} {person.Nachname} in {person.Ort}");
            }
            Console.WriteLine();


        }

        //#########################################
        //                Aufgabe 4               #   
        //#########################################
        private OdbcDataReader GetArtikelsFromDB()
        {
            odbcConnection = new OdbcConnection("DRIVER={MySQL ODBC 8.0 ANSI Driver};DATABASE=dbdemo2;UID=demo-user");
            odbcConnection.Open();
            OdbcCommand odbcCommand = new OdbcCommand("SELECT id,name,beschreibung,groesse,farbe,menge,preis FROM artikel", odbcConnection);
            return odbcCommand.ExecuteReader();
        }

        private void ReadArtikelsFromDB()
        {
            OdbcDataReader reader = this.GetArtikelsFromDB();
            while (reader.Read())
            {
                artikelList.Add(new Artikel{
                    ID = (uint)reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Beschreibung = reader.GetString(2),
                    Groesse = reader.GetString(3),
                    Farbe = reader.GetString(4),
                    Menge = reader.GetInt32(5),
                    Preis = reader.GetFloat(6),
                });
            }
            reader.Close();
        }

        private void MostExpensiveArticle()
        {
            this.ReadArtikelsFromDB();
            var teuersterArtikel = (from a in this.artikelList select a.Preis).Max();
            Console.WriteLine($"Der teuerste Artikel kostet {teuersterArtikel}");
        }

        //#########################################
        //                Aufgabe 5               #   
        //#########################################
        //SELECT vorname, nachname, ort FROM personen
        //WHERE ort = 'Bedford' AND ort IN(
        //    SELECT ort FROM personen GROUP BY ort
        //    HAVING COUNT(ort)>=3
        //    ) ORDER BY nachname
        private void ESA5(string queriedCity)
        {
            this.personList = new List<Person>(); ;
            this.ReadPersonenFromDB();
            var ort = from o in this.personList group o by o.Ort into gort where gort.Count() >= 3 select gort.Key;
            var result = from p in this.personList orderby p.Nachname where p.Ort == queriedCity && ort.Contains(queriedCity) select p;
            Console.WriteLine($"{result.Count()} wohnen in \"{queriedCity}\"");
            foreach (Person person in result)
            {
                Console.WriteLine($"  {person.Vorname} {person.Nachname} in {person.Ort}");
            }
            Console.WriteLine();
        }

        private void Aufgabe05()
        {
            this.ESA5("Bedford");
        }

        static void Main(string[] args)
        {
            Program pr = new Program();
            for (int i = 0; i < Enum.GetValues(typeof(SwitchReason)).Cast<int>().Count(); i++)
            {
                pr.SwitchMethod((SwitchReason)i);
            }

            Console.ReadKey();
        }
    }
}

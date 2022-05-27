using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;

namespace Aufgabe_04
{

    [Table(Name = "Categories")]
    class Kategorie
    {
        [Column(Name = "CategoryID", IsPrimaryKey = true)]
        public int Id { get; set; }
        [Column(Name = "CategoryName")]
        public string Name { get; set; }
        [Column(Name = "Description")]
        public string Beschreibung { get; set; }

    }

    class Program
    {
        private string connectionString = @"Data Source=DESKTOP-7LG8IV4\TEW_SQLEXPRESS;Initial Catalog=Northwind;Integrated Security=True";

        public void DistinctCatergories()
        {
            DataContext dataContext = new DataContext(connectionString);
            dataContext.Log = Console.Out;
            var kategorie =
                from c in dataContext.GetTable<Kategorie>()
                select c;
            foreach (var c in kategorie)
            {
                Console.WriteLine($"Produktgruppe {c.Name} ({c.Beschreibung})");
            }
        }

        static void Main(string[] args)
        {
            Program pr = new Program();
            // pr.CustomersIn("test");
            pr.DistinctCatergories();
            Console.ReadLine();
        }
    }
}

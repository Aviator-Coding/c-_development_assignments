using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Lektion_03
{

    [Table(Name = "Customers")]
    class Kunden
    {
        [Column(Name = "CustomerID", IsPrimaryKey = true)]
        public string Id { get; set; }
        [Column(Name = "ContactName")]
        public string Name { get; set; }
        [Column(Name = "City")]
        public string Stadt { get; set; }
    }

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

        void DistinctPerson()
        {
            DataContext dataContext = new DataContext(connectionString);
            // ...
        }

        public void DistinctCities()
        {
            DataContext dataContext = new DataContext(connectionString);
            dataContext.Log = Console.Out;
            var cities =
                (from customer in dataContext.GetTable<Kunden>()
                 select customer.Stadt).Distinct();
            foreach (var c in cities)
                Console.WriteLine(c);
        }

        public void CustomersIn(string city)
        {
            DataContext dataContext = new DataContext(connectionString);
            var customers =
                from c in dataContext.GetTable<Kunden>()
                where c.Stadt == city
                select c;
            foreach (var c in customers)
                Console.WriteLine(c.Name);
        }

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Lektion_01_List
{
    delegate void MessageDelegate(string message);

    class Program
    {
       public IList<string> cityList = new List<string>
        {
            "Darmstadt", "Needham", "Waltham", "Atlanta", "Winchester", "Milton", "Stow", "Acton", "Concord",
            "Framingham", "Natick", "Watertown", "Emeryville", "Burlington", "Lexington", "Houston", "Newton",
            "Belmont", "Bedford", "Boston", "Westwood", "Cambridge", "West Roxbury", "Gloucester", "Long Beach",
            "Wellesley", "Rutherford", "New York", "Paoli", "Knoxville", "Carmel", "Middletown", "Raleigh",
            "Chattanooga", "Hull", "Columbus", "Syracuse", "Brooklyn Park", "Minneapolis", "St Paul", "Mamaroneck",
            "Westerville", "South Laguna", "Elmsford", "McLean", "Winnipeg", "Wilmington", "St. Louis", "San Francisco",
            "Sarasota", "Baltimore", "New Berlin", "Bohemia", "Fort Wayne", "Burbank", "Plymouth", "Ft. Wayne",
            "Salt Lake City", "Kansas City", "Fairfax", "Spokane", "Wood Bridge", "Don Mills", "Austin", "Cincinnati",
            "North Miami", "Lakewood", "Port Washington", "Washington", "Powell", "Mississauga", "Matthews", "Landover",
            "Denver", "Victoria", "Miramar", "Winter Park", "Overland Park", "Hartford", "Miami", "Sioux City",
            "Edmonton", "San Ramon", "Rochester", "Jackson", "East Douglas", "Reston", "Lisle", "Albany", "Laramie",
            "Santa Fe", "San Jose", "Orlando", "New Orleans", "LeCroix", "New Bedford", "Fargo", "Eugene",
            "Los Angeles", "Missola", "Manchester", "Danvers", "Canton", "Lincoln", "Marblehead", "Arlington", "Iselin",
            "Schaumburg",
        };

       void message(string message)
       {
           Console.WriteLine(message);
       }

       void MessagePerDelegate(string message)
       {
           MessageDelegate messageDelegate = new MessageDelegate(this.message);
           messageDelegate(message);
       }
        
        static void Main(string[] args)
        {
            Program pr = new Program();
            int caseNumber = 3;
            switch (caseNumber)
            {
                case 1:
                    pr.MessagePerDelegate("test");
                    break;

                case 2:
                    var listenum = pr.cityList.GetEnumerator();
                    while (listenum.MoveNext())
                    {
                        Console.WriteLine(listenum.Current);
                    }
                    break;


                case 3:
                    var result = pr.cityList.OrderBy(city => city.Length).Take(5);
                    foreach (var city in result)
                    {
                        Console.WriteLine(city);
                    }
                    

                    break;
            }
            //foreach (var city in pr.cityList)
            //{
            //    Console.WriteLine(city);
            //}

            Console.ReadLine();
        }
    }
}

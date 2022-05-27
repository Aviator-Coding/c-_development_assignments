using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aufgabe01_Ueberladungen
{
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

        public void TestUeberladungen()
        {
            Console.WriteLine($"{cityList.Min(name => name.Length)}");
           

        }

        public void Where()
        {
            var cityListWithWhitespaces = cityList.Where(name => name.Contains(" "));
            foreach(var city in cityListWithWhitespaces)
            {
                Console.WriteLine(city);
            }
        }

        static void Main(string[] args)
        {
            Program pr = new Program();
            pr.Where();
            Console.ReadLine();
        }
    }
}

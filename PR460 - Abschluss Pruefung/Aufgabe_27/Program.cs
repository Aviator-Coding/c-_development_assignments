using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aufgabe_27
{
    class Program
    {
        public enum Monat
        {
            Unknown,
            Januar,
            Februar,
            Maerz,
            April,
            Mai,
            Juni,
            July,
            August,
            September,
            Oktober,
            November,
            Dezember
        }
        static void Main(string[] args)
        {
            Console.WriteLine($"Heute ist der Monat {(Monat)DateTime.Now.Month} ({DateTime.Now.Month})");
            Console.ReadLine();
        }
    }
}

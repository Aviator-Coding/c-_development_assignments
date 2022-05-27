using System;

namespace Aufgabe_03
{
    class Program
    {

        public static int Fakultaet(int n)
        {
            int result = 1;

            for (int i = 1; i <= n; i++)
            {
                result *= i;
                Console.WriteLine($"Durchlauf {i} - Ergebnis {result}");
            }
            return result;
        }

        static void Main(string[] args)
        {
            Console.WriteLine($"Das Ergebniss der Fakultaet 10 lautet: {Program.Fakultaet(10)}");
        }
    }
}

using System;

namespace Aufgabe_04
{
    class Program
    {

        static void UlamVermutung(int zahl)
        {
            if (zahl <= 0)
            {
                Console.WriteLine("Die Zahl ist geringer wie 0!! Und kann dadurch nicht berechnet werden.");
                return;
            }

            while (zahl != 1)
            {
              
                if (zahl % 2 == 0)
                {
                    //Der Rest war 0 was heisst das unsere Zahl gerade ist
                    zahl /= 2;
                }
                else
                {
                    // Der Rest war nicht gleich 0 was heisst das wir ungerade sind
                    zahl = zahl * 3  + 1;
                }
                Console.WriteLine($"Momentane Zahl {zahl}");
            }
        }
        static void Main(string[] args)
        {
            Program.UlamVermutung(101);
        }
    }
}

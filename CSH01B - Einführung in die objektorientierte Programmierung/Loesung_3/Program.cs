using System;

namespace Loesung
{
    class Program
    {
        enum Airbus { A300 = 200, A310, A318 = 100, A319, A320, A321, A330 = 300, A340, A380 = 400 }
        static void Main(string[] args)
        {
            Console.WriteLine($"Der Zahlenwert des Enumeration-Elements A321 lautet {(int)Airbus.A321}");
        }
    }
}
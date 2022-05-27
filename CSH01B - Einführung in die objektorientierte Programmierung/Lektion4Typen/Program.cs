using System;

namespace Lektion4Typen
{
    class Referenztyp
    {
        public int value;

        public Referenztyp(int value)
        {
            this.value = value;
        }
    }

    class Program
    {
        static void WerttypTesten()
        {
            int a = 12345;
            int b = a;
            Console.WriteLine($"b = {b}");
            a = 0;
            Console.WriteLine($"b nach Aenderung von a = {b}");
        }

        static void ReferenzTypTesten()
        {
            Referenztyp a = new Referenztyp(12345);
            Referenztyp b = a;
            Console.WriteLine($"b = {b.value}");
            a.value = 0;
            Console.WriteLine($"b nach Aenderung von a = {b.value}");


        }
        static void Main(string[] args)
        {
            Program.ReferenzTypTesten();
            Program.WerttypTesten();
        }
    }
}

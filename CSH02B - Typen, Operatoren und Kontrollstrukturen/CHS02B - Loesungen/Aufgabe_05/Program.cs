using System;

namespace Aufgabe_05
{
    class Program
    {
        public bool BitMusterPruefen(int zahl, int operand)
        {
            return (zahl & operand) == operand;
        }

        static void Main(string[] args)
        {
            Program pr = new Program();
            Console.WriteLine($"pr.BitMusterPruefen(2345, 2049) {pr.BitMusterPruefen(2345, 2049)}");
            Console.WriteLine($"pr.BitMusterPruefen(2345, 2050) {pr.BitMusterPruefen(2345, 2050)}");
        }
    }
}

using System;

namespace Lektion2
{
    // ich bin ein komemnta
    /*
     * Ich bin ein kommenta
     */

    /*
     ich bin ein komemnra
     */
    class Program
    {
        static string ToString(int value)
        {
            return "" + value;
        }

        public int ToInd(string text)
        {
            return Convert.ToInt32(text);
        }
        static void Main(string[] args)
        {
            string s = Program.ToString(12345);
            Console.WriteLine(s);
        }
    }
}

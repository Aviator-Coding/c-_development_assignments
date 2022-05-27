using System;
using System.IO;

namespace Loesung_01
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
           
            FileStream fs = File.Open("D:\\CSharp\\CSH03\\Loesung_01\\files\\test.txt", FileMode.OpenOrCreate);
            fs.Close();

        }
    }
}

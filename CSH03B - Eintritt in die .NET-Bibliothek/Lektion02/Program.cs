using System;
using System.Threading;


namespace Lektion02
{
    class Program
    {
        public void ProgrammTakten()
        {
            while (true)
            {
                Thread.Sleep(1000);
                Console.Write($"{DateTime.Now}\r");
            }

        }
        static void Main(string[] args)
        {
            Program test = new Program();
            test.ProgrammTakten();
        }
    }
}

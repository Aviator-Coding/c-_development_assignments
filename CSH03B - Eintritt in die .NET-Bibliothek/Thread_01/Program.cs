using System;

namespace Thread_01
{
    class Program
    {
        public void ProgrammTakten()
        {

            Console.Write($"Date {System.DateTime.Now}\r");
            while (true)
            {
                System.Threading.Thread.Sleep(1000);
                Console.Write($"Date {System.DateTime.Now}\r");
            }
        }

        public void DateTest()
        {
            Console.WriteLine($"Date {System.DateTime.Now}");
            Console.WriteLine($"Current Hour: {System.DateTime.Now.Hour}");
            Console.WriteLine($"Current Minute: {System.DateTime.Now.Minute}");
            Console.WriteLine($"Current Second : {System.DateTime.Now.Second}");
        }

        public void TabTest()
        {
            Console.WriteLine($"1\t2\t3\t4\t");
        }

        static void Main(string[] args)
        {
            Program test = new Program();
            //test.TabTest();
            //test.DateTest();
            test.ProgrammTakten();
        }
    }
}

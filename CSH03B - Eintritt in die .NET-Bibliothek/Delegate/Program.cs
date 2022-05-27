using System;

namespace Delegate
{
    // Definierung der Struktur des Delegates
    public delegate void TestDelegate(string message);

    class Program
    {
        public static TestDelegate transponder;

        static void Main(string[] args)
        {
            //.WriteLine("Hello World!");
            Test t = new Test();
            TTest tt = new TTest();
            transponder("Hallo");
        }
    }


    class Test
    {
        public Test()
        {
            Program.transponder += new TestDelegate(Message);
            Program.transponder += new TestDelegate(mWTF);
        }

        public void Message(string message)
        {
            Console.WriteLine($"Hello World - {message}");
        }
        public void mWTF(string message)
        {
            Console.WriteLine($"Hello WTF - {message}");
        }

    }

    class TTest
    {
        public TTest()
        {
            Program.transponder += new TestDelegate(Message);
        }
        public void Message(string test)
        {
            Console.WriteLine($"This is a message from TTest {test}");
        }
    }
}

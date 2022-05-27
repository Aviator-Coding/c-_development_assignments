using System;

namespace Lektion3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Luftfahrzeug LF = new Luftfahrzeug("LH 4080");
            LF.Steigen(300);
            LF.Sinken(50);

            Flugzeug FL = new Flugzeug("");
            FL.Steigen(200);
        }

        static void Message(string message)
        {
            Console.WriteLine(message);
        }
    }

    class Luftfahrzeug
    {
        protected string kennung;
        public Luftfahrzeug() { }

        public Luftfahrzeug(string kennung)
        {
            this.kennung = kennung;
        }

        public void Steigen(int meter)
        {
            Console.WriteLine(kennung + " steigt " + meter + " Meter");
        }

        public void Sinken(int meter)
        {
            Console.WriteLine(kennung + " sinkt " + meter + " Meter");
        }
    }

    class Flugzeug : Luftfahrzeug
    {
        public Flugzeug(string kennung) : base(kennung)
        {

        }
    }

    class Starrfluegelflugzeug : Flugzeug
    {
        public Starrfluegelflugzeug(string kennung) : base(kennung)
        {
        }
    }

    class Duesenflugzeug : Starrfluegelflugzeug
    {
        public Duesenflugzeug(string kennung) : base(kennung)
        {
        }
    }

}

using System;

namespace Lektion_01
{
    class Luftfahrzeug
    {
        protected string kennung;

        public override string ToString()
        {
            return $"Luftfahrzeug mit der kennung {this.kennung}";
        }

        public Luftfahrzeug(string kennung)
        {
            this.kennung = kennung;
        }
    }

    class Uebungen
    {
        public void Konsolenausgabe()
        {
            string ort = "Kleinostheim";
            int jahr = 1981;
            double ausgabe = 0.03;
            string waehrung = "DM";
            Console.WriteLine($"Die Einwohner von {ort} haben {jahr} {ausgabe} Mio. {waehrung} fuer Weihnachtsgeschenke ausgegeben");
        }
        public void ObjectMethoden()
        {
            Luftfahrzeug flieger1 = new Luftfahrzeug("LH 3000");
            Luftfahrzeug flieger2 = new Luftfahrzeug("LH 4000");
            Luftfahrzeug flieger3 = flieger1;

            Console.WriteLine($"Flieger 1 gleich Flieger 2? {flieger1.Equals(flieger2)}");
            Console.WriteLine($"Flieger 1 gleich Flieger 3? {flieger1.Equals(flieger3)}");

            Console.WriteLine($"Flieger1-Hascode: {flieger1.GetHashCode()}");
            Console.WriteLine($"Flieger2-Hascode: {flieger2.GetHashCode()}");
            Console.WriteLine($"Flieger3-Hascode: {flieger3.GetHashCode()}");

            Console.WriteLine($"ToString() Flieger1 : {flieger1.ToString()}");
            flieger3 = null;
            Console.WriteLine($"flieger 3 nach null-Zuweisung {flieger3}");

        }

        public void Objectzuweisung()
        {
            object obj0 = new object();
            Console.WriteLine($"obj0 {obj0.GetType()}");
            object obj1 = new Luftfahrzeug("LH 200");
            Console.WriteLine($"obj1 {obj1.GetType()}");
            object obj2 = 12;
            Console.WriteLine($"obj2 {obj2.GetType()}");
            object obj3 = "Eine Zeichenkette";
            Console.WriteLine($"obj3 {obj3.GetType()}");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Uebungen ue = new Uebungen(); 
            //ue.ObjectMethoden();
           // ue.Konsolenausgabe();
           ue.Objectzuweisung();
        }
    }
}

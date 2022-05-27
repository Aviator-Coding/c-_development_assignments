using System;

namespace Loesung
{

    abstract class Figur
    {
        protected int breite, hoehe;
        
        public abstract void Umfang();
        public abstract void Flaeche();
    }

    class Rechteck : Figur
    {
        public Rechteck(int breite,int hoehe)
        {
            this.breite = breite;
            this.hoehe = hoehe;
        }

        public override void Umfang()
        {
            Console.WriteLine($"Der Umfang des Rechtecks betraegt: {2 * (this.breite + this.hoehe)}");
        }

        public override void Flaeche()
        {
            Console.WriteLine($"Die Flaeche des Rechtecks betraegt: {this.breite * this.hoehe}");
        }
    }

    class Quadrat : Figur
    {
        public Quadrat(int breite)
        {
            this.breite = breite;
        }
        public override void Umfang()
        {
            Console.WriteLine($"Der Umfang des Quadrat betraegt: {4 * this.breite}");
        }

        public override void Flaeche()
        {
            Console.WriteLine($"Die Flaeche des Quadrat betraegt: {this.breite * this.breite}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Rechteck reck = new Rechteck(500,100);
            reck.Flaeche();
            reck.Umfang();

            Quadrat qad = new Quadrat(100);
            qad.Flaeche();
            qad.Umfang();

        }
    }
}

using System;
using System.Threading;

namespace Lektion01
{
    interface ITransponder
    {
        void Transpond(string kennung, Position pos);
    }
    enum Düsenflugzeugtyp : short { A300, A310, A318, A319, A320, A321, A330, A340, A380, Boeing_717, Boeing_737, Boeing_747, Boeing_777, Boeing_BBJ }

    public struct Position
    {
        public int x, y, h;
        public Position(int x, int y, int h)
        {
            this.x = x;
            this.y = y;
            this.h = h;
        }
        public void PositionÄndern(int deltaX, int deltaY, int deltaH)
        {
            x = x + deltaX;
            y = y + deltaY;
            h = h + deltaH;
        }
        public void HöheÄndern(int deltaH)//Aufruf in Sinken/Steigen
        {
            h = h + deltaH;
        }
    }

    abstract class Luftfahrzeug
    {
        protected string kennung;
        protected Position pos;

        public string Kennung
        {
            get { return kennung; }
            //set { Console.WriteLine($"You cannot set the Property outside the Constructor"); }
        }
        public abstract void Steigen(int meter);
        public abstract void Sinken(int meter);

    }

    class Flugzeug : Luftfahrzeug
    {
        public Flugzeug(string kennung, Position pos)
        {
            this.kennung = kennung;
            this.pos = pos;
        }
        public override void Steigen(int meter)
        {
            pos.HöheÄndern(meter);
            Console.WriteLine(kennung + "steigt " + meter + "Meter, Höhe =" + pos.h);
        }

        public override void Sinken(int meter)
        {
            pos.HöheÄndern(-meter);
            Console.WriteLine(kennung + "sinkt " + meter + "Meter, Höhe =" + pos.h);
        }
    }

    class Starrflügelflugzeug : Flugzeug, ITransponder
    {
        public Starrflügelflugzeug(string kennung, Position pos) : base(kennung, pos)
        {

        }
        public void Transpond(string kennung, Position pos)
        {
            if (kennung.Equals(this.kennung))
                Console.WriteLine(this.kennung + " erkennt Eingang eigenes Signal.");
            else
                Console.WriteLine(this.kennung + " empfängt Position von {0}: x = {1}, y = {2}, Höhe = {3}", this.kennung, pos.x, pos.y, pos.h);
        }
        //Provisorischer Test in Lektion 1 und Thread in Lektion 2
        public void Refresh()
        {


        }
    }

    class Düsenflugzeug : Starrflügelflugzeug
    {
        public Düsenflugzeug typ;
        private int sitzplaetze;
        private int fluggaeste;

        public enum Airbus { A300=280,A310=240,A318=260,A319=250,A320=290,A321=320,A330=350,A340=310,A350=253,A380=550}

        public int Fluggaeste
        {
            set {
                if (sitzplaetze < (fluggaeste + value))
                {
                    Console.WriteLine($"Keine Buchung : Die " +
                                      $"Fluggastzahl wuerde mit der Buchung" +
                                      $"von {value} Plaetzen die verfuegbaren Plaetze" +
                                      $"von {sitzplaetze} um {value + fluggaeste - sitzplaetze} uebersteigen");

                }
                else
                    fluggaeste += value;
            }
            get { return fluggaeste; }
        }

        public int Sitzplaetze
        {
            set {
                sitzplaetze = value;
            }
            get
            {
                if (sitzplaetze <= 0)
                    Console.WriteLine($"Fehler Sitzplaetze muessen groesser wie null sein momentaner Wert {sitzplaetze}");

                return sitzplaetze;
            }
        }

        public void Buchen(int gaeste)
        {
            Console.WriteLine($"Wir buchen {gaeste} zu dem Flug hinzu");
            Fluggaeste += gaeste;
            Console.WriteLine($"Wir haben nun {Fluggaeste} in unserem Flug");
        }

        public Düsenflugzeug(Airbus airbus, string kennung, Position pos) : base(kennung, pos)
        {
            this.Sitzplaetze = (int)airbus;
        }
    }

    class Program
    {

        // Lektion 01 - Aufgabe 1.2
        public void Kennung()
        {
            Flugzeug flieger = new Flugzeug("LH 500", new Position(500, 300, 20));
            Console.WriteLine("Kennung = {0}", flieger.Kennung);

            Düsenflugzeug flieger2 = new Düsenflugzeug(Düsenflugzeug.Airbus.A300,"DS 300", new Position(500, 300, 20));
            //flieger2.Maxplaetze = 0;

            Console.WriteLine($"Wir haben {flieger2.Sitzplaetze} Sitzplaetze");
            flieger2.Buchen(10);
            flieger2.Buchen(10);
            flieger2.Buchen(100);

        }

        public void TransponderTest() // Code 1.6
        {
            Starrflügelflugzeug flieger1 = new Starrflügelflugzeug("LH 3000", new Position(3000, 2000, 100));
            flieger1.Refresh();
            Console.WriteLine();
            Starrflügelflugzeug flieger2 = new Starrflügelflugzeug("LH 500", new Position(3500, 1500, 180));
            flieger1.Refresh();
            flieger2.Refresh();
            Console.WriteLine();
            Starrflügelflugzeug flieger3 = new Starrflügelflugzeug("LH444", new Position(17300, 23400, 780));
            flieger1.Refresh();
            flieger2.Refresh();
            flieger3.Refresh();
            Console.WriteLine();

            flieger1.Refresh();
            flieger3.Refresh();
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            Program test = new Program();
            test.Kennung();
        }
    }
}

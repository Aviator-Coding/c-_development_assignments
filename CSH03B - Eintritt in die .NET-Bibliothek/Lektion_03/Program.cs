using System;
using System.Threading;

namespace CSH03_Lektion1
{
    public delegate void TransponderDel(string kennung, Position pos);
    public delegate void FliegerRegisterDel();
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

        public void PositionÄndern(int deltaX, int deltaY)
        {
            x = x + deltaX;
            y = y + deltaY;
        }

        public void HöheÄndern(int deltaH)//Aufruf in Sinken/Steigen
        {
            h = h + deltaH;
        }

        public override string ToString()
        {
            return $"x:{x} y:{y} z{h}";
        }
    }

    abstract class Luftfahrzeug
    {
        protected string kennung;
        protected Position pos;

        public string Kennung
        {
            protected set { kennung = value; }
            get { return kennung; }
        }

        private int sitzplätze;
        private int fluggäste;
        public int Fluggäste
        {
            set
            {
                if (sitzplätze < (fluggäste + value))
                    Console.WriteLine("Keine Buchung: Die " + "Fluggastzahl würde mit der Zubuchung " + "von {0} Plätzen die verfügbaren Plätze "
                        + "von {1} um {2} übersteigen!", value, sitzplätze, value + fluggäste - sitzplätze);
                else
                    fluggäste += value;
            }
            get { return fluggäste; }
        }


        public abstract void Steigen(int meter);
        public abstract void Sinken(int meter);

    }

    class Flugzeug : Luftfahrzeug
    {
        protected Position zielPos;
        protected int streckeProTakt;

        public void Starte(Position zielPos, int streckeProTakt)
        {
            this.zielPos = zielPos;
            this.streckeProTakt = streckeProTakt;
            Console.WriteLine($"{this.kennung} - Start at {zielPos.ToString()}");
        }
        public Flugzeug(string kennung, Position startPos)
        {
            this.kennung = kennung;
            this.pos = startPos;
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

        public Starrflügelflugzeug(string kennung, Position startPos) : base(kennung, startPos)
        {
            Program.transponder += new TransponderDel(Transpond);
        }
        public void Transpond(string kennung, Position pos)
        {
            if (kennung.Equals(this.kennung))
                Console.WriteLine($"{this.kennung} an Position x={pos.x}, y={pos.y}");
            /* CSH03 - Lektion 1 - Aufgabe 1.9
            else
                Console.WriteLine(this.kennung + " empfängt Position von {0}: x = {1}, y = {2}, Höhe = {3}", this.kennung, pos.x, pos.y, pos.h);

            else if (this.pos.h - pos.h < 100 && this.pos.h - pos.h > 0)
                Console.WriteLine("\aWarnung: {0} fliegt nur {1} Meter höher als {2}",
                    this.kennung, this.pos.h - pos.h, kennung);
            else if (pos.h - this.pos.h < 100 && pos.h - this.pos.h > 0)
                Console.WriteLine("\aWarnung: {0} fliegt nur {1} Meter tiefer als {2}",
                    this.kennung, pos.h - this.pos.h, kennung);
                        */
            //
        }
        //Provisorischer Test in Lektion 1 und Thread in Lektion 2
        public void Refresh()
        {
            Program.transponder(kennung, pos);

        }


        public void Steuern()
        {
            double a, b, alpha, a1, b1;
            bool gelandet = false;

            if (!gelandet)
            {
                Program.transponder(kennung, pos);

                a = zielPos.x - pos.x;
                b = zielPos.y - pos.y;
                alpha = Math.Atan2(b, a);
                a1 = Math.Cos(alpha) * streckeProTakt;
                b1 = Math.Sin(alpha) * streckeProTakt;
                pos.PositionÄndern((int)a1, (int)b1);

                if (Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2)) < streckeProTakt)
                {
                    gelandet = true;
                    Console.WriteLine($"{kennung} ist gelandet: {pos.ToString()}");
                    Program.fliegerregister -= this.Steuern;
                }
            }
        }


    }

    class Düsenflugzeug : Starrflügelflugzeug
    {
        public Düsenflugzeug typ;
        public Düsenflugzeug(string kennung, Position startPos) : base(kennung, startPos)
        {

        }
    }

    class Program
    {

        public static TransponderDel transponder;
        public static FliegerRegisterDel fliegerregister;

        // zwei Testmethoden zum Property-Abschnitt (Lektion 1.1)
        public void Kennung()
        {
            Flugzeug flieger = new Flugzeug("LH 500", new Position(500, 300, 20));
            Console.WriteLine("Kennung = {0}", flieger.Kennung);
        }
        // Lektion 1
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
            transponder -= flieger2.Transpond; // aus Delegate entfernen
            flieger1.Refresh();
            flieger3.Refresh();
            Console.WriteLine();
        }
        public void Buchen(int anzahl)
        {
            Flugzeug flieger = new Flugzeug("LH 333", new Position(3400, 4400, 200));
            flieger.Fluggäste = anzahl;

        }
        public void ProgrammTaken()
        {
            Starrflügelflugzeug flieger1 = new Starrflügelflugzeug("LH 500", new Position(3500, 1500, 180));
            flieger1.Starte(new Position(1000, 500, 200),200);
            Starrflügelflugzeug flieger2 = new Starrflügelflugzeug("LH 3000", new Position(3000, 2000, 100));
            flieger2.Starte(new Position(1000, 500, 200), 260);
            Program.fliegerregister += flieger1.Steuern;
            Program.fliegerregister += flieger2.Steuern;
            while (true)
            {
                Program.fliegerregister();
                Thread.Sleep(1000);
            }
        }
        static void Main(string[] args)
        {
            Program test = new Program();
           // test.Kennung();
            //test.TransponderTest();
            // test.Buchen(11);
            test.ProgrammTaken();
            //Console.ReadLine();
        }
    }
}

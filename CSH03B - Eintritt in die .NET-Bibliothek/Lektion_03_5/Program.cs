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
        protected int flughoehe;
        protected int steighoeheProTakt;
        protected int sinkhoeheProTakt;
        protected bool steigt = false;
        protected bool sinkt = false;
        protected bool gelandet = false;

        public Flugzeug(string kennung, Position startPos)
        {
            Program.transponder += new TransponderDel(Transpond);
            this.kennung = kennung;
            this.pos = startPos;
        }

        public void Starte(Position zielPos, int streckeProTakt, int flughoehe, int steighoeheProTakt, int sinkhoeheProTakt)
        {
            this.zielPos = zielPos;
            this.streckeProTakt = streckeProTakt;
            this.flughoehe = flughoehe;
            this.steighoeheProTakt = steighoeheProTakt;
            this.sinkhoeheProTakt = sinkhoeheProTakt;
            this.steigt = true;

            Console.WriteLine($"{this.kennung} - Start at {zielPos.ToString()}");
        }

        private bool SinkenEinleiten()
        {
            double strecke = Math.Sqrt(Math.Pow(streckeProTakt, 2) - Math.Pow(sinkhoeheProTakt, 2));
            int sinkstrecke = (int)(strecke * (pos.h - zielPos.h) / sinkhoeheProTakt);
            int zieldistanz = Zieldistanz();
            if (sinkstrecke >= zieldistanz)
            {
                Console.WriteLine($"{Kennung} Sinkstrecke {sinkstrecke} >= Zieldistanz {zieldistanz}");
                return true;
            }

            return false;
        }

        private void PositionsBerechnen(double strecke, int steighoeheProTakt)
        {
            double a, b, alpha, a1, b1;
            a = zielPos.x - pos.x;
            b = zielPos.y - pos.y;
            alpha = Math.Atan2(b, a);
            a1 = Math.Cos(alpha) * streckeProTakt;
            b1 = Math.Sin(alpha) * streckeProTakt;
            pos.PositionÄndern((int)a1, (int)b1,steighoeheProTakt);
        }

        private int Zieldistanz()
        {
            return (int)Math.Sqrt(Math.Pow(zielPos.x - pos.x, 2) + Math.Pow(zielPos.y - pos.y, 2));
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

        public void Transpond(string kennung, Position pos)
        {
            if (kennung.Equals(this.kennung))
                Console.WriteLine($"{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second} - {this.kennung} an Position x={pos.x}, y={pos.y}, h={pos.h} Zieldistanz {Zieldistanz()}");
        }

        public void Steuern()
        {
            if (steigt)
            {
                if (this.SinkenEinleiten())
                {
                    steigt = false;
                    sinkt = true;
                }
                else if (pos.h > flughoehe)
                {
                    steigt = false;
                }
            }
            else if (sinkt)
            {
                if (pos.h <= zielPos.h + sinkhoeheProTakt)
                    gelandet = true;
            }
            else
            {
                if (this.SinkenEinleiten())
                {
                    sinkt = true;
                }
            }

            if (!gelandet)
            {
                Program.transponder(kennung, pos);
                if (steigt)
                {
                    double strecke = Math.Sqrt(Math.Pow(streckeProTakt, 2) - Math.Pow(steighoeheProTakt, 2));
                    this.PositionsBerechnen(strecke, steighoeheProTakt);
                }
                else if (sinkt)
                {
                    double strecke = Math.Sqrt(Math.Pow(streckeProTakt, 2) - Math.Pow(sinkhoeheProTakt, 2));
                    this.PositionsBerechnen(strecke, -sinkhoeheProTakt);
                }
                else
                {
                    this.PositionsBerechnen(streckeProTakt, 0);
                }
            }
            else
            {
                Program.fliegerregister -= this.Steuern;
                Program.transponder -= this.Transpond;
                Console.WriteLine($"\n {Kennung} gelandet Zieldistanz={Zieldistanz()}, Hoehendistanz={pos.h - zielPos.h}");
            }

        }
    }

    class Starrflügelflugzeug : Flugzeug, ITransponder
    {

        public Starrflügelflugzeug(string kennung, Position startPos) : base(kennung, startPos)
        {
            // Program.transponder += new TransponderDel(Transpond);
        }

        //Provisorischer Test in Lektion 1 und Thread in Lektion 2
        public void Refresh()
        {
            Program.transponder(kennung, pos);

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
            flieger1.Starte(new Position(1000, 500, 200), 200, 300, 20, 10);
            Starrflügelflugzeug flieger2 = new Starrflügelflugzeug("LH 3000", new Position(3000, 2000, 100));
            flieger2.Starte(new Position(1000, 500, 200), 260, 350, 25, 15);
            Program.fliegerregister += flieger1.Steuern;
            Program.fliegerregister += flieger2.Steuern;
            while (Program.fliegerregister != null)
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

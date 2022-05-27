using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CSH03_Lektion3
{

    public delegate void TransponderDel(string kennung, Position pos);
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

        protected Position pos;
        public abstract void Steigen(int meter);
        public abstract void Sinken(int meter);

    }

    class Flugzeug : Luftfahrzeug
    {
        protected Position zielPos;
        protected int streckeProTakt;
        protected int flughöhe;
        protected int steighöheProTakt;
        protected int sinkhöheProTakt;
        protected bool steigt = false;
        protected bool sinkt = false;

        public void Starte(Position zielPos, int streckeProTakt, int flughöhe, int steighöheProTakt, int sinkhöheProTakt)
        {
            // neu Lektion 3
            this.zielPos = zielPos;
            this.streckeProTakt = streckeProTakt;
            this.flughöhe = flughöhe;
            this.steighöheProTakt = steighöheProTakt;
            this.sinkhöheProTakt = sinkhöheProTakt;
            this.steigt = true;
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
            Program.transponder += Transpond;
        }
        public void Transpond(string kennung, Position pos)
        {
            double abstand = Math.Sqrt(Math.Pow(this.pos.x - pos.x, 2) + Math.Pow(this.pos.y - pos.y, 2));
            DateTime timestamp = DateTime.Now;
            if (kennung.Equals(this.kennung))
            {
                Console.Write("{0}:{1}", timestamp.Minute, timestamp.Second);
                Console.Write("\t{0}-Position: {1}-{2}-{3}", this.kennung, base.pos.x, base.pos.y, base.pos.h);
                Console.Write("Zieldistanz: {0} m\n", Zieldistanz());
            }
            else
            {
                Console.Write("\t{0} ist {1} m von {2} entfernt.\n", this.kennung, (int)abstand, kennung);
                if (Math.Abs(this.pos.h - pos.h) < 100 && abstand < 500)
                    Console.WriteLine("\tWARNUNF: {0} hat nur {1} m Höhenabstand!", kennung, Math.Abs(this.pos.h - pos.h));
            }
        }

        // Neu Lektion 3


        double a, b, alpha, a1, b1;
        bool gelandet = false;


        private bool SinkenEinleiten()
        {
            double strecke = Math.Sqrt(Math.Pow(streckeProTakt, 2) - Math.Pow(sinkhöheProTakt, 2));
            int sinkstrecke = (int)(strecke * (pos.h - zielPos.h) / sinkhöheProTakt);
            int zieldistanz = Zieldistanz();
            if (sinkstrecke >= zieldistanz)
            {
                //optinale Consolenausgabe zur Kontrolle
                Console.WriteLine("{0} Sinkstrecke {1} >= Zieldistanz {2}", kennung, sinkstrecke, zieldistanz);
                return true;
            }
            else
                return false;
        }

        // Beim Sinkflug ist ein negativer Wert für den zweiten
        // Parameter anzugeben.

        private void PositionBerechnen(double strecke, int steighöheProTakt)
        {
            // modifizierte Übernahme der bisherigen Berechungen aus "Steuern"
            a = zielPos.x - pos.x;
            b = zielPos.y - pos.y;
            alpha = Math.Atan2(b, a);
            a1 = Math.Cos(alpha) * strecke;
            b1 = Math.Sin(alpha) * strecke;
            pos.PositionÄndern((int)a1, (int)b1, steighöheProTakt);
        }
        private int Zieldistanz()
        {
            return (int)Math.Sqrt(Math.Pow(zielPos.x - pos.x, 2) + Math.Pow(zielPos.y - pos.y, 2));
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
                else if (pos.h > flughöhe)
                {
                    steigt = false;
                }
            }
            else if (sinkt)
            {
                if (pos.h <= zielPos.h + sinkhöheProTakt)
                    gelandet = true;
            }
            else
            {
                //Horizontalflug
                if (this.SinkenEinleiten())
                {
                    sinkt = true;
                }
            }
            if (!gelandet)
            {
                // Zunächst die aktuelle Position ausgeben:
                Program.transponder(kennung, pos);
                //"Strecke" (am Boden) berechnen:
                if (steigt)
                {
                    double strecke = Math.Sqrt(Math.Pow(streckeProTakt, 2) - Math.Pow(steighöheProTakt, 2));
                    this.PositionBerechnen(strecke, steighöheProTakt);
                }
                else if (sinkt)
                {
                    double strecke = Math.Sqrt(Math.Pow(streckeProTakt, 2) - Math.Pow(sinkhöheProTakt, 2));
                    this.PositionBerechnen(strecke, -sinkhöheProTakt);
                }
                else
                {
                    // im Horizontalflug ist "strecke" gleich "streckeProTakt"
                    this.PositionBerechnen(streckeProTakt, 0);
                }
            }
            else
            {
                // Flieger derigistrieren, Transponder abschalten, Abschlussmeldung
                Program.fliegerRegister -= this.Steuern;
                Program.transponder -= this.Transpond;
                Console.WriteLine("\n{0} gelandet (Zieldistanz={1}, Höhendistanz={2}", kennung, Zieldistanz(), pos.h - zielPos.h);
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
        // Wiederholungsaufgabe 2.3
        public delegate void FliegerRegisterDel();
        public static FliegerRegisterDel fliegerRegister;

        public void ProgrammTaken()
        {
            Starrflügelflugzeug flieger1 = new Starrflügelflugzeug("LH 500", new Position(3500, 1400, 180));
            Program.fliegerRegister += flieger1.Steuern;
            flieger1.Starte(new Position(1000, 500, 20), 200, 300, 20, 10);
            Starrflügelflugzeug flieger2 = new Starrflügelflugzeug("LH 3000", new Position(3000, 2000, 100));
            Program.fliegerRegister += flieger2.Steuern;
            flieger2.Starte(new Position(1000, 500, 200), 260, 350, 25, 15);
            while (fliegerRegister != null)
            {
                Thread.Sleep(1000);
                fliegerRegister();
                Console.WriteLine();
            }
        }
        static void Main(string[] args)
        {
            Program test = new Program();
            test.ProgrammTaken();
            Console.ReadLine();
        }
    }
}

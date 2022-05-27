using System;
using System.IO;
using System.Threading;

namespace ESA_Projekt
{

    public delegate void TransponderDel(string kennung, Position pos);
    interface ITransponder
    {
        void Transpond(string kennung, Position pos);
    }
    enum Airbus : short { A300, A310, A318, A319, A320, A321, A330, A340, A380, Boeing_717, Boeing_737, Boeing_747, Boeing_777, Boeing_BBJ }

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
        protected Flugschreiber flugSchreiber;

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

        public Flugschreiber Flugschreiber
        {
            get { return flugSchreiber; }
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
            //Program.transponder += new TransponderDel(Transpond);
            Program.transponder += Transpond;
        }
        public void Transpond(string kennung, Position pos)
        {
            double abstand = Math.Sqrt(Math.Pow(this.pos.x - pos.x, 2) + Math.Pow(this.pos.y - pos.y, 2));
            //if (kennung.Equals(this.kennung))
            //  Console.WriteLine("{0} an Position x={1}, y={2}, h={3}", kennung, pos.x, pos.y, pos.h);
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
                this.flugSchreiber.Stop();

                Console.WriteLine("\n{0} gelandet (Zieldistanz={1}, Höhendistanz={2}", kennung, Zieldistanz(), pos.h - zielPos.h);
            }

        }

    }

    class Düsenflugzeug : Starrflügelflugzeug
    {
        // CSH03 Lektion 5
        public Airbus typ;
        private int sitzplätze;
        public Düsenflugzeug(string kennung, Position startPos) : base(kennung, startPos)
        {
            bool initialized = this.Starte();
            if (initialized)
            {
                Program.transponder += this.Transpond;
                Program.fliegerRegister += this.Steuern;
            }
        }

        public bool Starte()
        {
            string pfad = ".\\init\\" + kennung + ".init";
            StreamReader reader;
            try
            {
                reader = new StreamReader(File.Open(pfad, FileMode.Open));
            }
            catch (IOException e)
            {
                Console.WriteLine("{0} Fehler beim Zugriff auf die Datei " + pfad, e.GetType().Name);
                return false;
            }
            int[] data = new int[9];
            for (int i = 0; i < 9; i++)
            {
                string str = reader.ReadLine();
                str = str.Substring(str.IndexOf('=') + 1);
                //Zur Kontrolle, später auskommentieren
                //Console.WriteLine(str);
                data[i] = Int32.Parse(str);
            }
            reader.Close();
            this.zielPos.x = data[0];
            this.zielPos.y = data[1];
            this.zielPos.h = data[2];
            streckeProTakt = data[3];
            flughöhe = data[4];
            steighöheProTakt = data[5];
            sinkhöheProTakt = data[6];
            //"typ" aus data[7] initialisieren
            Array typen = Enum.GetValues(typeof(Airbus));
            this.typ = (Airbus)typen.GetValue(data[7]);
            sitzplätze = data[8];
            Console.WriteLine("Flug {0} vom Typ {1} mit {2} Plätzen initialisiert.", kennung, typ, sitzplätze);
            steigt = true;
            Console.WriteLine();

            this.flugSchreiber = new Flugschreiber(this.kennung , typ ,pos , zielPos);
            return true;
        }
    }

    //Flugschreiber Fuer Loesungsaufgaben
    class Flugschreiber
    {
        protected StreamWriter sw;
        protected StreamReader sr;
        protected string logFileName;
        protected string kennung;

        public string LogFileName
        {
            get { return logFileName; }
        }

        public bool IsLoggingActive
        {
            get { return Program.protokollieren; }
        }

        public Flugschreiber(string kennung,Airbus typ,Position pos , Position zielPos)
        {
            this.kennung = kennung;
            if (this.IsLoggingActive)
            {
                this.logFileName = $".//{kennung}_{DateTime.Now.Day}-{DateTime.Now.Hour}-{DateTime.Now.Minute}-{DateTime.Now.Second}.bin";
                this.sw = new StreamWriter(File.Open(this.logFileName, FileMode.Create));

                this.sw.WriteLine($"{kennung} (Typ {typ}) startet an Position {pos.x}-{pos.y}-{pos.h} mit Zielposition {zielPos.x}-{zielPos.y}-{zielPos.h}");
                Program.transponder += this.Transpond;
            }
        }

        public void ESA4Out()
        {
            ESA4Out(logFileName);
        }

        public void ESA4Out(string protokollpfad)
        {
            this.sr = new StreamReader(File.Open(protokollpfad, FileMode.Open));
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
            this.sr.Close();
        }

        public void Transpond(string kennung,Position pos)
        {
            if(kennung.Equals(this.kennung))
                this.sw.WriteLine($"{pos.x,10}\t{pos.y,10}\t{pos.h,10}");
        }

        public void Stop()
        {
            if (this.IsLoggingActive)
            {
                Program.transponder -= this.Transpond;
                this.sw.Close();
            }
        }
    }

    class Program
    {

        public static TransponderDel transponder;
        // Wiederholungsaufgabe 2.3
        public delegate void FliegerRegisterDel();
        public static FliegerRegisterDel fliegerRegister;
        public static bool protokollieren = true;

        public void ProgrammTaken()
        {
            Düsenflugzeug flieger1 = new Düsenflugzeug("LH 500", new Position(3500, 1400, 180));
            Düsenflugzeug flieger2 = new Düsenflugzeug("LH 3000", new Position(3000, 2000, 100));
            Console.WriteLine($"Logging {flieger1.Kennung} to {flieger1.Flugschreiber.LogFileName}");
            Console.WriteLine($"Logging {flieger2.Kennung} to {flieger2.Flugschreiber.LogFileName}");
            Console.WriteLine("\n\n");
            while (fliegerRegister != null)
            {
                fliegerRegister();
                Console.WriteLine();
                Thread.Sleep(1000);
            }
            flieger1.Flugschreiber.ESA4Out();
            flieger2.Flugschreiber.ESA4Out();
        }
        static void Main(string[] args)
        {
            Program test = new Program();
            test.ProgrammTaken();
            Console.ReadLine();
        }
    }
}

using System;
using Microsoft.VisualBasic.CompilerServices;

namespace Lektion4
{
    enum Airbus : short { A300,A310,A318,A319,A320,A321,A330,A340,A350,A380}

    enum Flugzeugtypen : short { Boeing_717, Boeing_737, Boeing_747, Boeing_767, Boeing_777, Boeing_BBJ}
    enum Wochentage: short { Montag,Dienstag,Mittwoch,Donnerstag,Freitag,Samstag,Sonntag}

    struct Point
    {
        public int x, y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
    struct Position
    {
        public int x, y, h;

        public Position(int x, int y, int h)
        {
            this.x = x;
            this.y = y;
            this.h = h;
        }

        public void PositionAendern(int deltaX, int deltaY, int deltaH)
        {
            x = x + deltaX;
            y = y + deltaY;
            h = h + deltaH;
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            Wochentage test = Wochentage.Freitag;
            Console.WriteLine($"Heute ist {test}");
            //Luftfahrzeug flieger = new Luftfahrzeug("LH 4070", new Position(105020, 30800, 110));
            //flieger.Steigen(100);
            //flieger.Sinken(50);

            ////Position pos = new Position(2500,1500,400);
            //Position pos1;
            //pos1.x = 1000;
            //pos1.y = 1000;
            //pos1.h = 500;
            //Luftfahrzeug flieger1 = new Luftfahrzeug("LH 4080", pos1);
            //Console.WriteLine($"Position Flieger 1 :  {pos1.x} - {pos1.y} - {pos1.h}");

            Position pos2 = new Position(1000, 1000, 500);
            Duesenflugzeug flieger2 = new Duesenflugzeug("L H334", pos2,Airbus.A350);
            flieger2.typ = (Airbus)8;
            Console.WriteLine($"Position Flieger 2 Typ {flieger2.typ} {(int)flieger2.typ} :  {pos2.x} - {pos2.y} - {pos2.h}");

            Console.WriteLine($"Typ eine enum-Elemtens {Airbus.A320.GetType()}");
            Console.WriteLine($"Wert eines enum-Elements: {Airbus.A380}");
            Console.WriteLine($"Index eines enum-Elements: {(int)Airbus.A320}");

            Duesenflugzeug flieger3 = new Duesenflugzeug("LH 1333", new Position(1000, 1000, 5000),Airbus.A310);
        }
    }
    class Luftfahrzeug
    {
        
        protected string kennung;
        protected Position pos;
        public Luftfahrzeug() { }

        public Luftfahrzeug(string kennung,Position pos)
        {
            this.kennung = kennung;
            this.pos = pos;
        }

        public void Steigen(int meter)
        {
            pos.PositionAendern(0,0,meter);
            Console.WriteLine(kennung + " steigt " + meter + " Meter neue hoehe "+ pos.h);
        }

        public void Sinken(int meter)
        {
            pos.PositionAendern(0, 0, -meter);
            Console.WriteLine(kennung + " sinkt " + meter + " Meter neue hoehe " + pos.h);
        }
    }

    class Flugzeug : Luftfahrzeug
    {
        public Flugzeugtypen typ;
        public Flugzeug(string kennung,Position pos) : base(kennung,pos)
        {

        }
    }

    class Starrfluegelflugzeug : Flugzeug
    {
        public Starrfluegelflugzeug(string kennung, Position pos) : base(kennung, pos)
        {
        }
    }

    class Duesenflugzeug : Starrfluegelflugzeug
    {
        public Airbus typ;
        public Duesenflugzeug(string kennung, Position pos,Airbus typ) : base(kennung, pos)
        {
            this.typ = typ;
        }
    }
}

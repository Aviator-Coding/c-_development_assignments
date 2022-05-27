using System;

namespace Lektion5
{
    class Program
    {
        static void Main(string[] args)
        {
            //Flugzeug fl = new Flugzeug("test", new Position(100, 100, 10));
            //fl.Steigen(50);
            //fl.Sinken(150);

            Starrfluegelflugzeug fs = new Starrfluegelflugzeug("A1234", new Position(100, 100, 5));
            fs.Steigen(50);
            fs.Sinken(150);


        }

        enum Airbus : short { A300, A310, A318, A319, A320, A321, A330, A340, A350, A380 }

        enum Flugzeugtypen : short { Boeing_717, Boeing_737, Boeing_747, Boeing_767, Boeing_777, Boeing_BBJ }
        enum Wochentage : short { Montag, Dienstag, Mittwoch, Donnerstag, Freitag, Samstag, Sonntag }

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

        interface Itransponder
        {

        }
        abstract class Luftfahrzeug
        {

            protected string kennung;
            protected Position pos;
            public Luftfahrzeug() { }

            public Luftfahrzeug(string kennung, Position pos)
            {
                this.kennung = kennung;
                this.pos = pos;
            }

            public abstract void Steigen(int meter);
            public abstract void Sinken(int meter);
        }

        class Flugzeug : Luftfahrzeug
        {
            public Flugzeugtypen typ;
            public Flugzeug(string kennung, Position pos) : base(kennung, pos)
            {

            }

            public override void Sinken(int meter)
            {
                pos.PositionAendern(0,0,meter);
                Console.WriteLine($"{kennung} steigt {meter} Hoehe {pos.h}");
            }

            public override void Steigen(int meter)
            {
                pos.PositionAendern(0, 0, -meter);
                Console.WriteLine($"{kennung} sinkt {meter} Hoehe {pos.h}");
            }
        }

        class Starrfluegelflugzeug : Flugzeug , Itransponder
        {
            public Starrfluegelflugzeug(string kennung, Position pos) : base(kennung, pos)
            {
                this.Transpond(kennung,pos);
            }

            public void Transpond(string kennung, Position pos)
            {
                Console.WriteLine($"Flieger funkt Kennung {kennung} und Position {pos.x} {pos.y} {pos.h}");
            }
        }

        class Duesenflugzeug : Starrfluegelflugzeug
        {
            public Airbus typ;
            public Duesenflugzeug(string kennung, Position pos, Airbus typ) : base(kennung, pos)
            {
                this.typ = typ;
            }
        }
    }
}

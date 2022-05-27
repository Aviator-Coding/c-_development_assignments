using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aufgabe_26
{
    class Program
    {

        static void Main(string[] args)
        {
            List<Form> LForm = new List<Form>();
            LForm.Add(new Kreis(20));
            LForm.Add(new Quadrat(20));
            LForm.Add(new Rechteck(20,50));

            foreach (var form in LForm)
            {
                form.CalcFlaeche();
                form.CalcUmfang();
            }

            Console.ReadLine();
        }
    }

    abstract class Form
    {
        protected double Umfang { get;set; }
        protected double Flaeche { get; set; }
        public virtual void CalcUmfang()
        {
            Console.WriteLine($"Der Umfang betraegt {Umfang}");
        }

        public virtual void CalcFlaeche()
        {
            Console.WriteLine($"Die Flaeche betraegt {Flaeche}");
        }
    }

    class Kreis : Form
    {
        private int Radius { get; set; }
        public Kreis(int radius)
        {
            this.Radius = radius;
        }

        public override void CalcUmfang()
        {
            this.Umfang = 2 * Math.PI * this.Radius;
            base.CalcUmfang();
        }

        public override void CalcFlaeche()
        {
            this.Flaeche = Math.Pow((double)this.Radius , 2) * Math.PI;
            base.CalcFlaeche();
        }
    }

    class Quadrat : Form
    {
        private int Size { get; set; }

        public Quadrat(int size)
        {
            this.Size = size;
        }
        public override void CalcUmfang()
        {
            this.Umfang = this.Size+ this.Size+ this.Size+ this.Size;
            base.CalcUmfang();
        }

        public override void CalcFlaeche()
        {
            this.Flaeche = Math.Pow((double)this.Size, 2);
            base.CalcFlaeche();
        }
    }

    class Rechteck : Form
    {
        private int Hoehe { get; set; }
        private int Breite { get; set; }

        public Rechteck(int hoehe, int breite)
        {
            this.Hoehe = hoehe;
            this.Breite = breite;
        }

        public override void CalcUmfang()
        {
            this.Umfang = this.Hoehe + this.Breite + this.Hoehe + this.Breite;
            base.CalcUmfang();
        }

        public override void CalcFlaeche()
        {
            this.Flaeche = this.Hoehe * this.Breite;
            base.CalcFlaeche();
        }
    }
}

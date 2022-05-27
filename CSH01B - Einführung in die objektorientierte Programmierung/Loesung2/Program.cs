using System;

namespace ESA2
{
    interface Interface
    {
        void TuNochWas();
    }

    abstract class A
    {
        public abstract void TuWas();
    }

    class B : A, Interface
    {

        //Dies setzt TuWas von Klasse a ausser Kraft. und benutzt seine eigene Tuwas 
        // Methode
        public override void TuWas()
        {
            Console.WriteLine("Ich tu was");
        }

        //Dadurch das wir ein Interface hier haben muessesn deren Funktionen auch implementiert
        //werden
        public void TuNochWas()
        {
            Console.WriteLine("Ich Tu noch etwas");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {

            B bObjekt = new B();
            bObjekt.TuWas();

        }
    }

}

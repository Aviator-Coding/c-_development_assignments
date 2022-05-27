using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lektion_04
{
    struct content
    {
        public content(string content,string type)
        {
            this.Content = content;
            this.Type = type;
        }
        public string Content { get; private set; }
        public string Type {   get; private set; }

    }

    class Program
    {
        void NewElement()
        {
            XElement box = new XElement("box",
            new XElement("card",
            new XElement("keyword", "Funktionale Konstruktion"),
            new XElement("abstract", "Zusammenfassung"),
            new XElement("content", "Konstruktion im Code wie im XML-Dokument"),
            new XElement("createDate", DateTime.Now)
            ));
            var result = from e in box.Elements()
                         select e;
            foreach (var r in result)
                Console.WriteLine(r);
            /* Ausgabe:
            <card>
            <keyword>Funktionale Konstruktion</keyword>
            <abstract>Zusammenfassung</abstract>
            <content>Konstruktion im Code wie im XML-Dokument</content>
            <createDate>2010-05-20T16:14:57.765625+02:00</createDate>
            </card>
            */
        }
        void TestXName()
        {
            XNamespace ns = "http://www.linq-to-xml.de";
            XElement element1 = new XElement("card");
            Console.WriteLine(element1);
            Console.WriteLine("LocalName={0}, Namespace={1}",
            element1.Name.LocalName, element1.Name.Namespace);
            XElement element2 = new XElement(ns + "card", new
            XAttribute("type", "text"));
            Console.WriteLine(element2);
            Console.WriteLine("LocalName={0}, Namespace={1}",
            element2.Name.LocalName, element2.Name.Namespace);
        }

        void AddCardElement(XElement card, XElement toAdd)
        {
            string[] multipleElements = { "keyword", "content" };
            XElement element = card.Descendants(toAdd.Name).Last();
            if (multipleElements.Contains(toAdd.Name.ToString()))
            {
                element.AddAfterSelf(toAdd);
                // Datum aktualisieren
                card.SetElementValue("date", DateTime.Now.ToString("dd.MM.yyyy HH:mm"));
            }
            else
                Console.WriteLine("FEHLER in AddCardElement: Das Element \"{0}\" ist nur einmal zugelassen", toAdd.Name.ToString());
        }

        // Adds a Kezword to a Card
        void AddKeyWord(XElement card,string keyword)
        {
            card.Add(new XElement("keyword", keyword));
        }

        // Adds the Content to the Card
        void AddContent(XElement card, content con)
        {
            card.Add(new XElement("content", con.Content, new XAttribute("type", con.Type)));
        }

        //Entwerfen Sie eine Methodenvariante von NewCard, mit deren Aufruf ein vollständiges
        //card-Element mit mehreren Stichworten und mehreren content-Elementen erstellt werden kann.
        //Schreiben Sie auch einen passenden exemplarischen Aufruf mit zumindest
        //zwei Stichworten und content-Elementen für jeden Typ.
        //Erstellen Sie mit dem so produzierten Element ein XDocument-Objekt (Methode NewDocument)
        //und geben Sie dessen Elementstruktur auf Konsole aus.Die Ausgabe dokumentieren Sie bitte durch einen Screenshot.

        XElement NewCard(string subject, string[] keywords,
            string @abstract, content[] contents, string type, string source)
        {
            // Create the Card as defined
            XElement card = new XElement("card",
                new XAttribute("subject", subject),
                //new XElement("keyword", keyword),
                new XElement("abstract", @abstract),
                //new XElement("content", content,
               //     new XAttribute("type", type)),
                new XElement("source", source),
                new XElement("createDate",
                    DateTime.Now.ToString("dd.MM.yyyy HH:mm")),
                new XElement("date",
                    DateTime.Now.ToString("dd.MM.yyyy HH:mm"))
            );

            // Add Keywords Element to Card
            foreach(string keyword in keywords)
            {
                this.AddKeyWord(card, keyword);
            }

            foreach(content con in contents)
            {
                this.AddContent(card, con);
            }


            return card;
        }

        static void Main(string[] args)
        {
            Program pr = new Program();
            var xml = pr.NewCard("subject", new string[] { "test", "test" }, "abstract", new content[] { new content("test","link") }, "type", "source") ;
            Console.WriteLine(xml);
            Console.ReadLine();
        }
    }
}

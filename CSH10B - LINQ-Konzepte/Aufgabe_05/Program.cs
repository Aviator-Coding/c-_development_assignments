using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Aufgabe_05
{
    class Program
    {
        static void Main(string[] args)
        {
            Box box = new Box();
            box.AddCard(new Card("subject", 
                                new string[] { 
                                    "test 1", 
                                    "test 2" }, 
                                "abstract", 
                                new content[] { 
                                    new content("I am a Titel", Type.titel), 
                                    new content("I am a url", Type.url) }, 
                                "type", 
                                "source"));

            box.AddCard(new Card("subject",
                                new string[] {
                                    "test 1",
                                    "test 2" },
                                "abstract",
                                new content[] {
                                    new content("I am a Titel", Type.titel),
                                    new content("I am a url", Type.url) },
                                "type",
                                "source"));

            box.AddCard(new Card("subject",
                    new string[] {
                                    "test 1",
                                    "test 2" },
                    "abstract",
                    new content[] {
                                    new content("I am a Titel", Type.titel),
                                    new content("I am a code", Type.code),
                                    new content("I am a image", Type.image),
                                    new content("I am a url", Type.url) },
                    "type",
                    "source"));

            box.getXML();
            Console.ReadLine();
        }
    }

    struct content
    {
        public content(string content, Type type)
        {
            this.Content = content;
            this.Type = type;
        }
        public string Content { get; private set; }
        public Type Type { get; private set; }

    }

    enum Type { titel, text, quote, code, url, image }

    class Box
    {
        XDocument doc = new XDocument();
        List<Card> cards = new List<Card>();

        public void AddCard(Card card)
        {
            this.cards.Add(card);
        }

        public void getXML()
        {
            XElement root = new XElement("box");

            foreach(Card card in cards)
            {
                root.Add(card.card);
            }

            doc.Add(root);
            Console.WriteLine(doc);
        }
    }

    class Card
    {
        public XElement card { get; private set; }
        public Card(string subject, string[] keywords, string @abstract, content[] contents, string type, string source)
        {
            // Create the Card as defined
            this.card = new XElement("card",
                new XAttribute("subject", subject),
                new XElement("abstract", @abstract),
                new XElement("source", source),
                new XElement("createDate",
                    DateTime.Now.ToString("dd.MM.yyyy HH:mm")),
                new XElement("date",
                    DateTime.Now.ToString("dd.MM.yyyy HH:mm"))
            );

            // Add Keywords Element to Card
            foreach (string keyword in keywords)
            {
                card.Add(new XElement("keyword", keyword));
            }

            foreach (content con in contents)
            {
                card.Add(new XElement("content", con.Content, new XAttribute("type", con.Type)));
            }
        }
    }
}

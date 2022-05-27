using System;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Aufgabe_30_LinqToObjects
{

    [Table(Name = "angestellte")]
    class Angestellter
    {
        [Column(Name = "id", IsPrimaryKey = true)]
        public int Id { get; set; }
        [Column(Name = "abtID")]
        public int AbtId { get; set; }
        [Column(Name = "versicherungsnr")]
        public string VersicherungsNr { get; set; }
        [Column(Name = "gehalt")]
        public double Gehalt { get; set; }
        [Column(Name = "einstellungsdatum")]
        public DateTime EinstellungsDatum { get; set; }
        [Column(Name = "ausscheidedatum")]
        public DateTime AusscheideDatum { get; set; }
        [Column(Name = "geburtsdatum")]
        public DateTime GeburtsDatum { get; set; }
        [Column(Name = "geschlecht")]
        public string Geschlecht { get; set; }
        [Column(Name = "personID")]
        public int PersonId { get; set; }

    }

    [Table(Name = "artikel")]
    class Artikel
    {
        [Column(Name = "id", IsPrimaryKey = true)]
        public int Id { get; set; }
        [Column(Name = "name")]
        public string Name { get; set; }
        [Column(Name = "beschreibung")]
        public string Beschreibung { get; set; }
        [Column(Name = "groesse")]
        public string Groesse { get; set; }
        [Column(Name = "farbe")]
        public string Farbe { get; set; }
        [Column(Name = "menge")]
        public int Menge { get; set; }
        [Column(Name = "preis")]
        public double Preis { get; set; }
    }

    [Table(Name = "bestellpositionen")]
    class Bestellposition
    {
        [Column(Name = "bestellID", IsPrimaryKey = true)]
        public int BestellId { get; set; }
        [Column(Name = "artikelID", IsPrimaryKey = true)]
        public int ArtikelId { get; set; }
        [Column(Name = "menge")]
        public int Menge { get; set; }
        [Column(Name = "lieferdatum")]
        public DateTime LieferDatum { get; set; }
    }

    [Table(Name = "bestellungen")]
    class Bestellung
    {
        [Column(Name = "id", IsPrimaryKey = true)]
        public int Id { get; set; }
        [Column(Name = "kundenID")]
        public int KundenId { get; set; }
        [Column(Name = "datum")]
        public DateTime Datum { get; set; }
        [Column(Name = "verkaeuferID")]
        public int VerkaeuferId { get; set; }
    }

    [Table(Name = "kontakte")]
    class Kontakt
    {
        [Column(Name = "id", IsPrimaryKey = true)]
        public int Id { get; set; }
        [Column(Name = "personID")]
        public int PersonId { get; set; }
    }

    [Table(Name = "kunden")]
    class Kunde
    {
        [Column(Name = "id", IsPrimaryKey = true)]
        public int Id { get; set; }
        [Column(Name = "firma")]
        public string Firma { get; set; }
        [Column(Name = "personID")]
        public int PersonId { get; set; }
    }

    [Table(Name = "lieferanten")]
    class Lieferant
    {
        [Column(Name = "id", IsPrimaryKey = true)]
        public int Id { get; set; }
        [Column(Name = "personID")]
        public int PersonId { get; set; }
        [Column(Name = "firma")]
        public string Firma { get; set; }
    }

    public class Person
    {
        public object Value { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public override string ToString()
        {
            return $"{Vorname} {Nachname}";
        }
    }

    public class Abteilung
    {
        public object Value { get; set; }
        public string Name { get; set; }
        public override string ToString()
        {
            return $"{Name}";
        }
    }
}

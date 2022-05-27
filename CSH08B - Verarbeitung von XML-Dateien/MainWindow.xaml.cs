using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Schema;

namespace Lektion___03
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private XmlNode kontextNode;
        private XmlNode tempNode;
        private XmlDocument xmldoc = new XmlDocument();
        private string newLine = Environment.NewLine;
        private char tab = '\x0009';

        public MainWindow()
        {
            InitializeComponent();
            try
            {
                // XML Reader Benoetigt extra Einstellungen um DTD zu verarbeiten
                XmlReaderSettings settings = new XmlReaderSettings();
                // Verarbeitung von DTD erlauben (dies is deaktiviert bei default)
                settings.DtdProcessing = DtdProcessing.Parse;
                // Validate the DTD damit wir auch wissen ob Sie Valide sind
                settings.ValidationType = ValidationType.DTD;
                //XmlUrlResolver wird verwendet, um externe XML-Ressourcen wie z. b. Entitäten, Dokumenttyp Definitionen (DTDs)
                //oder Schemas aufzulösen. Außerdem werden Sie verwendet, um include-und Import-Elemente in XSL-Stylesheets
                //(Extensible Stylesheet Language) oder XSD-Schemas (XML Schema Definition Language) zu verarbeiten.
                settings.XmlResolver = new XmlUrlResolver();
                // Validierungs Event
                settings.ValidationEventHandler += new ValidationEventHandler(ValidationCallBack);

                // XML Reader wird benoetig fuer  das XMLDoc 
                XmlReader reader = XmlReader.Create("Kartei.xml", settings);
                xmldoc.Load(reader);
                kontextNode = xmldoc;
                tbAusgabe.Text = AusgabeString(kontextNode);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.GetType()} - {ex.Message}");
                statusBarText.Content = ex.GetType().ToString();
            }
        }

        // Callback Funktion fuer Fehler beim validieren
        private static void ValidationCallBack(object sender, ValidationEventArgs e)
        {
            Console.WriteLine("Validation Fehler: {0}", e.Message);
        }

        private void parent_Click(object sender, RoutedEventArgs e)
        {
            tempNode = kontextNode.ParentNode;
            Ausgabe("ParentNode");
        }

        private void child_Click(object sender, RoutedEventArgs e)
        {
            tempNode = kontextNode.FirstChild;
            Ausgabe("FirstChild");
        }

        private void next_Click(object sender, RoutedEventArgs e)
        {
            tempNode = kontextNode.NextSibling;
            Ausgabe("NextSibling");
        }

        private void previous_Click(object sender, RoutedEventArgs e)
        {
            tempNode = kontextNode.PreviousSibling;
            Ausgabe("PreviousSibling");
        }

        private void x_Click(object sender, RoutedEventArgs e)
        {
            tbAusgabe.Text = AusgabeString(kontextNode);
        }

        private void Ausgabe(string node)
        {
            if (tempNode == null)
            {
                tbAusgabe.Text = $"Es gibt kein {node} zu {kontextNode.NodeType} \"{kontextNode.Name}\"";
            }
            else
            {
                kontextNode = tempNode;
                tbAusgabe.Text = AusgabeString(kontextNode);
            }
        }

        private string AusgabeString(XmlNode kontextNode)
        {
            string retString = $"NodeType:{kontextNode.NodeType} {newLine}Name:{kontextNode.Name} {newLine}";
            switch (kontextNode.NodeType)
            {
                case XmlNodeType.Document:
                    retString += $"URL:{xmldoc.BaseURI}";
                    break;
                case XmlNodeType.DocumentType:
                    XmlDocumentType docType = xmldoc.DocumentType;

                    if(docType.InternalSubset != null)
                     retString += $"InternalSubset:{docType.InternalSubset.Trim()} {newLine}";

                    if(docType.PublicId != null)
                        retString += $"publicID: {docType.PublicId}  {newLine}";

                    if (docType.SystemId != null)
                        retString += $"systemID: {docType.SystemId}  {newLine}";
                    break;
                case XmlNodeType.Element:
                    for (int i = 0; i < kontextNode.Attributes.Count; i++)
                    {
                        if (i == 0)
                            retString += "Attribut (e)";
                        else
                        {
                            retString += tab;
                            retString += tab;
                        }

                        retString += kontextNode.Attributes.Item(i).Name;
                        retString += $"={kontextNode.Attributes.Item(i).Value} {newLine}";
                    }
                    break;
                case XmlNodeType.EntityReference:
                    break;
                default:
                    retString += "Value:    ";
                    retString += kontextNode.Value;
                    break;
            }

            return retString;
        }

        private void btnChilds_Click(object sender, RoutedEventArgs e)
        {
            string ausgabe = "";
            if (kontextNode.HasChildNodes)
            {
                for (int i = 0; i < kontextNode.ChildNodes.Count; i++){
                    ausgabe += kontextNode.ChildNodes[i].OuterXml + newLine;
                }
                tbAusgabe.Text = ausgabe;
            }
            else
            {
                tbAusgabe.Text = $"{kontextNode.Name} hat keine ChildNodes";
            }
        }

        private void btnTagName_Click(object sender, RoutedEventArgs e)
        {
            if(tbByTagName.Text.Length == 0)
            {
                statusBarText.Content = "Bitte einen Tag Namen angeben";
            }
            else
            {
                statusBarText.Content = "";
                string tagName = tbByTagName.Text.Trim();
                string ausgabe = $"Eintraege zum Elementnamen {tagName}\":{newLine}{newLine}";
                XmlNodeList nodelist = xmldoc.GetElementsByTagName(tagName);
                for(int i = 0; i < nodelist.Count; i++)
                {
                    ausgabe += nodelist[i].InnerText + newLine;
                }
                tbAusgabe.Text = ausgabe;
                tbByTagName.Text = "";
            }
        }

        private void btnRoot_Click(object sender, RoutedEventArgs e)
        {
            kontextNode = xmldoc.DocumentElement;
            tbAusgabe.Text = AusgabeString(kontextNode);
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnElementChild_Click(object sender, RoutedEventArgs e)
        {
            XmlElement element = xmldoc.CreateElement(tbEingabe.Text);
            try
            {
                kontextNode.AppendChild(element);
                tbAusgabe.Text = AusgabeString(kontextNode);
                statusBarText.Content = $"Kind-Element zu \"{kontextNode.Name}\" eingefuegt";
            }catch(Exception ex)
            {
                Console.WriteLine($"{ex.GetType()} - {ex.Message}");
                statusBarText.Content = ex.GetType().ToString();
            }
        }

        private void btnElementBefore_Click(object sender, RoutedEventArgs e)
        {
            XmlElement element = xmldoc.CreateElement(tbEingabe.Text);
            try
            {
                kontextNode.ParentNode.InsertBefore(element,kontextNode);
                tbAusgabe.Text = AusgabeString(kontextNode);
                statusBarText.Content = $"Kind-Element zu \"{kontextNode.Name}\" eingefuegt";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.GetType()} - {ex.Message}");
                statusBarText.Content = ex.GetType().ToString();
            }
        }

        private void btnElemenAfter_Click(object sender, RoutedEventArgs e)
        {
            XmlElement element = xmldoc.CreateElement(tbEingabe.Text);
            try
            {
                kontextNode.ParentNode.InsertAfter(element, kontextNode);
                tbAusgabe.Text = AusgabeString(kontextNode);
                statusBarText.Content = $"Kind-Element zu \"{kontextNode.Name}\" eingefuegt";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.GetType()} - {ex.Message}");
                statusBarText.Content = ex.GetType().ToString();
            }
        }

        private void btnText_Click(object sender, RoutedEventArgs e)
        {
            XmlText textNode = xmldoc.CreateTextNode(tbEingabe.Text);
            try
            {
                kontextNode.AppendChild(textNode);
                statusBarText.Content = $"Text zu \"{kontextNode.Name}\" hinzugefuegt";
            }
            catch(Exception ex)
            {
                Console.WriteLine($"{ex.GetType()} - {ex.Message}");
                statusBarText.Content = ex.GetType().ToString();
            }
        }

        private void btnCData_Click(object sender, RoutedEventArgs e)
        {
            XmlCDataSection cdatnode = xmldoc.CreateCDataSection(tbEingabe.Text);
            try
            {
                kontextNode.AppendChild(cdatnode);
                statusBarText.Content = $"Text zu \"{kontextNode.Name}\" hinzugefuegt";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.GetType()} - {ex.Message}");
                statusBarText.Content = ex.GetType().ToString();
            }
        }

        private void btnErsetzen_Click(object sender, RoutedEventArgs e)
        {
            XmlNode node = null;
            if (kontextNode.FirstChild.NodeType.Equals(XmlNodeType.Text))
            {
                node = xmldoc.CreateTextNode(tbEingabe.Text);
            }else if (kontextNode.FirstChild.NodeType.Equals(XmlNodeType.CDATA))
            {
                node = xmldoc.CreateCDataSection(tbEingabe.Text);
            }

            if(node != null)
            {
                try
                {
                    kontextNode.ReplaceChild(node, kontextNode.FirstChild);
                    statusBarText.Content = $"Inhalt von \"{kontextNode.Name}\" ersetzt";
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.GetType()} - {ex.Message}");
                    statusBarText.Content = ex.GetType().ToString();
                }
            }
            else
            {
                statusBarText.Content = "Die Text-Node Liess sich nicht erzeugen";
            }
        }

        private void btnAttribute_Click(object sender, RoutedEventArgs e)
        {
            string attributeName = tbName.Text;
            string attributeValue = tbValue.Text;
            if (attributeName == "") {
                statusBarText.Content = "Bitte einen Attributsnamen eingeben";
                return;
             }

            if(attributeValue == "")
            {
                statusBarText.Content = "Bitte einen Attribut Value eingeben";
                return;
            }


            XmlAttribute attribute = xmldoc.CreateAttribute(attributeName);
            attribute.Value = attributeValue;

            try
            {
                ((XmlElement)kontextNode).SetAttributeNode(attribute);
                tbAusgabe.Text = $"Attribut \"{attributeName}\"=\"{attributeValue}\" fuer \"{kontextNode.Name}\" gesetzt";
                statusBarText.Content = "";
            }
            catch(Exception ex)
            {
                Console.WriteLine($"{ex.GetType()} - {ex.Message}");
                statusBarText.Content = ex.GetType().ToString();
            }
        }

        private void btnEntfernen_Click(object sender, RoutedEventArgs e)
        {
            string name = tbName.Text;
            if (name == "")
                statusBarText.Content = "Bitte einen Attributsnamen eingeben";
            else
            {
                try
                {
                    ((XmlElement)kontextNode).RemoveAttribute(name);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.GetType()} - {ex.Message}");
                    statusBarText.Content = ex.GetType().ToString();
                }
            }
        }
    }
}

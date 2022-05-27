using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Win32;

namespace Aufgabe_28
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string FileName { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        //Closes the Current Window
        private void mClick_Close_Window(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control)
            {
                switch (e.Key)
                {
                    case Key.S:
                        mFileSave();
                        e.Handled = true;
                        break;
                    case Key.N:
                        if (MessageBox.Show("Sind Sie Sicher das sie eine Neues Dokument erstellen wollen", "Neues Dokument...", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            this.mFileNew();
                            e.Handled = true;
                        }
                        break;
                    case Key.O:
                        this.mFileNew();
                        e.Handled = true;
                        break;
                    case Key.C:
                        MessageBox.Show("kopieren....");
                        break;
                    case Key.V:
                        MessageBox.Show("einfuegen....");
                        break;
                    case Key.X:
                        MessageBox.Show("Ausschneiden....");
                        break;
                }
            }

           
        }


        private void mFileNew()
        {
            FileName = String.Empty;
            RTextEditor.Document.Blocks.Clear();
            LStatusBar.Content = $"Neues Dokument Erstellt";
        }

        private void mFileOpen()
        {
            OpenFileDialog OpenDialog = new OpenFileDialog();
            OpenDialog.DefaultExt = "*.rtf";
            OpenDialog.Filter = "Rich Text Dokumente|*.rtf";

            if (OpenDialog.ShowDialog() == true)
            {
                this.FileName = OpenDialog.FileName;
                if (!File.Exists(this.FileName))
                {
                    LStatusBar.Content = $"Dokument kann nicht geoeffnet werden {this.FileName} existiert nicht";
                }

                FileStream fileStream = new FileStream(this.FileName, FileMode.Open);
                TextRange textRange = new TextRange(RTextEditor.Document.ContentStart, RTextEditor.Document.ContentEnd);
                textRange.Load(fileStream, DataFormats.Rtf);
                fileStream.Close();
                LStatusBar.Content = $"Dokument {this.FileName} geoeffnet";
            }


        }

        private void mFileSave()
        {
            if (FileName == String.Empty)
            {
                mFileSaveAs();
                return;
            }

            FileStream fileStream = new FileStream(this.FileName,FileMode.Create);
            TextRange textRange = new TextRange(RTextEditor.Document.ContentStart, RTextEditor.Document.ContentEnd);
            textRange.Save(fileStream,DataFormats.Rtf);
            fileStream.Close();
            LStatusBar.Content = $"Dokument gespeichert unter {FileName} - {DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}";

            
        }

        private void mFileSaveAs()
        {
            SaveFileDialog SaveDialog = new SaveFileDialog();
            SaveDialog.DefaultExt = "*.rtf";
            SaveDialog.Filter = "Rich Text Dokumente|*.rtf";

            if (SaveDialog.ShowDialog() == true)
            {
                this.FileName = SaveDialog.FileName;
                mFileSave();
            }
        }

        private void mFileNew(object sender, RoutedEventArgs e)
        {
            mFileNew();
        }

        private void mFileOpen(object sender, RoutedEventArgs e)
        {
            mFileOpen();
        }

        private void mFileSave(object sender, RoutedEventArgs e)
        {
            mFileSave();
        }

        private void mFileSaveAs(object sender, RoutedEventArgs e)
        {
            mFileSaveAs();
        }

        private void RichTextUpdate(object sender, KeyEventArgs e)
        {
            LStatusBar.Content = $"Datei wurde geaendert";
        }
    }
}

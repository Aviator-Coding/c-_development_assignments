using System;
using System.IO;

namespace IO_Filesystem_01
{
    class Program
    {
        public void BinaryWriter(string pfad, int[] content)
        {
            BinaryWriter bw = new BinaryWriter(File.Open(pfad, FileMode.Create));
           for(int i =0; i < content.Length; i++)
            {
                bw.Write(content[i]);
            }
            bw.Close();

        }

        public void BinaryRead(string path, int length)
        {
            BinaryReader br = new BinaryReader(File.Open(path, FileMode.Open));
            bool goOn = true;
            while(goOn)
            {
                try
                {
                    Console.WriteLine(br.ReadInt32());
                }
                catch (EndOfStreamException e)
                {
                    Console.WriteLine(e.Message);
                    goOn = false;
                }
            }
            br.Close();
        }

        public void DateiErstellen(string pfad,byte[] array)
        {
            FileStream stream = File.Open(pfad, FileMode.Create);
            stream.Write(array, 0, array.Length);
            stream.Close();

        }

        public void DateiLesen(string pfad)
        {
            FileStream fs = File.Open(pfad, FileMode.Create);
            byte[] array = new byte[fs.Length];
            fs.Read(array, 0, (int)fs.Length);
            for(int i=0;i<array.Length; i++)    
            {
                Console.Write((char)array[i]);
            }
            Console.WriteLine();
            fs.Close();
        }

        public void WriterNutzen(string pfad,string content)
        {
            StreamWriter sw = new StreamWriter(File.Open(pfad, FileMode.Create));
            sw.WriteLine(content);
            sw.Close();
        }

        public void ReaderNutzen(string pfad)
        {
            StreamReader sr = new StreamReader(File.Open(pfad, FileMode.Open));
            string line;
            while((line = sr.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
            
            sr.Close();
        }



        static void Main(string[] args)
        {
            Program test = new Program();
            string fileName = @"D:\CSharp\CSH03\IO_Filesystem_01\file\Musterdatei.txt";
            byte[] array = { 68, 97, 116, 101, 105 };
            string content = @"Dieser Text enthaelt 
neue zeilen
e
e
e
f
sdf
sdfsdfsdfs
dsfsd
";
            int[] pos = { 1400, 3250, 280 };
            //test.WriterNutzen(fileName, content);
            //test.ReaderNutzen(@"D:\CSharp\CSH03\IO_Filesystem_01\file\Musterdatei.bin");
            //test.DateiErstellen(fileName,array);
            // test.DateiLesen(fileName);
            //test.BinaryWriter(@"D:\CSharp\CSH03\IO_Filesystem_01\file\Musterdatei.bin", pos);
            test.BinaryRead(@"D:\CSharp\CSH03\IO_Filesystem_01\file\Musterdatei.bin", 5);

        }
    }
}

using System;
using System.IO;

namespace ESA_2
{
    class Program
    {
        static void Main(string[] args)
        {

            ESA2 esa = new ESA2("D:\\CSharp\\CSH03\\Lektion_01\\ESA2.txt");
            esa.Data = new byte[]{      32, 32, 67, 67, 32, 32, 32, 35, 32, 35, 32,
                                        32, 67, 32, 32, 67, 32, 32, 35, 32, 35, 32,
                                        67, 32, 32, 32, 32, 32, 35, 35, 35, 35, 35,
                                        67, 32, 32, 32, 32, 32, 32, 35, 32, 35, 32,
                                        67, 32, 32, 32, 32, 32, 35, 35, 35, 35, 35,
                                        32, 67, 32, 32, 67, 32, 32, 35, 32, 35, 32,
                                        32, 32, 67, 67, 32, 32, 32, 35, 32, 35, 32 };
            esa.ESA2In();
            esa.ESA2Out();
        }
    }

    class ESA2
    {
        protected byte[] data;
        private string path;

        public ESA2(string spath)
        {
            path = spath;
        }

        public byte[] Data
        {
            set
            {
                this.data = value;
            }
        }

        public void ESA2In()
        {
            BinaryWriter bw = new BinaryWriter(File.Open(this.path, FileMode.Create));
            for (int i =0; i<this.data.Length; i++)
            {
                bw.Write(this.data[i]);
            }
            bw.Close();
        }

        public void ESA2Out()
        {
            bool goOn = true;
            int i = 1;
            BinaryReader br = new BinaryReader(File.Open(this.path, FileMode.Open));
            while (goOn)
            {
                try
                {
                    Console.Write($"{br.ReadByte()} ");
                    if (i % 11 == 0)
                    {
                        Console.Write("\n");
                    }
                    i++;
                }
                catch (EndOfStreamException e)
                {
                   // Console.WriteLine(e.Message);
                    goOn = false;
                }
            }
        }
    }
}

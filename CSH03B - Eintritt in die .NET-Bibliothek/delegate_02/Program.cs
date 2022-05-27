using System;

namespace delegate_02
{
    public delegate void MessageBroadcast(string message);
    class Program
    {
        public static MessageBroadcast broadcast;
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            AOL_Messanger aol = new AOL_Messanger();
            ICQMessanger icq = new ICQMessanger();

            Program.broadcast += new MessageBroadcast(aol.Message);
            Program.broadcast += new MessageBroadcast(icq.Message);

            Program.broadcast("Hallo Messanger");
        }
    }

    class AOL_Messanger{

      public void Message(string msg)
        {
            Console.WriteLine($"AOL Messanger: {msg}");
        }
        }


    class ICQMessanger
    {

        public void Message(string msg)
        {
            Console.WriteLine($"ICQ Messanger: {msg}");
        }
    }
}

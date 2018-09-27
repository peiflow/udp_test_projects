using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace AES_UI_V1
{
    public class UDP_RX
    {
        public byte[] data;
        public static bool fan1Res;
        Updater updater;
        public static Image fan1 { get; set; }


        public UDP_RX()
        {
            updater = new Updater();
        }

        public void Listen()
        {
            UdpClient client = new UdpClient(1020);
            IPEndPoint serverEP = new IPEndPoint(IPAddress.Any, 1020);

            while (true)
            {
                Console.WriteLine("Listening");
                byte[] d = client.Receive(ref serverEP);
                RaiseDataReceived(new ReceivedDataArgs(serverEP.Address, serverEP.Port, d));
                foreach (var item in d)
                {
                    Console.Write(item);
                }
                Console.Write(Environment.NewLine);
                Console.Write(Environment.NewLine);
            }
        }

        public delegate void DataReceived(object sender, ReceivedDataArgs args);

        public event DataReceived DataReceivedEvent;

        private void RaiseDataReceived(ReceivedDataArgs args)
        {
            if (DataReceivedEvent != null)
                DataReceivedEvent(this, args);
        }
    }
}

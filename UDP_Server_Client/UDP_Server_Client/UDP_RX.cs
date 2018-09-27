using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UDP_Server_Client
{
    class UDP_RX
    {
        public void Listen()
        {
            UdpClient client = new UdpClient(1020);
            IPEndPoint serverEP = new IPEndPoint(IPAddress.Any, 1020);

            while (true)
            {
                Console.WriteLine("Listened: " + DateTime.Now.ToLongTimeString());
                byte[] data = client.Receive(ref serverEP);
                foreach(byte b in data)
                {
                    Console.Write(b);
                }
                Console.WriteLine(Environment.NewLine+ "----------");

                RaiseDataReceived(new ReceivedDataArgs(serverEP.Address, serverEP.Port, data));
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

    internal class ReceivedDataArgs
    {
        public IPAddress ipAddress { get; set; }
        public int Port { get; set; }
        public byte[] ReceivedBytes;

        public ReceivedDataArgs(IPAddress iPAddress, int port, byte[] data)
        {
            this.ipAddress = iPAddress;
            this.Port = port;
            this.ReceivedBytes = data;
        }
    }

    internal class HandleDataClass
    {
        public void SuscribeToEvent(UDP_RX reciever)
        {
            reciever.DataReceivedEvent += receiver_DataReceivedEvent;
        }

        void receiver_DataReceivedEvent(object sender, ReceivedDataArgs args)
        {
            Console.WriteLine("Received message from [{0}:{1}]:" +
                Environment.NewLine + "{2}",
                args.ipAddress.ToString(),
                args.Port.ToString(),
                "Length: "+
                args.ReceivedBytes.Length,
                Environment.NewLine + "----------");
        }
    }
}

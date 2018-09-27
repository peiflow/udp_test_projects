using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UDP_Server_Client
{
    class UDP_TX
    {
        internal void SendMsg()
        {
            UdpClient c = new UdpClient();
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1020);
            Random rdm = new Random();

            c.Connect(ep);

            while (true)
            {
                Debug.WriteLine("Sent: " + DateTime.Now.ToLongTimeString());
                byte[] data = new byte[18];                

                var p = rdm.Next(0, 2);
                var v = rdm.Next(1, 10);

                data[p] = Convert.ToByte(rdm.Next(1, 10) <= 5 ? 0 : 1);

                foreach(byte b in data)
                    Debug.Write(b);

                c.Send(data, data.Length);
                Debug.WriteLine(Environment.NewLine + "----------");
                Thread.Sleep(5000);
            }
        }
    }
}

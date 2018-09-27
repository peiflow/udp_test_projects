using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AES_UI_V1
{
    public class UDP_TX
    {
        UdpClient client;
        IPEndPoint ep;

        public UDP_TX()
        {
            client = new UdpClient();
            ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1030);

            client.Connect(ep);
        }

        public void SendData(byte[] sRX)
        {
            try
            {
                client.Send(sRX, sRX.Length);
                foreach (var item in sRX)
                {                    
                    Debug.Write(item);
                }
                Debug.Write(Environment.NewLine);
            }
            catch (Exception ex) {
                Debug.WriteLine(ex.ToString());
            }
        }
    }
}

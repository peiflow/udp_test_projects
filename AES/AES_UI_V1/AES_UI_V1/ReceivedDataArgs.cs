using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AES_UI_V1
{
    public class ReceivedDataArgs : EventArgs
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
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UDP_Server_Client
{
    class Program
    {
        static void Main(string[] args)
        {
            UDP_RX rX = new UDP_RX();
            UDP_TX tX = new UDP_TX();
            HandleDataClass handleDataClass = new HandleDataClass();

            Thread txTask = new Thread(() => {
                tX.SendMsg();
            });
            txTask.Start();

            Thread rxTask = new Thread(() => {
                rX.Listen();
            });
            rxTask.Start();

            Thread dataHandlerThread = new Thread(() => {
                handleDataClass.SuscribeToEvent(rX);
            });
            dataHandlerThread.Start();

            while (true)
            {
                Thread.Sleep(100);
            }
        }
    }
}

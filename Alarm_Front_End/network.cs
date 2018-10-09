using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// time data dll
using TimeDataDLL;

// Sockets
using System.Net.Sockets;
using System.Net;

// Threads
using System.Threading;


// byte data serialization
using System.Runtime.Serialization.Formatters.Binary;

// memory streams
using System.IO;

namespace Alarm_Front_End
{
    partial class model
    {

        private int localPort = 2568;
        private String localIP = "127.0.0.1";

        private UdpClient dataSocket;

        private Thread reciveWorkerThread;

        private bool threadsActive;

        private bool initalizeNetwork()
        {

            try
            {
                dataSocket = new UdpClient(localPort);
            }
            catch (Exception ex)
            {
                return(false);
            }

            threadsActive = true;

            ThreadStart threadFunction = new ThreadStart(reciveThread);
            reciveWorkerThread = new Thread(threadFunction);
            reciveWorkerThread.Start();

            return (true);
        }

        private void reciveThread()
        {

            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(localIP), localPort);

            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream tempStream = new MemoryStream();
            
            while (threadsActive)
            {
                try
                {
                    Byte[] receiveData = dataSocket.Receive(ref endPoint);

                    tempStream = new System.IO.MemoryStream(receiveData);
                    TimeDataDLL.TimeData.StructTimeData temp = (TimeDataDLL.TimeData.StructTimeData)formatter.Deserialize(tempStream);

                    if(!temp.isAlarmTime)
                    {
                        setTime(temp.hour, temp.minute, temp.second);
                    }
                    else
                    {
                        setAlarm(temp.second, temp.minute, temp.hour);
                    }

                    //recivedString += (DateTime.Now + ": " + System.Text.Encoding.Default.GetString(receiveData) + "\n");
                }
                catch (SocketException ex)
                {
                    return;
                }
            }
        }

        private void networkStop()
        {
            threadsActive = false;
            reciveWorkerThread.Abort();
            reciveWorkerThread.Join();
        }

    }
}

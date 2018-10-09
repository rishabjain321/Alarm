using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Sockets
using System.Net.Sockets;
using System.Net;

// Message Box
// don't forget to add Reference System.Windows.Forms
//using System.Windows.Forms;
using System.Windows;
using TimeDataDLL;


// byte data serialization
using System.Runtime.Serialization.Formatters.Binary;

// memory streams
using System.IO;

namespace FinalProject
{
    public partial class Model 
    {

      
        // some data that keeps track of ports and addresses
        private UInt32 _remotePort = 2568;
        private String _remoteIPAddress = "127.0.0.1";

       
        // this is the UDP socket that will be used to communicate
        // over the network
        private UdpClient _dataSocket;

        public Model()
        {
            try
            {
                // set up generic UDP socket and bind to local port
                //
                //_receiveSocket = new UdpClient(_localPort);
                _dataSocket = new UdpClient();
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
     

                return;
            }



        }
        

        public void SendMessage(TimeDataDLL.TimeData.StructTimeData data)
        {
            IPEndPoint remoteHost = new IPEndPoint(IPAddress.Parse(_remoteIPAddress), (int)_remotePort);

            // formatter used for serialization of data
            BinaryFormatter formatter = new BinaryFormatter();

            MemoryStream stream = new MemoryStream();

            formatter.Serialize(stream, data);

            Byte[] sendData = stream.ToArray();
            

            try
            {
                _dataSocket.Send(sendData, sendData.Length, remoteHost);
            }
            catch (SocketException ex)
            {
                MessageBox.Show(ex.ToString());
               
                return;
            }
        }

       
    }
}


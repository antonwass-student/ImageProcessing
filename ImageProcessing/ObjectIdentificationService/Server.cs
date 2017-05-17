using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using CommunicationProtocol;

namespace ObjectIdentificationService
{
    public class Server
    {
        /*
        private bool IsRunning = false;
        public void Start(ModelLibrary library)
        {
            IPAddress address = IPAddress.Parse("127.0.0.1");
            TcpListener listener = new TcpListener(address, 12345);

            IsRunning = true;

            listener.Start();

            while (IsRunning)
            {
                TcpClient client = listener.AcceptTcpClient();
                Console.WriteLine("Connection accepted from " + client.Client.RemoteEndPoint);

                NetworkStream stream = client.GetStream();

                IFormatter formatter = new BinaryFormatter();

                while (IsRunning)
                {
                    Console.WriteLine("Waiting for messages...");
                    IdentificationRequest request = (IdentificationRequest)formatter.Deserialize(stream);

                    //TODO: list of bytes (?????) into an image

                    
                    
                    string identifiedObject = library.IdentifyObject(request.Image);

                    IdentificationResponse resp = new IdentificationResponse();
                    resp.Timestamp = DateTime.Now;
                    resp.Object = identifiedObject;

                    formatter.Serialize(stream, resp);
                    stream.Flush();
                }
            }
        }

        public void Stop()
        {
            IsRunning = false;
        }

    */
    }

}

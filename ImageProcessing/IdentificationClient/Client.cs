using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using CommunicationProtocol;

namespace IdentificationClient
{
    public class Client
    {
        private TcpClient client;
        private NetworkStream stream;

        public Client()
        {
            client = new TcpClient("127.0.0.1", 12345);
            stream = client.GetStream();
        }

        public void SendRequest(Bitmap image)
        {
            IFormatter formatter = new BinaryFormatter();

            IdentificationRequest request = new IdentificationRequest()
            {
                Image = image,
                Timestamp = DateTime.Now
            };

            formatter.Serialize(stream, request);

            stream.Flush();

            IdentificationResponse response = (IdentificationResponse)formatter.Deserialize(stream);

            Console.WriteLine(response.Object);
        }
    }
}

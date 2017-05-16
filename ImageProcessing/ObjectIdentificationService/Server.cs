using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using CommunicationProtocol;
using Newtonsoft.Json;

namespace ObjectIdentificationService
{
    public class Server
    {
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
                using (StreamReader reader = new StreamReader(client.GetStream()))
                using (StreamWriter writer = new StreamWriter(client.GetStream()))
                {
                    // format to json
                    IFormatter formatter = new BinaryFormatter();
                    while (IsRunning)
                    {
                        Console.WriteLine("Waiting for messages...");
                        //IdentificationRequest request = (IdentificationRequest)formatter.Deserialize(stream);
                        var request = JsonConvert.DeserializeObject<RequestMsg>(reader.ReadLine());

                        Bitmap bmp = new Bitmap(request.Width, request.Height, PixelFormat.Format8bppIndexed);

                        var rect = new Rectangle(0, 0, request.Width, request.Height);
                        var bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed);

                        var ptr = bmpData.Scan0;

                        for (var i = 0; i < request.Height; i++)
                        {
                            Marshal.Copy(request.Pixels, i * request.Width, ptr, request.Width);
                            ptr += bmpData.Stride;
                        }

                        bmp.UnlockBits(bmpData);

                        string identifiedObject = library.IdentifyObject(bmp);

                        var response = new ResponseMsg() {ObjectName = identifiedObject, Success = true};

                        var responseJson = JsonConvert.SerializeObject(response);
                        writer.WriteLine(responseJson);
                        writer.Flush();

                    }
                }
            }
        }

        public void Stop()
        {
            IsRunning = false;
        }

        [Serializable]
        private class RequestMsg
        {
            public Byte[] Pixels { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
            
        }

        [Serializable]
        private class ResponseMsg
        {
            public string ObjectName { get; set; }
            public bool Success { get; set; }
        }
    }
}

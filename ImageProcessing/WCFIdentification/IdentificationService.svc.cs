using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Newtonsoft.Json;
using ObjectIdentificationService;
using WCFIdentification;

namespace WCFIdentification
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "IdentificationService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select IdentificationService.svc or IdentificationService.svc.cs at the Solution Explorer and start debugging.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class IdentificationService : IIdentificationService
    {
        private ModelLibrary ml;

        public IdentificationService()
        {
            ml = new ModelLibrary();
            ml.LoadModels();
        }

        public IdentificationResponse Identify(IdentificationRequest request)
        {
            Debug.WriteLine("Request!");

            //var request = JsonConvert.DeserializeObject<IdentificationRequest>(requestJson);

            //decode

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
            
            var objName = ml.IdentifyObject(bmp);

            Console.WriteLine("Identified object = '" + objName +"'");

            var response = new IdentificationResponse()
            {
                ObjectName = objName
            };


            return response;
        }

        [DataContract]
        public class IdentificationRequest
        {
            [DataMember]
            public byte[] Pixels { get; set; }
            [DataMember]
            public int Height { get; set; }
            [DataMember]
            public int Width { get; set; }
        }

        [DataContract]
        public class IdentificationResponse
        {
            public string ObjectName { get; set; }
        }


    }
}

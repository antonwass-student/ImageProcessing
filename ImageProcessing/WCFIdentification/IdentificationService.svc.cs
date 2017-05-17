using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Newtonsoft.Json;
using ObjectIdentificationService;

namespace WCFIdentification
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "IdentificationService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select IdentificationService.svc or IdentificationService.svc.cs at the Solution Explorer and start debugging.
    public class IdentificationService : IIdentificationService
    {
        public IdentificationService()
        {
            ModelLibrary ml = new ModelLibrary();
            ml.LoadModels();
        }


        public string Identify(string requestJson)
        {
            Console.WriteLine("Request!");

            var request = JsonConvert.DeserializeObject<IdentificationRequest>(requestJson);



            var response = new IdentificationResponse()
            {
                ObjectName = ""
            };


            return JsonConvert.SerializeObject(response);
        }


        public class IdentificationRequest
        {
            public byte[] Pixels { get; set; }
            public int Height { get; set; }
            public int Width { get; set; }
        }

        public class IdentificationResponse
        {
            public string ObjectName { get; set; }
        }


    }
}

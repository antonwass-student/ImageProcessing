using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunicationProtocol
{
    [Serializable]
    public class IdentificationResponse
    {
        public DateTime Timestamp { get; set; }
        public string Object { get; set; }
    }
}

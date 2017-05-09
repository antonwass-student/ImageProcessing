using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CommunicationProtocol
{
    [Serializable]
    public class IdentificationRequest
    {
        public DateTime Timestamp { get; set; }
        public Bitmap Image { get; set; }
    }
}

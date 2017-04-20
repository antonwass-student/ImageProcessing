using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Features2D;
using Emgu.CV.Util;

namespace ObjectIdentification
{
    /// <summary>
    /// TODO: Refactor class
    /// </summary>
    public class ObjectFeatures
    {
        private VectorOfKeyPoint _keyPoints;
        private UMat _descriptors;

        public ObjectFeatures(VectorOfKeyPoint keyPoints, UMat descriptors)
        {
            this._descriptors = descriptors;
            this._keyPoints = keyPoints;
        }


        public string getDescriptorString()
        {
            string desc = "";
            foreach (byte b in _descriptors.Bytes)
                desc += b.ToString();
            return desc;
        }
    }
}

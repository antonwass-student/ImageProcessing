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
        public VectorOfKeyPoint KeyPoints { get; }
        public UMat Descriptors { get; }

        public ObjectFeatures(VectorOfKeyPoint keyPoints, UMat descriptors)
        {
            this.Descriptors = descriptors;
            this.KeyPoints = keyPoints;
        }


        public string GetDescriptorString()
        {
            return Encoding.UTF8.GetString(Descriptors.Bytes);
        }
    }
}

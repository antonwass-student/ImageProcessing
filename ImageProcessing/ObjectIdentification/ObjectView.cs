using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectIdentification
{
    /// <summary>
    /// A view of a physical object.
    /// 
    /// </summary>
    public class ObjectView
    {
        private Perspective _imagePerspective;
        private string _imagePath;
        public ObjectFeatures Features { get; }

        public ObjectView(Perspective perspective, string imagePath)
        {
            this._imagePerspective = perspective;
            this._imagePath = imagePath;
            this.Features = FeatureDetector.DetectFeatures_Brisk(imagePath);
        }

        public enum Perspective
        {
            Up = 0,
            Down = 1,
            Right = 2,
            Left = 3,
            Front = 4,
            Back = 5
        }
    }
}

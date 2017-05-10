using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Util;

namespace ObjectIdentification
{
    /// <summary>
    /// A collection of the results from an image search
    /// TODO: refactor to ImageMatch?
    /// </summary>
    public class ImageSearchResult
    {
        public ObjectView MatchingView { get;}
        public Mat Homography { get;}
        public VectorOfVectorOfDMatch Matches { get;}
        public ObjectFeatures ObservedFeatures { get; }
        public Mat Mask { get; }

        public ImageSearchResult(ObjectView view, Mat homography, VectorOfVectorOfDMatch matches, ObjectFeatures observedFeatures, Mat mask)
        {
            this.MatchingView = view;
            this.Homography = homography;
            this.Matches = matches;
            this.ObservedFeatures = observedFeatures;
            this.Mask = mask;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;
using Emgu.CV.Util;


namespace ObjectIdentification
{
    public partial class FeatureDetector
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="imagePath"></param>
        public static ObjectFeatures DetectFeatures_Brisk(string imagePath)
        {
            try
            {
                //Load the image.
                Image<Bgr, byte> image = new Image<Bgr, byte>(imagePath);

                Mat modelImage = image.Mat;

                VectorOfKeyPoint modelKeyPoints = new VectorOfKeyPoint();

                using (UMat uModelImage = modelImage.GetUMat(AccessType.Read))
                {
                    Brisk brisk = new Brisk();

                    UMat descriptors = new UMat();

                    Debug.WriteLine("Detecting features and computing descriptors...");

                    brisk.DetectAndCompute(uModelImage, null, modelKeyPoints, descriptors, false);

                    Debug.WriteLine("Computation finished!");

                    return new ObjectFeatures(modelKeyPoints, descriptors);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception when detecting features" + e.Message);
                throw;
            }
        }


        public static List<ImageSearchResult> SearchImageForObjects(WorldObject modelObject, string imageToSearch)
        {
            int k = 2;
            double uniquenessThreshold = 0.8;
            double hessianThresh = 300;

            ObjectFeatures targetImageFeatures = DetectFeatures_Brisk(imageToSearch);
     
            Mat mask;

            List<ImageSearchResult> searchResults = new List<ImageSearchResult>();

            foreach (ObjectView view in modelObject.Views)
            {
                if (view == null)
                    continue;

                VectorOfVectorOfDMatch matches = new VectorOfVectorOfDMatch();

                BFMatcher matcher = new BFMatcher(DistanceType.L2);
                matcher.Add(view.Features.Descriptors);

                matcher.KnnMatch(targetImageFeatures.Descriptors, matches, 2, null);

                mask = new Mat(matches.Size, 1, DepthType.Cv8U, 1);

                mask.SetTo(new MCvScalar(255));

                Features2DToolbox.VoteForUniqueness(matches, uniquenessThreshold, mask);

                int nonZeroCount = CvInvoke.CountNonZero(mask);

                if (nonZeroCount >= 4)
                {
                    nonZeroCount = Features2DToolbox.VoteForSizeAndOrientation(view.Features.KeyPoints,
                        targetImageFeatures.KeyPoints, matches, mask, 1.5, 20);

                    if (nonZeroCount >= 4)
                    {
                        Mat homography = Features2DToolbox.GetHomographyMatrixFromMatchedFeatures(view.Features.KeyPoints,
                            targetImageFeatures.KeyPoints, matches, mask, 2);

                        searchResults.Add(new ImageSearchResult(view, homography, matches));
                    }
                }
            }

            return searchResults;
        }
    }
}

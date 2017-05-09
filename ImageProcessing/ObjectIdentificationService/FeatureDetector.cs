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
using System.Drawing;


namespace ObjectIdentificationService
{
    public partial class FeatureDetector
    {

        public static ObjectFeatures DetectFeatures(string imagePath)
        {
            return DetectFeatures_Brisk(new Image<Bgr, byte>(imagePath));
        }

        public static ObjectFeatures DetectFeatures(Bitmap image)
        {
            return DetectFeatures_Brisk(new Image<Bgr, byte>(image));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="imagePath"></param>
        private static ObjectFeatures DetectFeatures_Brisk(Image<Bgr, byte> image)
        {
            try
            {
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


        /// <summary>
        /// 
        ///
        /// TODO: thresholds must be set
        /// </summary>
        /// <param name="model"></param>
        /// <param name="imageToSearch"></param>
        /// <returns></returns>
        public static bool SearchImageForObjects(List<ObjectFeatures> model, Bitmap image)
        {
            int k = 2;
            double uniquenessThreshold = 0.8;
            double hessianThresh = 300;

            ObjectFeatures targetImageFeatures = DetectFeatures(image);
     
            Mat mask;

            VectorOfVectorOfDMatch matches = new VectorOfVectorOfDMatch();

            BFMatcher matcher = new BFMatcher(DistanceType.L2);

            foreach(ObjectFeatures of in model)
            {
                matcher.Add(of.Descriptors);

                matcher.KnnMatch(targetImageFeatures.Descriptors, matches, 2, null);

                mask = new Mat(matches.Size, 1, DepthType.Cv8U, 1);

                mask.SetTo(new MCvScalar(255));

                Features2DToolbox.VoteForUniqueness(matches, uniquenessThreshold, mask);

                int nonZeroCount = CvInvoke.CountNonZero(mask);

                if (nonZeroCount >= 4)
                {
                    nonZeroCount = Features2DToolbox.VoteForSizeAndOrientation(of.KeyPoints,
                        targetImageFeatures.KeyPoints, matches, mask, 1.5, 20);

                    if (nonZeroCount >= 4)
                    {
                        return true;
                        /*
                        Mat homography = Features2DToolbox.GetHomographyMatrixFromMatchedFeatures(view.Features.KeyPoints,
                            targetImageFeatures.KeyPoints, matches, mask, 2);

                        searchResults.Add(new ImageSearchResult(view, homography, matches));
                        */
                    }
                }
            }

            return false;
        }
    }
}

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
                throw e;
            }
            
        }
    }
}

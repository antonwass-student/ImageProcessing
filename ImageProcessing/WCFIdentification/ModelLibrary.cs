using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace ObjectIdentificationService
{
    public class ModelLibrary
    {
        private Dictionary<string, List<ObjectFeatures>> models = new Dictionary<string, List<ObjectFeatures>>();

        /// <summary>
        /// Loads all models in the models directory
        /// </summary>
        public void LoadModels()
        {
            Console.WriteLine("Loading models...");
            int filesLoaded = 0;
            foreach(string filePath in Directory.GetFiles(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath + "bin/models/"))
            {
                Bitmap img = new Bitmap(Image.FromFile(filePath));

                string fileName = filePath.Split('/').Last().Split('_').First();

                List<ObjectFeatures> listOfFeatures;

                if (models.TryGetValue(fileName, out listOfFeatures))
                {

                }
                else
                {
                    listOfFeatures = new List<ObjectFeatures>();
                    models.Add( fileName.Split('.').First(), listOfFeatures);
                }
                    

                listOfFeatures.Add(FeatureDetector.DetectFeatures(filePath));

                filesLoaded++;
            }

            Console.WriteLine("Finished loading " + filesLoaded + " file(s).");
        }

        /// <summary>
        /// Searches an image for previously learned models.
        /// </summary>
        /// <param name="image"></param>
        public string IdentifyObject(Bitmap image)
        {
            foreach(string key in models.Keys)
            {
                if(FeatureDetector.SearchImageForObjects(models[key], image))
                {
                    return key;
                }
            }

            return null;
        }
    }
}

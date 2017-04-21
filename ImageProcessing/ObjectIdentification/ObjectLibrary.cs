using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ObjectIdentification
{
    /// <summary>
    /// Stores and manage a library of real world objects.
    /// </summary>
    public class ObjectLibrary
    {
        private Dictionary<int, WorldObject> _worldObjects;

        public ObjectLibrary()
        {
            _worldObjects = new Dictionary<int, WorldObject>();
        }


        /// <summary>
        /// Train the object identifier with images mapped to an ID and perspective.
        /// </summary>
        /// <param name="objectId">An identifier for the object on the image</param>
        /// <param name="imagePath">Path to an image of the object to learn</param>
        /// <param name="perspective">The perspective used when taking the picture</param>
        public void Train(int objectId, string imagePath, ObjectView.Perspective perspective)
        {
            WorldObject wo;

            if (_worldObjects.TryGetValue(objectId, out wo)) { }
            else
            {
                wo = new WorldObject(objectId);
                _worldObjects.Add(objectId, wo);
            }

            wo.AddView(imagePath, perspective);
        }

        /// <summary>
        /// Searches an image for objects previously learned.
        /// </summary>
        /// <param name="imagePath">The image to search</param>
        /// <param name="objectId"></param>
        public List<ImageSearchResult> Search(string imagePath, int objectId)
        {
            WorldObject wo;
            if (_worldObjects.TryGetValue(objectId, out wo))
            {
                return FeatureDetector.SearchImageForObjects(wo, imagePath);
            }

            throw new NoSuchObjectException("Object with ID " + objectId + " does not exist in the library.");
            
        }


        public ObjectFeatures GetFeatures(int objectId, ObjectView.Perspective perspective)
        {
            WorldObject wo = null;
            if (_worldObjects.TryGetValue(objectId, out wo))
            {
                return wo.GetFeatures(perspective);
            }

            return null;
        }

        /// <summary>
        /// No object with that ID was found in the library.
        /// </summary>
        public class NoSuchObjectException : Exception
        {
            public NoSuchObjectException(string message) : base(message)
            {
            }
        }
    }
}

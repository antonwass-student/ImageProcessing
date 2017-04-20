using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectIdentification
{
    public class WorldObject
    {
        private int _id;
        private readonly ObjectView[] _views = new ObjectView[6];

        public WorldObject(int objectId)
        {
            _id = objectId;
        }

        /// <summary>
        /// </summary>
        /// <param name="imagePath"></param>
        /// <param name="perspective"></param>
        public void AddView(string imagePath, ObjectView.Perspective perspective)
        {
            _views[(int)perspective] = new ObjectView(perspective, imagePath);
        }

        /// <summary>
        /// TEMP
        /// </summary>
        /// <returns></returns>
        public ObjectFeatures getFeatures(ObjectView.Perspective perspective)
        {
            return _views[(int)perspective].GetFeatures();
        }

    }
}

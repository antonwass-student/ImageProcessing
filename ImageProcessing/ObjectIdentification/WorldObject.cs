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
        public ObjectView[] Views { get; }

        public WorldObject(int objectId)
        {
            _id = objectId;

            Views = new ObjectView[6];
        }

        /// <summary>
        /// </summary>
        /// <param name="imagePath"></param>
        /// <param name="perspective"></param>
        public void AddView(string imagePath, ObjectView.Perspective perspective)
        {
            Views[(int)perspective] = new ObjectView(perspective, imagePath);
        }

        /// <summary>
        /// TEMP
        /// </summary>
        /// <returns></returns>
        public ObjectFeatures GetFeatures(ObjectView.Perspective perspective)
        {
            return Views[(int) perspective].Features;
        }



    }
}

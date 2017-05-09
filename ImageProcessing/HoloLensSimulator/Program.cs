using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentificationClient;
using System.Drawing;

namespace HoloLensSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            string file = Console.ReadLine();

            Client c = new Client();

            c.SendRequest(new Bitmap(Image.FromFile(file)));

            while (true)
            {
                file = Console.ReadLine();
                c.SendRequest(new Bitmap(Image.FromFile(file)));
            }

            Console.ReadKey();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectIdentificationService
{
    class Program
    {
        static void Main(string[] args)
        {
            ModelLibrary ml = new ModelLibrary();
            ml.LoadModels();

            Server server = new Server();

            server.Start(ml);

            Console.ReadKey();

            server.Stop();
        }
    }
}

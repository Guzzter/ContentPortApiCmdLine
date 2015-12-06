using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tridion.ContentManager.ImportExport;
using Tridion.ContentManager.ImportExport.Client;

namespace ContentPortApi
{
    class Program
    {
        // http://tridioninternals.blogspot.nl/
        static void Main(string[] args)
        {
            BatchPorter port = new BatchPorter("export.xml");
            port.Go();

        }

        
    }
}

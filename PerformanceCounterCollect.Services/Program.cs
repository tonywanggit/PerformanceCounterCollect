using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace PerformanceCounterCollect.Services
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(hc =>
            {
                hc.UseNLog();
                // service is constructed using its default constructor
                hc.Service<PerfmonWorker>();
                // sets service properties
                hc.SetServiceName(typeof(PerfmonWorker).Namespace);
                hc.SetDisplayName(typeof(PerfmonWorker).Namespace);
                hc.SetDescription("PerfmonWorker - one to monitor them all.");
            });
        }
       
    }
}

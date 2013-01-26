using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Topshelf;
using Topshelf.Logging;

namespace PerformanceCounterCollect.Services
{
    class PerfmonWorker: ServiceControl
    {
        private readonly LogWriter logger = HostLogger.Get<PerfmonWorker>();
        public static bool ShouldStop { get; private set; }
        private ManualResetEvent stopHandle;

        public bool Start(HostControl hostControl)
        {
            logger.Info("Starting PerfmonWorker...");

            stopHandle = new ManualResetEvent(false);

            ThreadPool.QueueUserWorkItem(new ServiceMonitor().Monitor, stopHandle);

            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            ShouldStop = true;
            logger.Info("Stopping PerfmonWorker...");
            // wait for all threads to finish
            stopHandle.WaitOne(ServiceMonitor.SleepIntervalInMilliSecs + 10);

            return true;
        }
    }
  
}

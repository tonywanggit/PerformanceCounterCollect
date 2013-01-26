using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PerformanceCounterCollect.Models;
using Topshelf.Logging;

namespace PerformanceCounterCollect.Services
{
    sealed class ServiceMonitor
    {
        public const int SleepIntervalInMilliSecs = 50000;

        private readonly LogWriter logger = HostLogger.Get<ServiceMonitor>();
        private IList<Tuple<int, PerformanceCounter>> serviceCounters;

        public void Monitor(object state)
        {
            ManualResetEvent stopHandle = (ManualResetEvent)state;
            String machineName = Environment.MachineName;
            try
            {
                Initialize(machineName);
                var snapshots = new ServiceCounterSnapshot[serviceCounters.Count];

                while (!PerfmonWorker.ShouldStop)
                {
                    Thread.Sleep(SleepIntervalInMilliSecs);

                    // this would be our timestamp value by which we will group the snapshots
                    DateTime timeStamp = DateTime.UtcNow;
                    // collect snapshots
                    for (int i = 0; i < serviceCounters.Count; i++)
                    {
                        var snapshot = new ServiceCounterSnapshot();
                        snapshot.CreationTimeUtc = timeStamp;
                        snapshot.SnapshotMachineName = machineName;
                        snapshot.ServiceCounterId = serviceCounters[i].Item1;
                        try
                        {
                            snapshot.ServiceCounterValue = serviceCounters[i].Item2.NextValue();
                            logger.DebugFormat("Performance counter {0} read value: {1}", GetPerfCounterPath(serviceCounters[i].Item2),
                                                snapshot.ServiceCounterValue);
                        }
                        catch (InvalidOperationException)
                        {
                            snapshot.ServiceCounterValue = null;
                            logger.DebugFormat("Performance counter {0} didn't send any value.", GetPerfCounterPath(serviceCounters[i].Item2));
                        }
                        snapshots[i] = snapshot;
                    }
                    SaveServiceSnapshots(snapshots);
                }
            }
            finally
            {
                stopHandle.Set();
            }
        }

        private void Initialize(String machineName)
        {
            try
            {
                var counters = new List<Tuple<int, PerformanceCounter>>();

                foreach (var counter in PerfmonClient.SelectServiceCounter(machineName))
                {
                    logger.InfoFormat(@"Creating performance counter: {0}\{1}\{2}\{3}", counter.MachineName ?? ".", counter.CategoryName,
                                        counter.CounterName, counter.InstanceName);
                    var perfCounter = new PerformanceCounter(counter.CategoryName, counter.CounterName, counter.InstanceName, counter.MachineName ?? ".");
                    counters.Add(new Tuple<int, PerformanceCounter>(counter.Id, perfCounter));
                    // first value doesn't matter so we should call the counter at least once
                    try { perfCounter.NextValue(); }
                    catch { }
                }
               

                serviceCounters = counters;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }

        private void SaveServiceSnapshots(IEnumerable<ServiceCounterSnapshot> snapshots)
        {
            PerfmonClient.SaveServiceSnapshots(snapshots);
        }

        private String GetPerfCounterPath(PerformanceCounter cnt)
        {
            return String.Format(@"{0}\{1}\{2}\{3}", cnt.MachineName, cnt.CategoryName, cnt.CounterName, cnt.InstanceName);
        }
    }
}

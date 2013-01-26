using System;

namespace PerformanceCounterCollect.Models
{
    public class ServiceCounterSnapshot
    {
        public int Id { get; set; }

        public int ServiceCounterId { get; set; }

        /// <summary>
        /// Machine on which the snapshot was taken.
        /// </summary>
        public String SnapshotMachineName { get; set; }

        public DateTime CreationTimeUtc { get; set; }

        public float? ServiceCounterValue { get; set; }
    }
}

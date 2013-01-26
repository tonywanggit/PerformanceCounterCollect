using System;

namespace PerformanceCounterCollect.Models
{
    public class ServiceCounter
    {
        public int Id { get; set; }

        public String ServiceName { get; set; }

        public String MachineName { get; set; }
        public String CategoryName { get; set; }
        public String CounterName { get; set; }
        public String InstanceName { get; set; }

        public String DisplayName { get; set; }
        
        public String DisplayType { get; set; }

        public override String ToString()
        {
            return String.Format(@"{0}\{1}\{2}\{3}", MachineName ?? ".", CategoryName, CounterName, InstanceName);
        }
    }
}

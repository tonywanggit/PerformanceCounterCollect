using MongoDB.Bson;
using System;

namespace PerformanceCounterCollect.Models
{
    public class ServiceCounterSnapshot : IConvertibleToBsonDocument
    {
        public int Id { get; set; }

        public ServiceCounter ServiceCounter { get; set; }
        public String MachineIP { get; set; }
        public String MachineName { get; set; }
        /// <summary>
        /// Machine on which the snapshot was taken.
        /// </summary>
        public String SnapshotMachineName { get; set; }

        public DateTime CreationTimeUtc { get; set; }

        public float? ServiceCounterValue { get; set; }

        public BsonDocument ToBsonDocument()
        {
            var doc = new BsonDocument { 
				{ "ID", this.Id},
				{ "SnapshotMachineName", this.SnapshotMachineName},
				{ "ServiceCounterValue", this.ServiceCounterValue},
				{ "CreationTimeUtc", this.CreationTimeUtc},
                { "SnapshotMachineName",this.SnapshotMachineName },
                { "MachineIP", this.MachineIP},
                { "MachineName",this.MachineName }
			};

            doc["ServiceCounter"] = BuildPropertiesBsonDocument(this.ServiceCounter);
            return doc;
        }

        protected static BsonDocument BuildPropertiesBsonDocument(ServiceCounter properties)
        {
            var doc = new BsonDocument { 
                {"ID", properties.Id},
				{ "InstanceName", properties.InstanceName},
				{ "MachineIP", properties.MachineIP},
				{ "MachineName", properties.MachineName},
                { "ServiceName",properties.ServiceName },
                { "DisplayType", properties.DisplayType},
                { "DisplayName",properties.DisplayName },
                {"CounterName",properties.CounterName},
                {"CategoryName",properties.CategoryName}
            };
            
            return doc;
        }
    }
}

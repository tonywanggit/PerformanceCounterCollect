//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Web;
//using Dapper;
//using PerformanceCounterCollect.Models;

//namespace PerformanceCounterCollect.Web.Models
//{
//    public class ServiceCounterSnapshotRepository: BaseRepository
//    {
//        public IEnumerable<ServiceCounterSnapshot> SaveServiceSnapshots(IEnumerable<ServiceCounterSnapshot> snapshots)
//        {
//            using (IDbConnection connection = OpenConnection())
//            {
//                foreach (var snapshot in snapshots)
//                {
//                    // insert new snapshot to the database
//                    int retVal = connection.Execute(
//    @"insert into service_counter_snapshots(ServiceCounterId,SnapshotMachineName,CreationTimeUtc,ServiceCounterValue) values (
//        @ServiceCounterId,@SnapshotMachineName,@CreationTimeUtc,@ServiceCounterValue)", snapshot);
//                    SetIdentity<int>(connection, id => snapshot.Id = id);
//                }
//            }
//            return snapshots;
//        }
//    }
//}
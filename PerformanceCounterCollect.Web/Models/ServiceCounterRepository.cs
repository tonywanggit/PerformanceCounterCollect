using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Dapper;
using PerformanceCounterCollect.Models;


namespace PerformanceCounterCollect.Web.Models
{
    public class ServiceCounterRepository : BaseRepository
    {
        public IEnumerable<ServiceCounter> SelectServiceCounter(string machineName)
        {
            using (IDbConnection connection = OpenConnection())
            {
                string query = "select Id,ServiceName,CategoryName,CounterName,InstanceName from service_counters where MachineName=@MachineName";
                return connection.Query<ServiceCounter>(query, new { MachineName = machineName });
            }
        }


    }
}
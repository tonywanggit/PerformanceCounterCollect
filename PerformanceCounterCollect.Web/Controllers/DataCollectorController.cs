using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PerformanceCounterCollect.Models;
using PerformanceCounterCollect.Web.Models;

namespace PerformanceCounterCollect.Web.Controllers
{
    public class DataCollectorController : ApiController
    {
        private ServiceCounterRepository scRepository;
        private ServiceCounterSnapshotRepository scSnapshotReposity;

        public DataCollectorController()
        {
            scRepository = new ServiceCounterRepository();
            scSnapshotReposity = new ServiceCounterSnapshotRepository();
        }

        // GET api/values/5
        [HttpGet]
        public IEnumerable<ServiceCounter> SelectServiceCounter(string machineName)
        {
            return scRepository.SelectServiceCounter(machineName);
        }

        // POST api/values
        [HttpPost]
        public IEnumerable<ServiceCounterSnapshot> SaveServiceSnapshots([FromBody]IEnumerable<ServiceCounterSnapshot> value)
        {
            return scSnapshotReposity.SaveServiceSnapshots(value);
        }
    }
      
}
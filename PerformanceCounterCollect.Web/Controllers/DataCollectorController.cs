using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PerformanceCounterCollect.Models;
using PerformanceCounterCollect.Web.Models;
using System.Configuration;
using MongoDB.Driver;

namespace PerformanceCounterCollect.Web.Controllers
{
    public class DataCollectorController : ApiController
    {
         private string _collectionName;
        private string _connectionName;
        private string _connectionString;
        private string _Database;

    

        public string Database
        {
            get { return _Database ?? "DataCollectorSnapshot"; }
            set { _Database = value; }
        }

        private ServiceCounterRepository scRepository;
        private ServiceCounterSnapshotRepository scSnapshotReposity;

        public DataCollectorController()
        {
            scRepository = new ServiceCounterRepository();
            scSnapshotReposity = new ServiceCounterSnapshotRepository();
            this._collectionName = "";
            this._connectionName = "MongoDB";
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
            using (var repository = GetRepository())
            {
                foreach (var item in value)
                {

                    var collectionName = !string.IsNullOrWhiteSpace(_collectionName)
                        ? _collectionName : "ServiceCounterSnapshot";
                    repository.Insert(collectionName, item.ToBsonDocument());
                }
            }     
            return value;

        }

        private IRepository GetRepository()
        {
            // We have a connection string name, grab this from the config.
            if (!string.IsNullOrWhiteSpace(this._connectionName))
            {
                if (ConfigurationManager.ConnectionStrings[this._connectionName] == null ||
                    string.IsNullOrWhiteSpace(ConfigurationManager.ConnectionStrings[this._connectionName].ConnectionString))
                    throw new MongoConnectionException("The connection string name specified was not found.");

                this._connectionString = ConfigurationManager.ConnectionStrings[this._connectionName].ConnectionString;
            }

            MongoUrlBuilder mongoUrlBuilder = null;
            // We have a connection string
            if (!string.IsNullOrWhiteSpace(this._connectionString))
            {
                mongoUrlBuilder = new MongoUrlBuilder(this._connectionString);

                if (string.IsNullOrEmpty(mongoUrlBuilder.DatabaseName))
                {
                    mongoUrlBuilder.DatabaseName = Database;
                }
            }

            return new MongoRepository(mongoUrlBuilder.ToMongoUrl(), mongoUrlBuilder.DatabaseName);
        }
    }
      
}
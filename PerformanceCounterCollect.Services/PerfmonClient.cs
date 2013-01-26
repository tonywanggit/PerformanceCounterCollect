using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PerformanceCounterCollect.Models;

namespace PerformanceCounterCollect.Services
{
    sealed class PerfmonClient
    {
        public static IEnumerable<ServiceCounter> SelectServiceCounter(string machineName)
        {
            string uri = string.Format("http://localhost:7542/api/DataCollector/SelectServiceCounter?machinename={0}",machineName);
            HttpClient client = new HttpClient();
            string body = client.GetAsync(uri).Result.Content.ReadAsStringAsync().Result;
            var serviceCounters = JsonConvert.DeserializeObject<IList<ServiceCounter>>(body);
            return serviceCounters;
        }

        public static string SaveServiceSnapshots(IEnumerable<ServiceCounterSnapshot> snapshots)
        {
            var requestJson = JsonConvert.SerializeObject(snapshots);

            HttpContent httpContent = new StringContent(requestJson);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var httpClient = new HttpClient();

            var responseJson = httpClient.PostAsync("http://localhost:7542/api/DataCollector/SaveServiceSnapshots", httpContent)
                .Result.Content.ReadAsStringAsync().Result;

            return responseJson;
        }
    }
}

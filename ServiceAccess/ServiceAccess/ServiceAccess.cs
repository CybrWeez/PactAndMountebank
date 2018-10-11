using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ServiceAccess
{
    public class ServiceAccess
    {
        private readonly HttpClient ServiceHttpClient;

        public ServiceAccess(HttpClient httpClient)
        {
            ServiceHttpClient = httpClient;
        }

        public async Task<string> GetData(string route)
        {
            var result = ServiceHttpClient.GetAsync(route).Result;
            if (result.StatusCode != HttpStatusCode.OK)
                throw new Exception("bad request");

            return result.Content.ReadAsStringAsync().Result;
        }
    }
}

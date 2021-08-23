using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DiscoverParkTest.Services
{
    public class ApiConsume : IApiConsume
    {
        public async Task<HttpResponseMessage> Get(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                return await client.GetAsync(url);
            }
        }


        public async Task<HttpResponseMessage> Post(string url, StringContent stringContent)
        {
            using (HttpClient client = new HttpClient())
            {
                return await client.PostAsync(url, stringContent);
            }
        }
    }
}

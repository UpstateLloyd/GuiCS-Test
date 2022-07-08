using GuiCs.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuiCs.Services
{
    public class IPService
    {
        public async Task<IPAddressModel> GetIPAsync()
        {
            var options = new RestClientOptions("https://jsonip.com") {
                MaxTimeout = 10000
            };
            var client = new RestClient(options);
            var request = new RestRequest("",Method.Get);
            RestResponse response = await client.ExecuteAsync(request);

            return JsonConvert.DeserializeObject<IPAddressModel>(response.Content);
        }
    }
}

using jzo.Models.InfoBipModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace jzo.Services
{
    public class InfoBipService
    {
        private static string _authorization;
        private static string _base_url;

        public InfoBipService()
        {
            _base_url = "https://api.infobip.com";
            _authorization = Convert.ToBase64String(Encoding.UTF8.GetBytes("digiForte1:Test123!"));
        }
        public static async  Task<bool> sendMessage(string number, string message)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_base_url + "/sms/1/text/single");

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

                //add authorization header for paystack
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", _authorization);

                //send a POST
                var content = new FormUrlEncodedContent(new[]
                {
                     new KeyValuePair<string, string>("from", "JZO"),
                     new KeyValuePair<string, string>("to", number),
                     new KeyValuePair<string, string>("text", message)
                });

                //do a POST
                var response = await client.PostAsync(_base_url + "/sms/1/text/single", content);

                var jsonResponse = await response.Content.ReadAsStringAsync();
                if(jsonResponse != null)
                {
                    var infoBipResponse = JsonConvert.DeserializeObject<SendResponse>(jsonResponse);
                    if(infoBipResponse.messages != null)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
    }
}

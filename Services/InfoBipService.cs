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
                client.BaseAddress = new Uri("https://api.infobip.com/sms/1/text/single");

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                //add authorization header for paystack
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "ZGlnaUZvcnRlMTpUZXN0MTIzIQ==");

                //send a POST
                var payload = new InfoBipPayload
                {
                    from = "JZO",
                    to = number,
                    text = message
                };

                //serialize payload to json
                var stringPayload = await Task.Run(()=> JsonConvert.SerializeObject(payload));

                // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
                var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                //do a POST
                var response = await client.PostAsync("https://api.infobip.com/sms/1/text/single", content);

                var jsonResponse = await response.Content.ReadAsStringAsync();
                if(jsonResponse != null)
                {
                    try
                    {
                        var infoBipResponse = JsonConvert.DeserializeObject<SendResponse>(jsonResponse);
                        if (infoBipResponse.messages != null)
                        {
                            return true;
                        }
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.StackTrace);
                        return false;
                    }
                }
                return false;
            }
        }
    }
}

using jzo.Models.PaystackModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace jzo.Services
{
    public class PaystackService
    {
        private static string paystack_key;
        private static string paystack_uri_header;

        public PaystackService()
        {
             paystack_uri_header = "https://api.paystack.co/transaction/";
             paystack_key = "sk_live_9718c5f6ab2f0186e14358c38f723c8547852c5a";
        }
        public static async Task<bool> IsPaymentExist(string payment_reference)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri($"https://api.paystack.co/transaction/verify/{payment_reference}");

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

                //add authorization header for paystack
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "sk_live_9718c5f6ab2f0186e14358c38f723c8547852c5a");

                //do a GET
                var response = await client.GetAsync($"https://api.paystack.co/transaction/verify/{payment_reference}");
                var jsonResponse = await response.Content.ReadAsStringAsync();

               
                var verification = JsonConvert.DeserializeObject<TransactionVerify>(jsonResponse);
                //return verification.status;
                return verification.status;
              
            }
        }
    }
}

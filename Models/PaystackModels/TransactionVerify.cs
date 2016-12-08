using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jzo.Models.PaystackModels
{

    public class TransactionVerify
    {
        public bool status { get; set; }
        public string message { get; set; }
        public Data data { get; set; }
    }

    public class TransactionVerifyData
    {
        public int amount { get; set; }
        public string currency { get; set; }
        public DateTime transaction_date { get; set; }
        public string status { get; set; }
        public string reference { get; set; }
        public string domain { get; set; }
        public int metadata { get; set; }
        public string gateway_response { get; set; }
        public object message { get; set; }
        public string channel { get; set; }
        public string ip_address { get; set; }
        public TransactionVerifyLog log { get; set; }
        public object fees { get; set; }
        public TransactionVerifyAuthorization authorization { get; set; }
        public Customer customer { get; set; }
        public string plan { get; set; }
    }

    public class TransactionVerifyLog
    {
        public int time_spent { get; set; }
        public int attempts { get; set; }
        public object authentication { get; set; }
        public int errors { get; set; }
        public bool success { get; set; }
        public bool mobile { get; set; }
        public object[] input { get; set; }
        public object channel { get; set; }
        public TransactionVerifyHistory[] history { get; set; }
    }

    public class TransactionVerifyHistory
    {
        public string type { get; set; }
        public string message { get; set; }
        public int time { get; set; }
    }

    public class TransactionVerifyAuthorization
    {
        public string authorization_code { get; set; }
        public string card_type { get; set; }
        public string last4 { get; set; }
        public string exp_month { get; set; }
        public string exp_year { get; set; }
        public string bin { get; set; }
        public string bank { get; set; }
        public string channel { get; set; }
        public bool reusable { get; set; }
        public string country_code { get; set; }
    }

    public class Customer
    {
        public int id { get; set; }
        public string customer_code { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
    }

}

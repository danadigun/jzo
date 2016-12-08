using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jzo.Models.InfoBipModels
{

    public class SendResponse
    {
        public SendResponseMessage[] messages { get; set; }
    }

    public class SendResponseMessage
    {
        public string to { get; set; }
        public SendResponseStatus status { get; set; }
        public int smsCount { get; set; }
        public string messageId { get; set; }
    }

    public class SendResponseStatus
    {
        public int id { get; set; }
        public int groupId { get; set; }
        public string groupName { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }

}

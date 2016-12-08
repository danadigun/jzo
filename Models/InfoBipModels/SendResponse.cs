using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jzo.Models.InfoBipModels
{

    public class InfoBipPayload
    {
        public string from { get; set; }
        public string to { get; set; }
        public string text { get; set; }
    }


    public class SendResponse
    {
        public SendResponseMessage[] messages { get; set; }
    }

    public class SendResponseMessage
    {
        public string to { get; set; }
        public SendResponseStatus status { get; set; }
    }

    public class SendResponseStatus
    {
        public int groupId { get; set; }
        public string groupName { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string action { get; set; }
    }

}

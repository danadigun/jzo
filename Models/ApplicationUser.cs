using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace jzo.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser /*: IdentityUser*/
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string mailingAddress { get; set; }
        public string phone { get; set; }
        //public string email { get; set; } 
    }
}

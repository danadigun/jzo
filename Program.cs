using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace jzo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //add create roles

            //host
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseEnvironment("Production")
                //.UseEnvironment("Development")
                .Build();

            host.Run();
        }
    }
}

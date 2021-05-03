using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YoumaconSecurityOps.Api.Models
{
    public class AppSettings
    {
        public string YoumaDbConnectionString { get; set; }

        public string EventStoreConnectionString { get; set;}
    }
}

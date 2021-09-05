using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YoumaconSecurityOps.Web.Client.Models
{
    /// <summary>
    /// Container for Settings stored in appsettings.json to be deserialized into
    /// </summary>
    public class AppSettings
    {
        /// <value>
        /// The Connection string for the security operations database
        /// </value>
        public string YoumaDbConnectionString { get; set; }

        /// <value>
        /// The connection string for the Event Store/Auditing Database
        /// </value>
        public string EventStoreConnectionString { get; set;}
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoumaconSecurityOps.Core.Shared.Models;

namespace YoumaconSecurityOps.Core.Shared.Parameters
{
    public abstract record QueryStringParameters(bool IsHistoricalQuery = false) : IEntity
    {
        public Guid Id => Guid.NewGuid();
    }
}

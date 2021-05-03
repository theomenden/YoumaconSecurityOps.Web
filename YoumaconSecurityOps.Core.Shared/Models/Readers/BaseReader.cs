using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoumaconSecurityOps.Core.Shared.Models.Readers
{
    public abstract class BaseReader: IEntity
    {
        public Guid Id { get; set; }
    }
}

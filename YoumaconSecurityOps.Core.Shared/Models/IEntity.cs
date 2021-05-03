using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoumaconSecurityOps.Core.Shared.Models
{
    public interface IEntity
    {
        Guid Id { get; }
    }
}

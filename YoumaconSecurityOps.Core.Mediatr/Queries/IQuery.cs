using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace YoumaconSecurityOps.Core.Mediatr.Queries
{
    public interface IQuery<T>: IRequest<T>
    {
        Guid Id { get; }
    }
}

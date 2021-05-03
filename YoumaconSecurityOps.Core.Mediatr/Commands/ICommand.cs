using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace YoumaconSecurityOps.Core.Mediatr.Commands
{
    public interface ICommand: IRequest
    {
        public Guid Id { get; }
    }
}

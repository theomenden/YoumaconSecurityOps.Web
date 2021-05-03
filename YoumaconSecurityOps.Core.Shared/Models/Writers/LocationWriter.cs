using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoumaconSecurityOps.Core.Shared.Models.Writers
{
    public record LocationWriter(string Name, bool IsHotel) : BaseWriter;
}

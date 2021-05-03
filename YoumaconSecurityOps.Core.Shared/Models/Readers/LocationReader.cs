using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoumaconSecurityOps.Core.Shared.Models.Readers
{
    public sealed class LocationReader: BaseReader
    {
        public string Name { get; set; }

        public bool IsHotel { get; set; }
    }
}

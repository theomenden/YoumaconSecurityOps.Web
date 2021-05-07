﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoumaconSecurityOps.Core.Shared.Models.Readers;

namespace YoumaconSecurityOps.Core.Mediatr.Queries
{
    public class GetLocationsQuery : IQuery<IAsyncEnumerable<LocationReader>>
    {
        public GetLocationsQuery()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; }
    }
}

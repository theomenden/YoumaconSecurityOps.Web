using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using YoumaconSecurityOps.Core.Shared.Models.Readers;
using YoumaconSecurityOps.Core.Shared.Models.Writers;

namespace YoumaconSecurityOps.Core.AutoMapper.Profiles
{
    public sealed class YoumaAutoMappingProfile: Profile
    {
        public YoumaAutoMappingProfile()
        {
            CreateMap<LocationWriter, LocationReader>();

            CreateMap<ContactWriter, ContactReader>();

            CreateMap<StaffWriter, StaffReader>();
        }
    }
}

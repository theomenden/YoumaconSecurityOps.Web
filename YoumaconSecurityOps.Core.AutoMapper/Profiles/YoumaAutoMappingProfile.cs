using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using YoumaconSecurityOps.Core.AutoMapper.Extensions;
using YoumaconSecurityOps.Core.EventStore.Events;
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

            //"The Opt part is important" The_Faid 2021-05-08
            CreateMap<EventBase, EventReader>()
                .ForMember(r => r.Data,
                    opt => opt.MapFrom(src => src.DataAsJson)); 
        }
    }
}

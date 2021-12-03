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

            CreateMap<IncidentWriter, IncidentReader>();

            CreateMap<StaffWriter, StaffReader>();

            CreateMap<ShiftWriter, ShiftReader>()
                .ForMember(sh => sh.StartingLocationId, opt => opt.MapFrom(src => src.StartingLocationId))
                .ForMember(sr => sr.StaffId, opt => opt.MapFrom(src => src.StaffMemberId));

            CreateMap<RadioWriter, RadioScheduleReader>()
                .ForMember(r => r.LastStaffToHaveId, opt => opt.MapFrom(rw => rw.LastStaffToHaveId))
                .ForMember(r => r.LocationId, opt => opt.MapFrom(rw => rw.LocationId));

            //"The Opt part is important" The_Faid 2021-05-08
            CreateMap<EventBase, EventReader>()
                .ForMember(r => r.Data,
                    opt => opt.MapFrom(src => src.DataAsJson)); 
        }
    }
}

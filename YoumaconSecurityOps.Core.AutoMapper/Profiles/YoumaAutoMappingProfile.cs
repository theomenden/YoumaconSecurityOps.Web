
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

            CreateMap<StaffTypeRoleMapWriter, StaffTypesRole>()
                .ForMember(str => str.Staff, opt => opt.Ignore());

            CreateMap<ShiftWriter, ShiftReader>()
                .ForMember(sh => sh.StartingLocationId, opt => opt.MapFrom(src => src.StartingLocationId))
                .ForMember(sr => sr.StaffId, opt => opt.MapFrom(src => src.StaffMemberId));

            CreateMap<RadioWriter, RadioScheduleReader>()
                .ForMember(r => r.LastStaffToHave_Id, opt => opt.MapFrom(rw => rw.LastStaffToHaveId))
                .ForMember(r => r.Location_Id, opt => opt.MapFrom(rw => rw.LocationId));

            //"The Opt part is important" The_Faid 2021-05-08
            CreateMap<EventBase, EventReader>()
                .ForMember(r => r.Data,
                    opt => opt.MapFrom(src => src.DataAsJson)); 
        }
    }
}

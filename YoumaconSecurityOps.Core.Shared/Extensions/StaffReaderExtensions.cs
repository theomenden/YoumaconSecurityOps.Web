
namespace YoumaconSecurityOps.Core.Shared.Extensions;

public static class StaffReaderExtensions
{
    public static StaffReader FromFullStaff(this FullStaffWriter fullStaffWriter)
    {
        return new()
        {
            ContactInformation = AddContactInformation(fullStaffWriter.StaffWriter.Id, fullStaffWriter.ContactWriter),
            StaffTypesRoles = AddStaffTypesRoles(fullStaffWriter.StaffWriter.Id, fullStaffWriter.StaffTypeRoleMapWriter),
            ShirtSize = fullStaffWriter.StaffWriter.ShirtSize,
            IsRaveApproved = fullStaffWriter.StaffWriter.IsRaveApproved,
            NeedsCrashSpace = fullStaffWriter.StaffWriter.NeedsCrashSpace,
            Id = fullStaffWriter.StaffWriter.Id
        };
    }

    private static ContactReader AddContactInformation(Guid staffId, ContactWriter contactWriter)
    {
        return new()
        {
            Staff_Id = staffId,
            CreatedOn = contactWriter.CreatedOn,
            Email = contactWriter.Email,
            FacebookName = contactWriter.FacebookName,
            FirstName = contactWriter.FirstName,
            LastName = contactWriter.LastName,
            Id = contactWriter.Id,
            PhoneNumber = contactWriter.PhoneNumber,
            PreferredName = contactWriter.PreferredName,
            Pronoun_Id = contactWriter.PronounId,
        };
    }

    private static List<StaffTypesRole> AddStaffTypesRoles(Guid staffId, StaffTypeRoleMapWriter staffTypeRoleMapWriter)
    {
        var initialRoles = new List<StaffTypesRole>(2);

        var defaultTypeRole = new StaffTypesRole
        {
            Id = Guid.NewGuid(),
            StaffId = staffId,
            StaffTypeId = 4,
            RoleId = 5
        };

        initialRoles.Add(defaultTypeRole);

        if (staffTypeRoleMapWriter.RoleId != defaultTypeRole.RoleId &&
            staffTypeRoleMapWriter.StaffTypeId != defaultTypeRole.StaffTypeId)
        {
            var additionalRole = new StaffTypesRole
            {
                Id = staffTypeRoleMapWriter.Id,
                StaffId = staffId,
                StaffTypeId = staffTypeRoleMapWriter.StaffTypeId,
                RoleId = staffTypeRoleMapWriter.RoleId
            };

            initialRoles.Add(additionalRole);
        }

        return initialRoles;
    }
}


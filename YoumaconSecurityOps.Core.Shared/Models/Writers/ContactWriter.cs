namespace YoumaconSecurityOps.Core.Shared.Models.Writers;

public record ContactWriter(Guid StaffId, DateTime CreatedOn, String Email, String FirstName, String LastName,
    String FacebookName, String PreferredName, Int64 PhoneNumber) : BaseWriter;
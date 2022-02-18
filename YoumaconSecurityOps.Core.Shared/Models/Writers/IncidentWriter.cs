namespace YoumaconSecurityOps.Core.Shared.Models.Writers;

public record IncidentWriter(Guid RecordedById, Guid ReportedById, Guid ShiftId, Guid LocationId
    , Severity Severity, String Title, String Description) : BaseWriter;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace YoumaconSecurityOps.Core.Shared.Models.Readers
{
    /// <summary>
    /// Represents an incident row in the database. 
    /// </summary>
    /// <remarks>Type of: <see cref="BaseReader"/></remarks>
    [Table("Incidents")]
    [Index(nameof(RecordedById), Name = "IX_Incidents_RecordedBy")]
    [Index(nameof(ReportedById), Name = "IX_Incidents_ReportedBy")]
    [Index(nameof(RecordedOn), nameof(Severity), Name = "IX_Incidents_Severity")]
    public partial class IncidentReader: BaseReader
    {
        [Column("RecordedBy_Id")]
        public Guid RecordedById { get; set; }

        [Column("ReportedBy_Id")]
        public Guid ReportedById { get; set; }

        [Column("Shift_Id")]
        public Guid ShiftId { get; set; }

        [Column("Location_Id")]
        public Guid LocationId { get; set; }

        public int Severity { get; set; }

        public DateTime RecordedOn { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = default!;

        [Required]
        [StringLength(1000)]
        public string Description { get; set; } = default!;

        public DateTime? ResolvedAt { get; set; }

        [ForeignKey(nameof(LocationId))]
        [InverseProperty("Incidents")]
        public virtual LocationReader Location { get; set; } = default!;

        [ForeignKey(nameof(RecordedById))]
        [InverseProperty(nameof(StaffReader.IncidentRecordedBy))]
        public virtual StaffReader RecordedBy { get; set; } = default!;

        [ForeignKey(nameof(ReportedById))]
        [InverseProperty(nameof(StaffReader.IncidentReportedBy))]
        public virtual StaffReader ReportedBy { get; set; } = default!;

        [ForeignKey(nameof(ShiftId))]
        [InverseProperty("Incidents")]
        public virtual ShiftReader Shift { get; set; } = default!;
    }
}

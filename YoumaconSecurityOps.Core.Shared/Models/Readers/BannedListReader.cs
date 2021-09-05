using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YoumaconSecurityOps.Core.Shared.Models.Readers
{
    [Table("BannedList")]
    public partial class BannedListReader: BaseReader
    {
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; } = default!;

        [Required]
        [StringLength(100)]
        public string LastName { get; set; } = default!;

        [Required]
        [StringLength(1000)]
        public string Reason { get; set; } = default!;

        public DateTime? LastSeenAt { get; set; }
    }
}

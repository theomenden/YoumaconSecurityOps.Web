namespace YoumaconSecurityOps.Core.Shared.Models.Readers;

[Table("WatchList")]
public partial class WatchListReader: BaseReader
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
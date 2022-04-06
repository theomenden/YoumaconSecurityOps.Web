namespace YoumaconSecurityOps.Core.Shared.Models.Readers;

[Table("WatchList")]
public partial class WatchList : IEntity
{
    [Key]
    public Guid Id { get; set; }
    [StringLength(100)]
    public string FirstName { get; set; } = null!;
    [StringLength(100)]
    public string LastName { get; set; } = null!;
    [StringLength(1000)]
    public string Reason { get; set; } = null!;
    public DateTime? LastSeenAt { get; set; }
}
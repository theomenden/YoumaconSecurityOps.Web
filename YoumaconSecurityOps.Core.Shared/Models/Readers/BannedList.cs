namespace YoumaconSecurityOps.Core.Shared.Models.Readers;

[Table("BannedList")]
public partial class BannedList : IEntity
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
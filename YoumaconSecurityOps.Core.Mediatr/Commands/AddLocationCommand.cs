namespace YoumaconSecurityOps.Core.Mediatr.Commands;

public record AddLocationCommand : ICommand
{
    public AddLocationCommand(string name, bool isHotel)
    {
        Id = Guid.NewGuid();
        Name = name;
        IsHotel = isHotel;
    }

    public Guid Id { get; }

    public string Name { get; }

    public bool IsHotel { get; }
}
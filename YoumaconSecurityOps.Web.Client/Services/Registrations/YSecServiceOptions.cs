namespace YoumaconSecurityOps.Web.Client.Services.Registrations;

public class YSecServiceOptions
{
    public YSecServiceOptions()
    {
        CreatedAt = DateTime.Now;
    }

    public DateTime CreatedAt { get; }

    public TimeSpan SlidingWindow { get; set; } = TimeSpan.FromMinutes(5);

    public TimeSpan AbsoluteWindow { get; set; } = TimeSpan.FromMinutes(30);


    public DateTime GetSlidingWindowRelativeToNow()
    {
        return CreatedAt.Add(SlidingWindow);
    }

    public DateTime GetAbsoluteWindowRelativeToNow()
    {
        return CreatedAt.Add(AbsoluteWindow);
    }
}


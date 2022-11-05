namespace YoumaconSecurityOps.Web.Client.Invariants;

public static class NavigateTo
{
    public static string UserProfile(string userName)
        => $"user\\{userName}";
}
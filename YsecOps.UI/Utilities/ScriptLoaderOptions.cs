namespace YsecOps.UI.Utilities;

internal sealed record ScriptLoaderOptions(bool IsAsync, bool IsDeferred, string AppendedTo, int MaxRetries, int RetryDelay)
{
    public string Id { get; init; } = String.Empty;

    public static readonly ScriptLoaderOptions Default = new(true, true, "head", 3, 25);
}

namespace YoumaconSecurityOps.Core.Shared.Responses;

/// <summary>
/// A holder that reflects the failure to process a model (command, query, etc) that cannot pass internal validation.
/// </summary>
public class InvalidRequestPayload
{
    public String Title { get; } = "One or more validation errors have occurred.";

    /// <value>
    /// A keyed collection of the validation failures on the model
    /// </value>
    public IDictionary<String, IEnumerable<String>> Errors { get; set; }
}


namespace YoumaconSecurityOps.Core.Shared.Responses;

public class OperationOutcome
{
    public OperationOutcome()
    {
        Message = String.Empty;
        CorrelationId = String.Empty;
        Errors = Enumerable.Empty<String>();
    }

    public OperationResult OperationResult { get; set; }

    public String CorrelationId { get; set; }

    public String Message { get; set; }

    public Boolean IsError { get; set; }

    public Boolean IsValidationFailure { get; set; }

    public IEnumerable<String> Errors { get; set; }

    public static OperationOutcome SuccessfulOutcome => new()
    {
        Errors= Enumerable.Empty<String>(),
        CorrelationId = String.Empty,
        IsError = false,
        IsValidationFailure = false,
        Message = String.Empty,
        OperationResult = OperationResult.Success
    };

    public static OperationOutcome UnsuccessfulOutcome => new()
    {
        Errors = Enumerable.Empty<String>(),
        CorrelationId = String.Empty,
        IsError = true,
        IsValidationFailure = false,
        Message = String.Empty,
        OperationResult = OperationResult.Failure
    };

    public static OperationOutcome ValidationFailureOutcome(IEnumerable<String> errors, String message = null) => new()
    {
        Errors = errors ?? Enumerable.Empty<String>(),
        CorrelationId = String.Empty,
        IsError = false,
        IsValidationFailure = true,
        Message = message ?? String.Empty,
        OperationResult = OperationResult.Failure
    };
}


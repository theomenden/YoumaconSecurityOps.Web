using System.Text;

namespace YoumaconSecurityOps.Web.Client.Components;

public partial class ErrorComponent : ComponentBase
{
    [Parameter] public IEnumerable<ApiResponse>? ApiErrors { get; init; }

    private String GetResponseMessages()
    {
        var sb = new StringBuilder();

        sb.AppendLine("Errors are as follows");
        
            sb.AppendJoin(Environment.NewLine, ApiErrors?.Select(ae => ae?.ResponseMessage) ?? Array.Empty<String>());
        
        return sb.ToString();
    }

    private String GetResponseCodes()
    {
        var sb = new StringBuilder();

        sb.Append("Response Codes: ");
        
        sb.AppendJoin(' ',
            ApiErrors?.GroupBy(ae => ae?.ResponseCode).Select(r => $"{r?.Key} {r?.Count()}") ?? Array.Empty<String>());

        return sb.ToString();
    }

    private String GetCorrelationIds()
    {
        var sb = new StringBuilder();

        sb.Append("Correlation Ids: ");
        
        sb.AppendJoin(Environment.NewLine, ApiErrors?.Select(ae => ae?.Outcome.CorrelationId) ?? Array.Empty<String>());

        return sb.ToString();
    }
}
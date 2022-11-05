using TheOmenDen.Shared.Extensions;

namespace YoumaconSecurityOps.Web.Client.Components;

public partial class ErrorComponent : ComponentBase, IDisposable
{
    private Boolean _disposedValue;

    [Parameter] public IEnumerable<ApiResponse>? ApiErrors { get; init; }

    protected override void OnInitialized()
    {
        if(!StringBuilderPoolFactory<ErrorComponent>.Exists(nameof(ErrorComponent)))
        {
            StringBuilderPoolFactory<ErrorComponent>.Create(nameof(ErrorComponent));
        }
    }

    private String GetResponseMessages()
    {
        var sb = StringBuilderPoolFactory<ErrorComponent>.Get(nameof(ErrorComponent));

        sb.Clear();

        sb.AppendLine("Errors are as follows");
        
            sb.AppendJoin(Environment.NewLine, ApiErrors?.Select(ae => ae?.ResponseMessage) ?? Array.Empty<String>());
        
        return sb.ToString();
    }

    private String GetResponseCodes()
    {
        var sb = StringBuilderPoolFactory<ErrorComponent>.Get(nameof(ErrorComponent));

        sb.Clear();

        sb.Append("Response Codes: ");
        
        sb.AppendJoin(' ',
            ApiErrors?.GroupBy(ae => ae?.ResponseCode).Select(r => $"{r?.Key} {r?.Count()}") ?? Array.Empty<String>());

        return sb.ToString();
    }

    private String GetCorrelationIds()
    {
        var sb = StringBuilderPoolFactory<ErrorComponent>.Get(nameof(ErrorComponent));

        sb.Clear();

        sb.Append("Correlation Ids: ");
        
        sb.AppendJoin(Environment.NewLine, ApiErrors?.Select(ae => ae?.Outcome.CorrelationId) ?? Array.Empty<String>());

        return sb.ToString();
    }

    protected virtual void Dispose(Boolean disposing)
    {
        if (!_disposedValue)
        {
            if (disposing && StringBuilderPoolFactory<ErrorComponent>.Exists(nameof(ErrorComponent)))
            {
                StringBuilderPoolFactory<ErrorComponent>.Remove(nameof(ErrorComponent));
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            _disposedValue = true;
        }
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    // ~ErrorComponent()
    // {
    //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
    //     Dispose(disposing: false);
    // }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
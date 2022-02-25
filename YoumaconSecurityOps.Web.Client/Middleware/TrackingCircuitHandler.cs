namespace YoumaconSecurityOps.Web.Client.Middleware;

public class TrackingCircuitHandler : CircuitHandler
{

    public TrackingCircuitHandler(SessionDetails sessionData)
    {
        SessionData = sessionData;
    }

    public event EventHandler CircuitsChanged;

    public string CircuitId { get; set; }

    public SessionDetails SessionData { get; set; }

    protected virtual void OnCircuitsChanged() => CircuitsChanged?.Invoke(this, EventArgs.Empty);

    public override Task OnCircuitClosedAsync(Circuit circuit, CancellationToken cancellationToken)
    {
        SessionData.Delete(circuit.Id);

        OnCircuitsChanged();

        return base.OnCircuitClosedAsync(circuit, cancellationToken);
    }

    public override Task OnCircuitOpenedAsync(Circuit circuit, CancellationToken cancellationToken)
    {
        CircuitId = circuit.Id;

        OnCircuitsChanged();
        
        return base.OnCircuitOpenedAsync(circuit, cancellationToken);
    }

    public override Task OnConnectionDownAsync(Circuit circuit, CancellationToken cancellationToken)
    {
        return base.OnConnectionDownAsync(circuit, cancellationToken);
    }

    public override Task OnConnectionUpAsync(Circuit circuit, CancellationToken cancellationToken)
    {
        return base.OnConnectionUpAsync(circuit, cancellationToken);
    }
}


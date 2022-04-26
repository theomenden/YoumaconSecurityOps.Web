#nullable disable
namespace YoumaconSecurityOps.Web.Client.Models;

public sealed class SessionDetails : IDisposable, IAsyncDisposable
{
    private readonly List<SessionModel> _sessions = new(30);

    private readonly ILogger<SessionDetails> _logger;

    public SessionDetails(ILogger<SessionDetails> logger)
    {
        _logger = logger;
    }

    public void Add(SessionModel session)
    {
        if (_sessions.Contains(session))
        {
            return;
        }

        session.CreatedAt = DateTime.Now;

        _sessions.Add(session);

        _logger.LogInformation("Successfully Added Session with Id {session}", session.Id);
    }

    public void Delete(Guid sessionId)
    {
        if (sessionId == Guid.Empty)
        {

            _logger.LogError("Could not remove session");
            return;
        }

        _sessions.RemoveAll(session => session.Id.Equals(sessionId));
    }

    public void Delete(String? circuitId)
    {
        if (String.IsNullOrWhiteSpace(circuitId))
        {
            _logger.LogError("Circuit Id was not specified");
            return;
        }

        if (_sessions.RemoveAll(session => session.CircuitId?.Equals(circuitId) == true) == 0)
        {
            _logger.LogError("Session with circuitId {CircuitId} Could not be found", circuitId);
        }
    }

    public SessionModel Get(Guid sessionId)
    {
        return _sessions.First(session => session.Id == sessionId);
    }

    public SessionModel Get(String circuitId)
    {
        return _sessions.First(session => session.CircuitId.Equals(circuitId));
    }

    public void Dispose()
    {
        _sessions.Clear();
    }

    public ValueTask DisposeAsync()
    {
        _sessions.Clear();

        return ValueTask.CompletedTask;
    }
}


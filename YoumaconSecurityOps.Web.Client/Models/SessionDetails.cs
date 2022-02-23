#nullable enable
using Microsoft.Extensions.Logging;

namespace YoumaconSecurityOps.Web.Client.Models;

public sealed class SessionDetails: IDisposable
{
    private readonly List<SessionModel> _sessions = new(30);

    private readonly ILogger<SessionDetails> _logger;

    public SessionDetails(ILogger<SessionDetails> logger)
    {
        _logger = logger;
    }

    public IEnumerable<SessionModel> Sessions => _sessions;

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
        try
        {
            _sessions.RemoveAll(session => session.Id.Equals(sessionId));
        }
        catch (ArgumentNullException ex)
        {
            _logger.LogError("Could not remove session(s) with Id {sessionId}: {@ex}", sessionId, ex);
        }
    }

    public void Delete(String? circuitId)
    {
        try
        {
            _sessions.RemoveAll(session => session.CircuitId?.Equals(circuitId) == true);
        }
        catch (ArgumentNullException ex)
        {
            _logger.LogError("Could not remove session(s) due to: {@ex}", ex);
        }
        catch (NullReferenceException ex)
        {
            _logger.LogError("Unable to perform session removal for session with Id: {circuitId}: {@ex}", circuitId,
                ex);
        }
        catch (Exception ex)
        {
            _logger.LogError("Unintelligible response created while trying to remove {circuitId}: {@ex}", circuitId, ex);
        }
    }

    public SessionModel Get(Guid sessionId)
    {
        return _sessions.FirstOrDefault(session => session.Id == sessionId);
    }

    public SessionModel Get(String circuitId)
    {
        return _sessions.FirstOrDefault(session => session.CircuitId.Equals(circuitId));
    }

    public void Dispose()
    {
        _sessions.Clear();
    }
}


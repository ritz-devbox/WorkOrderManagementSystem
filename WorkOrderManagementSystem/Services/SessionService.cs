using WorkOrderManagementSystem.Data;

namespace WorkOrderManagementSystem.Services;

public class SessionService
{
    public Guid InstanceId { get; } = Guid.NewGuid();
    private WorkerAccount? _currentUser;
    private string? _sessionToken;
    private DateTime? _loginTime;

    public event Action? OnChange;

    public bool IsAuthenticated => _currentUser != null;
    
    public WorkerAccount? CurrentUser => _currentUser;
    
    public Role? CurrentRole => _currentUser?.Role;
    
    public string? SessionToken => _sessionToken;

    public void Login(WorkerAccount user, string token)
    {
        _currentUser = user;
        _sessionToken = token;
        _loginTime = DateTime.UtcNow;
        NotifyStateChanged();
    }

    public void Logout()
    {
        _currentUser = null;
        _sessionToken = null;
        _loginTime = null;
        NotifyStateChanged();
    }

    public bool IsSessionExpired()
    {
        if (_loginTime == null) return true;
        var sessionDuration = DateTime.UtcNow - _loginTime.Value;
        return sessionDuration.TotalMinutes > 480; // 8 hours
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}

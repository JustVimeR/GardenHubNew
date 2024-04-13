using System.Collections.Generic;

public class AvailableConnections
{
    public string? ChatsConnection;

    public string? NotificationsConnection;
}

public class UserConnectionManager : IUserConnectionManager
{
    private readonly Dictionary<string, AvailableConnections> _userConnections = new Dictionary<string, AvailableConnections>();

    private readonly object _lock = new object();

    public void AddChatsConnection(string userId, string connectionId)
    {
        lock (_lock)
        {
            if (_userConnections.ContainsKey(userId) && _userConnections[userId].ChatsConnection == null)
            {
                _userConnections[userId].ChatsConnection = connectionId;
            }
            else
            {
                _userConnections[userId] = new() { ChatsConnection = connectionId };
            }
        }
    }

    public void AddNotificationsConnection(string userId, string connectionId)
    {
        lock (_lock)
        {
            if (_userConnections.ContainsKey(userId) && _userConnections[userId].NotificationsConnection == null)
            {
                _userConnections[userId].NotificationsConnection = connectionId;
            }
            else
            {
                _userConnections[userId] = new() { NotificationsConnection = connectionId };
            }
        }
    }

    public void RemoveChatsConnection(string userId)
    {
        lock (_lock)
        {
            if (_userConnections.ContainsKey(userId))
            {
                if (_userConnections[userId].NotificationsConnection == null)
                {
                    _userConnections.Remove(userId);
                }
                else
                {
                    _userConnections[userId].ChatsConnection = null;
                }
            }
        }
    }

    public void RemoveNotificationsConnection(string userId)
    {
        lock (_lock)
        {
            if (_userConnections.ContainsKey(userId))
            {
                if (_userConnections[userId].ChatsConnection == null)
                {
                    _userConnections.Remove(userId);
                }
                else
                {
                    _userConnections[userId].NotificationsConnection = null;
                }
            }
        }
    }

    public AvailableConnections GetConnectionsForUser(string userId)
    {
        return _userConnections.GetValueOrDefault(userId) ?? new();
    }
}

public interface IUserConnectionManager
{
    public void AddChatsConnection(string userId, string connectionId);
    public void AddNotificationsConnection(string userId, string connectionId);
    public void RemoveChatsConnection(string userId);
    public void RemoveNotificationsConnection(string userId);
    public AvailableConnections GetConnectionsForUser(string userId);
}

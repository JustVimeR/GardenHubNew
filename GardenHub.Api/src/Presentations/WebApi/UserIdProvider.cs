using Core.Constants;
using Microsoft.AspNetCore.SignalR;


public class UserIdProvider : IUserIdProvider
{
    public string? GetUserId(HubConnectionContext connection)
    {
        return connection.User?.FindFirst(Defaults.UserProfileIdClaimIdentifier)?.Value;
    }
}

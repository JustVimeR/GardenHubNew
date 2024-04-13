using Microsoft.AspNetCore.SignalR;
using Services.GardenhubServices.Interfaces;
using System;
using System.Threading.Tasks;


public class ChatHub : Hub
{
    private readonly IChatService _chatService;
    private readonly IUserConnectionManager _userConnectionManager;

    public ChatHub(IUserConnectionManager userConnectionManager, IChatService chatService)
    {
        _chatService = chatService;
        _userConnectionManager = userConnectionManager;
    }

    public async override Task OnDisconnectedAsync(Exception? exception)
    {
        string userId = Context.UserIdentifier!;

        _userConnectionManager.RemoveChatsConnection(userId);

        await base.OnDisconnectedAsync(exception);
    }

    public async override Task OnConnectedAsync()
    {
        string userId = Context.UserIdentifier!;
        string connectionId = Context.ConnectionId;

        _userConnectionManager.AddChatsConnection(userId, connectionId);

        await base.OnConnectedAsync();
    }

    public async Task SendMessage(string message, string receiverId)
    {
        string userId = Context.UserIdentifier!;

        AvailableConnections connections = _userConnectionManager.GetConnectionsForUser(receiverId);

        if (connections.ChatsConnection != null)
            await Clients.Client(connections.ChatsConnection).SendAsync("ReceiveMessage", userId, message);

        await _chatService.SaveChatMessage(long.Parse(receiverId), long.Parse(userId), message);
    }
}

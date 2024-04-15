using Core.Constants;
using Microsoft.AspNetCore.SignalR;
using MimeKit;
using Models.DbEntities;
using Models.DTOs.GetDTOs;
using Services.GardenhubServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class NotificationHub : Hub
{
    private readonly IProjectService _projectService;
    private readonly IChatService _chatService;
    private readonly IUserConnectionManager _userConnectionManager;

    public NotificationHub(IUserConnectionManager userConnectionManager, IChatService chatService,
        IProjectService projectService)
    {
        _projectService = projectService;
        _chatService = chatService;
        _userConnectionManager = userConnectionManager;
    }

    public async override Task OnDisconnectedAsync(Exception? exception)
    {
        string userId = Context.UserIdentifier!;

        _userConnectionManager.RemoveNotificationsConnection(userId);

        await base.OnDisconnectedAsync(exception);
    }

    public async override Task OnConnectedAsync()
    {
        string userId = Context.UserIdentifier!;
        string connectionId = Context.ConnectionId;

        _userConnectionManager.AddNotificationsConnection(userId, connectionId);

        await base.OnConnectedAsync();
    }

    public async Task SendProjectApply(string message, string projectId)
    {
        string userId = Context.UserIdentifier!;

        Project project = await _projectService.GetFirstAsync(x => x.Id == long.Parse(projectId));

        long receiverId = project.CustomerId;

        AvailableConnections connections = _userConnectionManager.GetConnectionsForUser(receiverId.ToString());

        if (connections.NotificationsConnection != null)
            await Clients.Client(connections.NotificationsConnection).SendAsync("ReceiveProjectApply", userId,
                projectId, message);

        message = string.Format(Defaults.ApplyNotificationPrefix, project)
            + Environment.NewLine + message;
        await _chatService.SaveNotificationMessage(receiverId, long.Parse(userId), message);
    }
}

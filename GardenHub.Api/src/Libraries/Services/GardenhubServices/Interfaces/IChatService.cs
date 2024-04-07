﻿using Models.DbEntities;
using Models.DTOs.GetDTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.GardenhubServices.Interfaces;

public interface IChatService : IService<Chat>
{
    public Task SaveChatMessage(long receiverId, long senderId, string message);

    public Task SaveNotificationMessage(long receiverId, long senderId, string message);

    public Task<List<GetMiniChatDTO>> GetUserChats();

    public Task<GetChatDTO> GetUserChat(long chatId);

    public Task<List<GetChatMessageDTO>> GetUserNotifications();
}

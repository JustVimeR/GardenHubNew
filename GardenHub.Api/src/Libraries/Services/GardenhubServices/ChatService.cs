using AutoMapper;
using Data.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.DbEntities;
using Models.DTOs.GetDTOs;
using Services.GardenhubServices.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.GardenhubServices;

public class ChatService : Service<Chat>, IChatService
{
    private readonly IMapper _mapper;
    private readonly IUserAccessor _userAccessor;
    private readonly IChatMessageRepository _messageRepository;

    public ChatService(IChatRepository chatRepository, IChatMessageRepository messageRepository,
        IUserAccessor userAccessor, IMapper mapper)
        : base(chatRepository)
    {
        _mapper = mapper;
        _userAccessor = userAccessor;
        _messageRepository = messageRepository;
    }

    public async Task SaveChatMessage(long receiverId, long senderId, string message)
    {
        Chat? chat = await base.GetFirstOrDefaultAsync(x =>
                x.User1Id == receiverId && x.User2Id == senderId ||
                x.User1Id == senderId && x.User2Id == receiverId);

        ChatMessage chatMessage = new ChatMessage() { SenderUserId = senderId, Message = message };

        if (chat == null)
        {
            chat = new Chat()
            {
                Name = "Private",
                User1Id = senderId,
                User2Id = receiverId
            };

            chat.ChatMessages = [chatMessage];
            await base.PostAsync(chat);
        }
        else
        {
            chatMessage.ChatId = chat.Id;
            await _messageRepository.Post(chatMessage);
            await _messageRepository.SaveChangesAsync();
        }
    }

    public async Task SaveNotificationMessage(long receiverId, long senderId, string message)
    {
        Chat chat = await base.GetFirstAsync(x =>
                x.NotificationOwnerId == receiverId);

        ChatMessage chatMessage = new ChatMessage
        {
            SenderUserId = senderId,
            ChatId = chat.Id,
            Message = message
        };

        await _messageRepository.Post(chatMessage);
        await _messageRepository.SaveChangesAsync();
    }

    public async Task<List<GetChatDTO>> GetUserChats()
    {
        return await _repository.GetWhere(
            x => x.User1Id == _userAccessor.UserProfileId ||
                x.User2Id == _userAccessor.UserProfileId,
                    y => y.Include(z => z.User1)
                          .Include(z => z.User2))
                          .Select(z => new GetChatDTO
                          {
                              Id = z.Id,

                              ChatMessages = _mapper.Map<List<GetChatMessageDTO>>(z.ChatMessages
                                              .OrderByDescending(m => m.PublicationDate)
                                              .Take(100).ToList()),

                              InterlocutorProfile = _mapper.Map<GetUserProfileDTO>(z.User1 ?? z.User2)
                          }).ToListAsync();
    }

    public async Task<List<GetChatMessageDTO>> GetUserNotifications()
    {
        Chat notificationChat = await _repository.GetFirstAsync(
                    x => x.NotificationOwnerId == _userAccessor.UserProfileId,
                    y => y.Include(z => z.ChatMessages)
                            .ThenInclude(z => z.SenderUser));

        return _mapper.Map<List<GetChatMessageDTO>>(notificationChat.ChatMessages);
    }

}

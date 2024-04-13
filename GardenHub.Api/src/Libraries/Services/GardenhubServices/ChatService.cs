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
    private readonly IChatMessageRepository _messageRepository;

    public ChatService(IChatRepository chatRepository, IChatMessageRepository messageRepository, IMapper mapper)
        : base(chatRepository)
    {
        _mapper = mapper;
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

    public async Task<List<GetMiniChatDTO>> GetUserChats(long userId)
    {
        return await _repository.GetWhere(
            x => x.User1Id == userId && x.User2 != null ||
                x.User2Id == userId && x.User1 != null,
                    y => y.Include(z => z.User1)
                                .ThenInclude(z=>z!.Icon)
                          .Include(z => z.User2)
                                .ThenInclude(z=>z!.Icon)!)
                          .Select(z => new GetMiniChatDTO
                          {
                              Id = z.Id,

                              LastChatMessage = _mapper.Map<GetChatMessageDTO>(z.ChatMessages!
                                              .OrderByDescending(m => m.PublicationDate)
                                              .FirstOrDefault()),

                              InterlocutorProfile =
                              _mapper.Map<GetUserMiniProfile>(z.User1!.Id == userId ? z.User2 : z.User1 )
                          }).ToListAsync();
    }

    public async Task<GetChatDTO> GetUserChat(long chatId, long userId)
    {
        Chat chat = await _repository.GetFirstAsync(
            x => x.Id == chatId,
                    y => y.Include(z => z.ChatMessages!.OrderByDescending(x => x.PublicationDate)!.Take(50))
                    .Include(z => z.User1)
                        .ThenInclude(z => z!.Icon)
                    .Include(z => z.User2)
                        .ThenInclude(z => z!.Icon)!);

        GetChatDTO chatDto = _mapper.Map<GetChatDTO>(chat);

        if (chat.User1Id != userId)
        {
            chatDto.InterlocutorProfile = _mapper.Map<GetUserMiniProfile>(chat.User1);
        }
        else
        {
            chatDto.InterlocutorProfile = _mapper.Map<GetUserMiniProfile>(chat.User2);
        }

        return chatDto;
    }

    public async Task<List<ChatMessage>> GetUserNotifications(long userId)
    {
        Chat notificationChat = await _repository.GetFirstAsync(
                    x => x.NotificationOwnerId == userId,
                    y => y.Include(z => z.ChatMessages)!
                            .ThenInclude(z => z.SenderUser)!);

        return notificationChat.ChatMessages!;
    }

}

using Data.Contexts;
using Data.Repos.Interfaces;
using Models.DbEntities;

namespace Data.Repos;

public class ChatMessageRepository : Repository<ChatMessage>, IChatMessageRepository
{
    public ChatMessageRepository(ApplicationDbContext dataContext) : base(dataContext)
    {

    }
}

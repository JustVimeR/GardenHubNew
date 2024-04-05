using Data.Contexts;
using Data.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.DbEntities;
using System.Linq;

namespace Data.Repos;

public class ChatRepository : Repository<Chat>, IChatRepository
{
    public ChatRepository(ApplicationDbContext dataContext) : base(dataContext)
    {

    }

    protected override IQueryable<Chat> PrepareDbSet()
    {
        return base.PrepareDbSet().Include(x => x.ChatMessages);
    }
}

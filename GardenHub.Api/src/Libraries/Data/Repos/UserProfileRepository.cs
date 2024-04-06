using Data.Contexts;
using Data.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.DbEntities;
using System.Linq;

namespace Data.Repos;

public class UserProfileRepository : Repository<UserProfile>, IUserProfileRepository
{
    public UserProfileRepository(ApplicationDbContext dataContext) : base(dataContext)
    {

    }

    protected override IQueryable<UserProfile> PrepareDbSet()
    {
        return base.PrepareDbSet()
            .Include(x => x.Cities)
            .Include(x => x.WorkTypes)
            .Include(x=>x.GardenerProjects)
                .ThenInclude(x=>x.WorkTypes)
            .Include(x=>x.CustomerProjects)
                .ThenInclude(x => x.WorkTypes)
            .Include(x => x.GardenerFeedbacks)!
                .ThenInclude(x => x!.Customer);
    }
}


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
            .Include(x => x.CustomerProfile)
            .Include(x => x.GardenerProfile)
                .ThenInclude(x => x!.Cities)
            .Include(x => x.GardenerProfile)
                .ThenInclude(x => x!.WorkTypes);
    }
}


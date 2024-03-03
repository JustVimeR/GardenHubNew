using Data.Contexts;
using Data.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.DbEntities;
using System.Linq;

namespace Data.Repos;

public class GardenerProfileRepository : Repository<GardenerProfile>, IGardenerProfileRepository
{
    public GardenerProfileRepository(ApplicationDbContext dataContext) : base(dataContext)
    {

    }

    protected override IQueryable<GardenerProfile> PrepareDbSet()
    {
        return base.PrepareDbSet().Include(x => x.Cities).Include(x => x.WorkTypes);
    }
}

using Data.Contexts;
using Data.Repos.Interfaces;
using Models.DbEntities;

namespace Data.Repos.Concrete
{
    public class GardenerProfileRepository : Repository<GardenerProfile>, IGardenerProfileRepository
    {
        public GardenerProfileRepository(ApplicationDbContext dataContext) : base(dataContext)
        {

        }
    }
}

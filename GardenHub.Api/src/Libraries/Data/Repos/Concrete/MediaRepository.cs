using Data.Contexts;
using Data.Repos.Interfaces;
using Models.DbEntities;

namespace Data.Repos.Concrete
{
    public class MediaRepository : Repository<Media>, IMediaRepository
    {
        public MediaRepository(ApplicationDbContext dataContext) : base(dataContext)
        {

        }
    }
}

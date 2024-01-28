using Data.Contexts;
using Data.Repos.Interfaces;
using Models.DbEntities;

namespace Data.Repos.Concrete
{
    public class CityRepository : Repository<City>, ICityRepository
    {
        public CityRepository(ApplicationDbContext dataContext) : base(dataContext)
        {

        }
    }
}

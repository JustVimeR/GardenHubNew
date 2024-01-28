using Data.Repos.Interfaces;
using Models.DbEntities;
using Services.Interfaces;

namespace Services.Concrete
{
    public class CityService : Service<City>, ICityService
    {
        public CityService(ICityRepository repository) : base(repository)
        {
        }
    }
}

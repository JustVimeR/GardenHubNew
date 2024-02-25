using Data.Repos.Interfaces;
using Models.DbEntities;
using Services.GardenhubServices.Interfaces;

namespace Services.GardenhubServices;

public class CityService : Service<City>, ICityService
{
    public CityService(ICityRepository repository) : base(repository)
    {
    }
}
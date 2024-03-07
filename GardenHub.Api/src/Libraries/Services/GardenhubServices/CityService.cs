using Core.Constants;
using Core.Exceptions;
using Data.Repos.Interfaces;
using Models.DbEntities;
using Services.GardenhubServices.Interfaces;
using System.Net;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Services.GardenhubServices;

public class CityService : Service<City>, ICityService
{
    public CityService(ICityRepository repository) : base(repository)
    {

    }

    public override async Task<City> PostAsync(City addCity)
    {
        var existingCity = await GetFirstOrDefaultAsync(c => c.Name.ToLower() == addCity.Name.ToLower());

        if (existingCity != null)
        {
            throw new ApiException((int)HttpStatusCode.BadRequest, ErrorMessages.EntityAlreadyExists,
                nameof(City), addCity.Name);
        }
        return await base.PutAsync(addCity);
    }

    public override async Task<City> PutAsync(City updateCity)
    {
        var existingCity = await GetFirstOrDefaultAsync(c => c.Name.ToLower() == updateCity.Name.ToLower() && c.Id != updateCity.Id);

        if (existingCity != null)
        {
            throw new ApiException((int)HttpStatusCode.BadRequest, ErrorMessages.EntityAlreadyExists,
                nameof(City), updateCity.Name);
        }
        return await base.PutAsync(updateCity);
    }
}
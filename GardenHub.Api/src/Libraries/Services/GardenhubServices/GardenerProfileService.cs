using Data.Repos.Interfaces;
using Models.DbEntities;
using Services.GardenhubServices.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.GardenhubServices;

public class GardenerProfileService : Service<GardenerProfile>, IGardenerProfileService
{
    private ICityService _cityService;
    private IUserAccessor _userAccessor;
    private IUserProfileService _userProfileService;

    public GardenerProfileService(IGardenerProfileRepository repository, ICityService cityService,
        IUserAccessor userAccessor, IUserProfileService userProfileService) : base(repository)
    {
        _userProfileService = userProfileService;
        _userAccessor = userAccessor;
        _cityService = cityService;
    }

    //TODO:Transaction
    public override async Task<GardenerProfile> PostAsync(GardenerProfile gardenerProfile)
    {
        UserProfile userProfile = await _userAccessor.GetUserProfileAsync();

        List<City> addCities = gardenerProfile.Cities;

        gardenerProfile.Cities = null!;

        foreach (City addCity in addCities)
        {
            City? city = await _cityService.GetFirstOrDefaultAsync(x =>
                                                x.Name.ToLower() == addCity.Name.ToLower());
            if (city != null)
            {
                gardenerProfile.Cities.Add(city);
            }
            else
            {
                await _cityService.PostAsync(addCity);

                gardenerProfile.Cities.Add(addCity);
            }
        }

        await base.PostAsync(gardenerProfile);

        userProfile.GardenerProfile = gardenerProfile;

        await _userProfileService.PutAsync(userProfile);

        return await base.PostAsync(gardenerProfile);
    }
}

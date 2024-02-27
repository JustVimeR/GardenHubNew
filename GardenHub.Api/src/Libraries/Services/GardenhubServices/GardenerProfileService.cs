using AutoMapper;
using Core.Constants;
using Core.Exceptions;
using Data.Repos.Interfaces;
using Models.DbEntities;
using Services.GardenhubServices.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.GardenhubServices;

public class GardenerProfileService : Service<GardenerProfile>, IGardenerProfileService
{
    private readonly ICityService _cityService;
    private readonly IUserAccessor _userAccessor;
    private readonly IUserProfileService _userProfileService;
    private readonly IMapper _mapper;

    public GardenerProfileService(IGardenerProfileRepository repository, ICityService cityService,
        IUserAccessor userAccessor, IUserProfileService userProfileService, IMapper mapper) : base(repository)
    {
        _userProfileService = userProfileService;
        _userAccessor = userAccessor;
        _cityService = cityService;
        _mapper = mapper;
    }

    public override async Task<GardenerProfile> PutAsync(GardenerProfile updateGardenerProfile)
    {
        if (updateGardenerProfile.Id == default)
        {
            throw new ApiException(400, ErrorMessages.UpdateEntityWithNoId, nameof(GardenerProfile));
        }

        UserProfile userProfile = await _userAccessor.GetUserProfileAsync();

        GardenerProfile gardenerProfile = await base.GetFirstAsync(x => x.Id == updateGardenerProfile.Id);

        _mapper.Map(updateGardenerProfile, gardenerProfile);

        return await base.PutAsync(gardenerProfile);
    }

    public override async Task<GardenerProfile> PostAsync(GardenerProfile gardenerProfile)
    {
        UserProfile userProfile = await _userAccessor.GetUserProfileAsync();

        await LinkCitiesOfGardenerProfile(gardenerProfile);

        userProfile.GardenerProfile = gardenerProfile;

        await _userProfileService.PutAsync(userProfile);

        return gardenerProfile;
    }

    private async Task LinkCitiesOfGardenerProfile(GardenerProfile gardenerProfile)
    {
        List<City> addCities = gardenerProfile.Cities;

        gardenerProfile.Cities = null!;

        foreach (City addCity in addCities)
        {
            City? city;

            if (addCity.Id != default)
            {
                city = await _cityService.GetFirstOrDefaultAsync(x => x.Id == addCity.Id);

                if (city != null)
                {
                    gardenerProfile.Cities.Add(city);
                }
                else
                {
                    addCity.Id = default;

                    await _cityService.PostAsync(addCity);

                    gardenerProfile.Cities.Add(addCity);
                }
            }
            else
            {
                city = await _cityService.GetFirstOrDefaultAsync(x =>
                                                x.Name.ToLower() == addCity.Name.ToLower());
                if (city == null)
                {
                    gardenerProfile.Cities.Add(addCity);
                }
                else
                {
                    gardenerProfile.Cities.Add(city);
                }
            }
        }
    }
}

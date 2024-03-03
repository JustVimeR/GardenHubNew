using AutoMapper;
using Core.Constants;
using Core.Exceptions;
using Data.Repos.Interfaces;
using Models.DbEntities;
using Services.GardenhubServices.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.GardenhubServices;

public class GardenerProfileService : Service<GardenerProfile>, IGardenerProfileService
{
    private readonly ICityService _cityService;
    private readonly IUserAccessor _userAccessor;
    private readonly IWorkTypeService _workTypeService;
    private readonly IUserProfileService _userProfileService;
    private readonly IMapper _mapper;

    public GardenerProfileService(IGardenerProfileRepository repository, ICityService cityService,
        IUserAccessor userAccessor, IUserProfileService userProfileService, IWorkTypeService workTypeService,
        IMapper mapper) : base(repository)
    {
        _workTypeService = workTypeService;
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

        await LinkCitiesOfGardenerProfile(gardenerProfile);

        await LinkWorkTypesOfGardenerProfile(gardenerProfile);

        return await base.PutAsync(gardenerProfile);
    }

    public override async Task<GardenerProfile> PostAsync(GardenerProfile gardenerProfile)
    {
        UserProfile userProfile = await _userAccessor.GetUserProfileAsync();

        await LinkWorkTypesOfGardenerProfile(gardenerProfile);

        await LinkCitiesOfGardenerProfile(gardenerProfile);

        userProfile.GardenerProfile = gardenerProfile;

        await _userProfileService.PutAsync(userProfile);

        return gardenerProfile;
    }

    private async Task LinkCitiesOfGardenerProfile(GardenerProfile gardenerProfile)
    {
        if(gardenerProfile.Cities==null)
        {
            return;
        }

        List<City> addCities = gardenerProfile.Cities;

        gardenerProfile.Cities = new();

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

    private async Task LinkWorkTypesOfGardenerProfile(GardenerProfile gardenerProfile)
    {
        if (gardenerProfile.WorkTypes == null)
        {
            return;
        }

        List<WorkType> gardenerWorkTypes = gardenerProfile.WorkTypes;

        gardenerProfile.WorkTypes = await _workTypeService.GetDerivedWorkTypesById(
            gardenerProfile.WorkTypes.Select(x => x.Id).ToList());
    }
}

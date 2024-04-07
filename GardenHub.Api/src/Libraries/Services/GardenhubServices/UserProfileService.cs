using AutoMapper;
using Core.Constants;
using Core.Exceptions;
using Data.IdentityModels;
using Data.Repos.Interfaces;
using Models.DbEntities;
using Services.GardenhubServices.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Core.Extensions;
using System.Threading.Tasks;
using Models.DTOs.PostDTOs;
using Microsoft.EntityFrameworkCore;
using Models.DTOs.GetDTOs;

namespace Services.GardenhubServices;

public class UserProfileService : Service<UserProfile>, IUserProfileService
{
    private readonly ICityService _cityService;
    private readonly IUserAccessor _userAccessor;
    private readonly IWorkTypeService _workTypeService;
    private readonly IMapper _mapper;

    public UserProfileService(IUserProfileRepository repository, ICityService cityService,
        IUserAccessor userAccessor, IWorkTypeService workTypeService,
        IMapper mapper) : base(repository)
    {
        _userAccessor = userAccessor;
        _workTypeService = workTypeService;
        _cityService = cityService;
        _mapper = mapper;
    }

    public Task<UserProfile> GetUserProfileFromToken()
    {
        long identityUserId = _userAccessor.IdentityUserId;

        return base.GetFirstAsync(x => x.IdentityId == identityUserId);
    }

    public async Task CreateApplicationUser(ApplicationUser user)
    {
        UserProfile userProfile = _mapper.Map<UserProfile>(user);

        await base.PostAsync(userProfile);
    }

    public override async Task<UserProfile> PutAsync(UserProfile updateUserProfile)
    {
        if (updateUserProfile.Id == default)
        {
            throw new ApiException((int)HttpStatusCode.BadRequest, ErrorMessages.UpdateEntityWithNoId,
                nameof(UserProfile));
        }

        UserProfile userProfile = await GetUserProfileFromToken();

        _mapper.SelectiveMap<UserProfile, PostUserProfileDTO>(updateUserProfile, userProfile);

        if (updateUserProfile.IsGardener)
        {
            _mapper.SelectiveMap<UserProfile, PostGardenerProfileDTO>(updateUserProfile, userProfile);

            await LinkCitiesOfGardenerProfile(userProfile);

            await LinkWorkTypesOfGardenerProfile(userProfile);
        }

        return await base.PutAsync(userProfile);
    }

    public override async Task<UserProfile> PostAsync(UserProfile userProfile)
    {
        if (userProfile.IsGardener)
        {
            await LinkWorkTypesOfGardenerProfile(userProfile);

            await LinkCitiesOfGardenerProfile(userProfile);
        }

        userProfile.NotificationChat = new() { Name = "Notifications" };

        await base.PostAsync(userProfile);

        return userProfile;
    }

    private async Task LinkCitiesOfGardenerProfile(UserProfile gardenerProfile)
    {
        if (gardenerProfile.Cities == null)
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

    private async Task LinkWorkTypesOfGardenerProfile(UserProfile gardenerProfile)
    {
        if (gardenerProfile.WorkTypes == null)
        {
            return;
        }

        List<WorkType> gardenerWorkTypes = gardenerProfile.WorkTypes;

        gardenerProfile.WorkTypes = await _workTypeService.GetDerivedWorkTypesById(
            gardenerProfile.WorkTypes.Select(x => x.Id).ToList());
    }

    public Task<List<UserProfile>> GetGardenerProfiles()
    {
        return base.GetWhereAsync(x => x.IsGardener);
    }

    public async Task<List<GetUserMiniProfile>> GetTopGardeners()
    {
        return await _repository.GetWhere(x => x.IsGardener && x.GardenerFeedbacks.Count() != 0)
            .Select(x =>
            new GetUserMiniProfile
            {
                Id = x.Id,
                Icon = _mapper.Map<GetMediaDTO>(x.Icon),
                Name = x.Name,
                UserName = x.UserName,
                AvgRating = x.GardenerFeedbacks.Sum(f => f.Rating) / x.GardenerFeedbacks.Count()
            })
            .Take(7)
            .OrderByDescending(g => g.AvgRating)
            .ToListAsync();
    }
}
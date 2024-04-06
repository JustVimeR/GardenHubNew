using Data.IdentityModels;
using Models.DbEntities;
using Models.DTOs.GetDTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.GardenhubServices.Interfaces;

public interface IUserProfileService : IService<UserProfile>
{
    Task CreateApplicationUser(ApplicationUser user);

    Task<List<UserProfile>> GetGardenerProfiles();

    Task<List<GetGardenerMiniProfile>> GetTopGardeners();

    Task<UserProfile> GetUserProfileFromToken();
}

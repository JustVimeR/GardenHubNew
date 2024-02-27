using AutoMapper;
using Data.IdentityModels;
using Data.Repos.Interfaces;
using Models.DbEntities;
using Services.GardenhubServices.Interfaces;
using System.Threading.Tasks;

namespace Services.GardenhubServices;

public class UserProfileService : Service<UserProfile>, IUserProfileService
{
    private IMapper _mapper;

    public UserProfileService(IUserProfileRepository repository, IMapper mapper) : base(repository)
    {
        _mapper = mapper;
    }

    public async Task CreateApplicationUser(ApplicationUser user)
    {
        UserProfile userProfile = _mapper.Map<UserProfile>(user);

        userProfile.CustomerProfile = new();

        await base.PostAsync(userProfile);
    }

    public override async Task<UserProfile> PostAsync(UserProfile userProfile)
    {
        //entity.IdentityId = _userAccessor.IdentityUserId;
        //entity.CustomerProfile = new CustomerProfile() { CreatedAt = entity.CreatedAt, CreatedBy=entity.CreatedBy,
        //UpdatedAt=entity.UpdatedAt,UpdatedBy=entity.UpdatedBy};

        userProfile.CustomerProfile = _mapper.Map<CustomerProfile>(userProfile);

        return await base.PostAsync(userProfile);
    }
}
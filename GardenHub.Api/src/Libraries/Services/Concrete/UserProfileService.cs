using AutoMapper;
using Data.Repos.Interfaces;
using Models.DbEntities;
using Services.Interfaces;
using System.Threading.Tasks;

namespace Services.Concrete
{
    public class UserProfileService : Service<UserProfile>, IUserProfileService
    {
        private IMapper _mapper;

        public UserProfileService(IUserProfileRepository repository, IMapper mapper) : base(repository)
        {
            _mapper = mapper;
        }


        public override async Task<UserProfile[]> PostAsync(UserProfile[] entities)
        {
            foreach (var entity in entities)
            {
                //entity.IdentityId = _userAccessor.IdentityUserId;
                //entity.CustomerProfile = new CustomerProfile() { CreatedAt = entity.CreatedAt, CreatedBy=entity.CreatedBy,
                //UpdatedAt=entity.UpdatedAt,UpdatedBy=entity.UpdatedBy};

                entity.CustomerProfile = _mapper.Map<CustomerProfile>((IEntityBase)entity);
            }

            return await base.PostAsync(entities);
        }
    }
}

using Data.Repos.Interfaces;
using Models.DbEntities;
using Services.Interfaces;
using System.Threading.Tasks;

namespace Services.Concrete
{
    public class GardenerProfileService : Service<GardenerProfile>, IGardenerProfileService
    {
        private ICityRepository _cityRepository;
        private IUserAccessor _userAccessor;
        private IUserProfileRepository _userProfileRepository;

        public GardenerProfileService(IGardenerProfileRepository repository, ICityRepository cityRepository, IUserAccessor userAccessor, IUserProfileRepository userProfileRepository) : base(repository)
        {
            _userProfileRepository = userProfileRepository;
            _userAccessor = userAccessor;
            _cityRepository = cityRepository;
        }
        //TODO:Transaction
        public override async Task<GardenerProfile[]> PostAsync(GardenerProfile[] entities)
        {
            
            foreach (var entity in entities)
            {
                var userProfile = await _userAccessor.GetUserProfileAsync();
                for (int i = 0; i < entity.Cities.Count; i++)
                {
                    var city = await _cityRepository.GetFirstOrDefaultAsync(x => x.Name == entity.Cities[i].Name);
                    if (city != null)
                        entity.Cities[i] = city;
                    
                    userProfile.GardenerProfile = entity;
                    repository.Post(new[] { entity });
                    await repository.SaveChangesAsync();
                    _userProfileRepository.Put(userProfile);
                    
                    await _userProfileRepository.SaveChangesAsync();
                }

            }

            return await base.PostAsync(entities);
        }
    }
}

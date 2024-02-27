using Data.IdentityModels;
using Models.DbEntities;
using System.Threading.Tasks;

namespace Services.GardenhubServices.Interfaces;

public interface IUserProfileService : IService<UserProfile>
{
    Task CreateApplicationUser(ApplicationUser user);
}

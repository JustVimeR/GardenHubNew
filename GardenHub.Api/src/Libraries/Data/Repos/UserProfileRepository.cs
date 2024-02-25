using Data.Contexts;
using Data.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.DbEntities;
using System.Linq;

namespace Data.Repos;

public class UserProfileRepository : Repository<UserProfile>, IUserProfileRepository
{
    public UserProfileRepository(ApplicationDbContext dataContext) : base(dataContext)
    {

    }
}


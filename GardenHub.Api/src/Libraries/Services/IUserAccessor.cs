using Models.DbEntities;
using System;
using System.Threading.Tasks;

namespace Services
{
    public interface IUserAccessor
    {
        string Username { get; }
        int  IdentityUserId { get; }

        public Task<UserProfile> GetUserProfileAsync();
    }
}
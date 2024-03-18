using Models.DbEntities;
using System.Threading.Tasks;

namespace Services;

public interface IUserAccessor
{
    string Username { get; }

    int IdentityUserId { get; }
}
namespace Services;

public interface IUserAccessor
{
    string Username { get; }

    long IdentityUserId { get; }

    long UserProfileId { get; }
}
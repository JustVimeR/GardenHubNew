using Core.Constants;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;

namespace Services;

public class UserAccessor : IUserAccessor
{
    readonly IHttpContextAccessor _httpContextAccessor;

    public UserAccessor(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string Username => _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Name)!;
    public long IdentityUserId //=> int.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    {
        get
        {
            var uidClaim = _httpContextAccessor.HttpContext?.User.FindFirst(Defaults.IdentityIdClaimIdentifier);

            if (uidClaim != null && long.TryParse(uidClaim.Value, out var userId))
            {
                return userId;
            }

            // Handle the case where the 'uid' claim is not found or cannot be parsed to an integer.
            // You might want to throw an exception, return a default value, or handle it based on your requirements.
            throw new InvalidOperationException($"Unable to retrieve or parse '{Defaults.IdentityIdClaimIdentifier}' claim.");
        }
    }

    public long UserProfileId
    {
        get
        {
            var uidClaim = _httpContextAccessor.HttpContext?.User.FindFirst(Defaults.UserProfileIdClaimIdentifier);

            if (uidClaim != null && long.TryParse(uidClaim.Value, out var userId))
            {
                return userId;
            }

            throw new InvalidOperationException($"Unable to retrieve or parse '{Defaults.UserProfileIdClaimIdentifier}' claim.");
        }
    }
}
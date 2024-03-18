using Microsoft.AspNetCore.Http;
using Services.GardenhubServices.Interfaces;
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
    public int IdentityUserId //=> int.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    {
        get
        {
            var uidClaim = _httpContextAccessor.HttpContext?.User.FindFirst("uid");

            if (uidClaim != null && int.TryParse(uidClaim.Value, out var userId))
            {
                return userId;
            }

            // Handle the case where the 'uid' claim is not found or cannot be parsed to an integer.
            // You might want to throw an exception, return a default value, or handle it based on your requirements.
            throw new InvalidOperationException("Unable to retrieve or parse 'uid' claim.");
        }
    }
}
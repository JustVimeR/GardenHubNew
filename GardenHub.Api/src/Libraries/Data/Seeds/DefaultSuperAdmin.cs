using Data.IdentityModels;
using Data.Repos.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models.DbEntities;
using Models.Enums;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Seeds;

public static class DefaultSuperAdmin
{
    public static async Task SeedAsync(UserManager<ApplicationUser> userManager,
        IUserProfileRepository userProfileRepository)
    {
        //Seed Default User
        var defaultUser = new ApplicationUser
        {
            UserName = "string",
            Email = "string@gmail.com",
            FirstName = "string",
            LastName = "string",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true
        };

        ApplicationUser? user = await userManager.Users
            .Include(u => u.UserRoles)
            .Include(u => u.UserProfile)
            .FirstOrDefaultAsync(u => u.Email == defaultUser.Email);

        if (user == null)
        {
            await userManager.CreateAsync(defaultUser, "!1String");
            await userManager.AddToRoleAsync(defaultUser, Roles.Moderator.ToString());
            await userManager.AddToRoleAsync(defaultUser, Roles.User.ToString());
            await userManager.AddToRoleAsync(defaultUser, Roles.Gardener.ToString());

            await SeedDefaultUserProfile(userProfileRepository, defaultUser);
        }
        else
        {
            if (!user.UserRoles!.Any())
            {
                await userManager.AddToRoleAsync(user, Roles.Moderator.ToString());
                await userManager.AddToRoleAsync(user, Roles.User.ToString());
                await userManager.AddToRoleAsync(user, Roles.Gardener.ToString());
            }

            if (user.UserProfile == null)
            {
                await SeedDefaultUserProfile(userProfileRepository, user);
            }
        }
    }

    private static async Task SeedDefaultUserProfile(IUserProfileRepository userProfileRepository,
        ApplicationUser user)
    {
        UserProfile userProfile = new()
        {
            IdentityId = user.Id,
            Name = "Super",
            Surname = "User",
            Email = user.Email,
            UserName = user.UserName,
            Description = "Seeded Test User Profile",
            BirthDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-20)),

            CustomerProfile = new CustomerProfile(),
            GardenerProfile = new GardenerProfile()
        };

        await userProfileRepository.Post(userProfile);
        await userProfileRepository.SaveChangesAsync();
    }
}

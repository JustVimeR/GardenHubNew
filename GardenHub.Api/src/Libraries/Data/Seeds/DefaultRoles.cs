using Data.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models.Enums;
using System;
using System.Threading.Tasks;

namespace Data.Seeds;

public static class DefaultRoles
{
    public static async Task SeedAsync(RoleManager<ApplicationRole> roleManager)
    {
        if (await roleManager.Roles.AnyAsync())
        {
            return;
        }

        await roleManager.CreateAsync(new ApplicationRole()
        {
            Name = Roles.Moderator.ToString(),
            CreatedDate = DateTime.Now
        });

        await roleManager.CreateAsync(new ApplicationRole()
        {
            Name = Roles.User.ToString(),
            CreatedDate = DateTime.Now
        });

        await roleManager.CreateAsync(new ApplicationRole()
        {
            Name = Roles.Gardener.ToString(),
            CreatedDate = DateTime.Now
        });
    }
}


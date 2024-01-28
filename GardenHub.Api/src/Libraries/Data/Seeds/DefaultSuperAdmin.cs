using Data.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Seeds
{
    public static class DefaultSuperAdmin { 
    
    
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
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
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "!1String");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Moderator.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.User.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.Gardener.ToString());
                }

                await userManager.AddToRoleAsync(user, Roles.Moderator.ToString());
                await userManager.AddToRoleAsync(user, Roles.User.ToString());
                await userManager.AddToRoleAsync(user, Roles.Gardener.ToString());
            }
        }
    }
}

using Data.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Models.Enums;
using System;
using System.Threading.Tasks;

namespace Identity.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(RoleManager<ApplicationRole> roleManager)
        {
            //Seed Roles
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
}

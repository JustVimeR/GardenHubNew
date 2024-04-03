using Core.Constants;
using Core.Services.Interfaces;
using Data.IdentityModels;
using Data.Repos.Interfaces;
using Data.Seeds;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Core.Services;

public class PrepareAppService
{
    public async Task Prepare(WebApplication app)
    {
        using IServiceScope serviceScope = app.Services.CreateScope();
        IServiceProvider serviceProvider = serviceScope.ServiceProvider;

        serviceProvider.GetRequiredService<DbMigrator>().Migrate();

        var emailService = serviceProvider.GetRequiredService<IEmailService>();
        try
        {
            //await emailService.SendAsync(new EmailRequest()
            //{
            //    From = "apprunner@app.runner",
            //    To = "apprunner@app.runner",
            //    Body = $"APP IS RUNNING",
            //    Subject = "APP"
            //});
        }
        catch
        {
            serviceProvider.GetRequiredService<IGardenHubLogger<PrepareAppService>>()
                .Error(ErrorMessages.InvalidEmailSettings);
        }

        var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
        await DefaultRoles.SeedAsync(roleManager);

        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var userProfileRepository = serviceProvider.GetRequiredService<IUserProfileRepository>();
        await DefaultSuperAdmin.SeedAsync(userManager, userProfileRepository);
    }
}

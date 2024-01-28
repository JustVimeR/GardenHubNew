﻿using Core.Services.Interfaces;
using Data.IdentityModels;
using Identity.Seeds;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Models.DTOs.Email;
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
            await emailService.SendAsync(new EmailRequest()
            {
                From = "sender@email.com",
                To = "sssssss@sss.sss",
                Body = $"APP IS RUNNING",
                Subject = "APP"
            });
        }
        catch
        {
            serviceProvider.GetRequiredService<IGardenHubLogger<PrepareAppService>>()
                .Error("Test email wasn't sent, update mailtrap creadentioals");
        }

        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        await DefaultSuperAdmin.SeedAsync(userManager);

        var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
        await DefaultRoles.SeedAsync(roleManager);
    }
}

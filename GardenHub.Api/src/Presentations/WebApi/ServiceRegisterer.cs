using Core.Services;
using Core.Services.Concrete;
using Core.Services.Concrete.Logging;
using Core.Services.Interfaces;
using Data.Contexts;
using Data.IdentityModels;
using Data.Repos.Concrete;
using Data.Repos.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Models.ResponseModels;
using Models.Settings;
using Newtonsoft.Json;
using Services;
using Services.Concrete;
using Services.IdentityServices.Concrete;
using Services.IdentityServices.Interfaces;
using Services.Interfaces;
using System;
using System.Text;
using WebApi.Helpers;
using WebApi.Swagger;

namespace WebApi
{
    public class ServiceRegisterer
    { 
        public IServiceCollection RegisterServices(IServiceCollection services, IConfiguration config)
        {
            AddConfigOptions(services, config);

            AddTransientDependencyGroup(services);
            AddScopedDependencyGroup(services);
            AddSingletonDependencyGroup(services);

            return services;
        }

        public IServiceCollection AddConfiguredDbContextWithIdentity(IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<IDbContext, ApplicationDbContext>(options =>
                options.UseSqlServer(
                    config.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddIdentity<ApplicationUser, ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders()
                .AddTokenProvider("MyApp", typeof(DataProtectorTokenProvider<ApplicationUser>));

            return services;
        }

        public IServiceCollection AddConfiguredAuthenticationWithAuthorization(IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
               .AddJwtBearer(o =>
               {
                   o.RequireHttpsMetadata = false;
                   o.SaveToken = false;
                   o.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuerSigningKey = true,
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateLifetime = true,
                       ClockSkew = TimeSpan.Zero,
                       ValidIssuer = config["JWTSettings:Issuer"],
                       ValidAudience = config["JWTSettings:Audience"],
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWTSettings:Key"]))
                   };
                   o.Events = new JwtBearerEvents()
                   {
                       OnAuthenticationFailed = c =>
                       {
                           c.NoResult();
                           c.Response.StatusCode = 500;
                           c.Response.ContentType = "text/plain";
                           return c.Response.WriteAsync(c.Exception.ToString());
                       },
                       OnChallenge = context =>
                       {
                           context.HandleResponse();
                           context.Response.StatusCode = 401;
                           context.Response.ContentType = "application/json";
                           var result = JsonConvert.SerializeObject(new BaseResponse<string>("You are not Authorized"));
                           return context.Response.WriteAsync(result);
                       },
                       OnForbidden = context =>
                       {
                           context.Response.StatusCode = 403;
                           context.Response.ContentType = "application/json";
                           var result = JsonConvert.SerializeObject(new BaseResponse<string>("You are not authorized to access this resource"));
                           return context.Response.WriteAsync(result);
                       },
                   };
               });

            services.AddAuthorization();

            return services;
        }

        public IServiceCollection AddConfiguredSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api Documentation", Version = "v1.0" });
                c.CustomSchemaIds(x => x.FullName);

                OpenApiSecurityScheme bearerSecuritySchema = new OpenApiSecurityScheme
                {
                    Description = $"Using the Authorization header with the" +
                        $" Bearer scheme.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };

                c.AddSecurityDefinition("Bearer", bearerSecuritySchema);

                c.OperationFilter<SwaggerAuthenticationFilter>();

            });

            return services;
        }

        private void AddConfigOptions(IServiceCollection services, IConfiguration config)
        {
            services.Configure<JWTSettings>(config.GetSection("JWTSettings"));

            services.Configure<MailSettings>(config.GetSection("MailSettings"));
        }

        private IServiceCollection AddTransientDependencyGroup(IServiceCollection services)
        {
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IEmailService, EmailService>();

            return services;
        }

        private IServiceCollection AddSingletonDependencyGroup(IServiceCollection services)
        {
            services.AddMemoryCache();

            services.AddAutoMapper(typeof(MappingProfiles));

            services.AddSingleton<ILoggerFactory, LoggerFactory>();
            services.AddSingleton(typeof(ILoggerProvider<>), typeof(LoggerProvider<>));

            services.AddHttpContextAccessor();

            return services;
        }

        private IServiceCollection AddScopedDependencyGroup(IServiceCollection services)
        {
            services.AddScoped(typeof(IGardenHubLogger<>), typeof(GardenHubLogger<>));

            services.AddScoped<PrepareAppService>();
            services.AddScoped<DbMigrator>();

            services.AddScoped<IAccountService, AccountService>();

            services.AddScoped<IUserAccessor, UserAccessor>();

            services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            services.AddScoped<IUserProfileService, UserProfileService>();

            services.AddScoped<IGardenerProfileRepository, GardenerProfileRepository>();
            services.AddScoped<IGardenerProfileService, GardenerProfileService>();

            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IProjectService, ProjectService>();

            services.AddScoped<IMediaRepository, MediaRepository>();
            services.AddScoped<IMediaService, MediaService>();

            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<ICityService, CityService>();

            services.AddScoped<IWorkTypeRepository, WorkTypeRepository>();
            services.AddScoped<IWorkTypeService, WorkTypeService>();

            services.AddScoped<IFeedbackRepository, FeedbackRepository>();
            services.AddScoped<IFeedbackService, FeedbackService>();

            return services;
        }
    }
}

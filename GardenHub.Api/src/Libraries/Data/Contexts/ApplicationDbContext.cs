
using Data.IdentityModels;
using Data.Mapping;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models.DbEntities;
using System;
using System.Linq;
using System.Reflection;

namespace Data.Contexts;

public class ApplicationDbContext : IdentityDbContext<
    ApplicationUser, ApplicationRole, int,
    ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin,
    ApplicationRoleClaim, ApplicationUserToken>,
    IDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        RegisterEntityMapping(builder);
        base.OnModelCreating(builder);

        builder.Entity<ApplicationUser>(entity =>
        {
            entity.ToTable(name: "User");
            entity.HasOne(e => e.UserProfile)
            .WithOne()
            .HasForeignKey<UserProfile>(e => e.IdentityId);
        });

        builder.Entity<ApplicationRole>(entity =>
        {
            entity.ToTable(name: "Role");
        });

        builder.Entity<ApplicationUserRole>(entity =>
        {
            entity.HasKey(ur => new { ur.UserId, ur.RoleId });

            entity.HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            entity.HasOne(ur => ur.User)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
            entity.ToTable("UserRoles");
        });

        builder.Entity<ApplicationUserClaim>(entity =>
        {
            entity.ToTable("UserClaims");
        });

        builder.Entity<ApplicationUserLogin>(entity =>
        {
            entity.ToTable("UserLogins");
        });

        builder.Entity<ApplicationRoleClaim>(entity =>
        {
            entity.ToTable("RoleClaims");

        });

        builder.Entity<ApplicationUserToken>(entity =>
        {
            entity.ToTable("UserTokens");
        });



    }
    public void RegisterEntityMapping(ModelBuilder modelBuilder)
    {
        var typeConfigurations = Assembly.GetExecutingAssembly().GetTypes().Where(type =>
            (type.BaseType?.IsGenericType ?? false) &&
            (type.BaseType.GetGenericTypeDefinition() == typeof(MappingEntityTypeConfiguration<>))
        );
        foreach (var item in typeConfigurations)
        {
            var configuration = (IMappingConfiguration)Activator.CreateInstance(item);
            configuration.ApplyConfiguration(modelBuilder);
        }
    }

    public virtual new DbSet<TEntity> Set<TEntity>() where TEntity : class
    {
        return base.Set<TEntity>();
    }
}

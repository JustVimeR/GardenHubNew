﻿// <auto-generated />
using System;
using Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240305230517_FeedbacksTable_Add_CustomerIdColumn")]
    partial class FeedbacksTable_Add_CustomerIdColumn
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CityGardenerProfile", b =>
                {
                    b.Property<long>("CitiesId")
                        .HasColumnType("bigint");

                    b.Property<long>("GardenersId")
                        .HasColumnType("bigint");

                    b.HasKey("CitiesId", "GardenersId");

                    b.HasIndex("GardenersId");

                    b.ToTable("CityGardenerProfile");
                });

            modelBuilder.Entity("Data.IdentityModels.ApplicationRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("Role", (string)null);
                });

            modelBuilder.Entity("Data.IdentityModels.ApplicationRoleClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims", (string)null);
                });

            modelBuilder.Entity("Data.IdentityModels.ApplicationUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<long?>("UserProfileId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("Data.IdentityModels.ApplicationUserClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaims", (string)null);
                });

            modelBuilder.Entity("Data.IdentityModels.ApplicationUserLogin", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogins", (string)null);
                });

            modelBuilder.Entity("Data.IdentityModels.ApplicationUserRole", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles", (string)null);
                });

            modelBuilder.Entity("Data.IdentityModels.ApplicationUserToken", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens", (string)null);
                });

            modelBuilder.Entity("GardenerProfileProject", b =>
                {
                    b.Property<long>("GardenersId")
                        .HasColumnType("bigint");

                    b.Property<long>("ProjectsId")
                        .HasColumnType("bigint");

                    b.HasKey("GardenersId", "ProjectsId");

                    b.HasIndex("ProjectsId");

                    b.ToTable("GardenerProfileProject");
                });

            modelBuilder.Entity("GardenerProfileWorkType", b =>
                {
                    b.Property<long>("GardenerProfilesId")
                        .HasColumnType("bigint");

                    b.Property<long>("WorkTypesId")
                        .HasColumnType("bigint");

                    b.HasKey("GardenerProfilesId", "WorkTypesId");

                    b.HasIndex("WorkTypesId");

                    b.ToTable("GardenerProfileWorkType");
                });

            modelBuilder.Entity("Models.DbEntities.City", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GetUtcDate()");

                    b.Property<string>("CreatedBy")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(50)")
                        .HasDefaultValue("'Anonymous creation'");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RecordStatus")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GetUtcDate()");

                    b.Property<string>("UpdatedBy")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(50)")
                        .HasDefaultValueSql("'Anonymous creation'");

                    b.HasKey("Id");

                    b.ToTable("Cities", (string)null);
                });

            modelBuilder.Entity("Models.DbEntities.CustomerProfile", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GetUtcDate()");

                    b.Property<string>("CreatedBy")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(50)")
                        .HasDefaultValue("'Anonymous creation'");

                    b.Property<int?>("RecordStatus")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GetUtcDate()");

                    b.Property<string>("UpdatedBy")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(50)")
                        .HasDefaultValueSql("'Anonymous creation'");

                    b.HasKey("Id");

                    b.ToTable("CustomerProfiles", (string)null);
                });

            modelBuilder.Entity("Models.DbEntities.Feedback", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GetUtcDate()");

                    b.Property<string>("CreatedBy")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(50)")
                        .HasDefaultValue("'Anonymous creation'");

                    b.Property<long?>("CustomerId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("EditedDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("GardenerId")
                        .HasColumnType("bigint");

                    b.Property<long?>("ProjectId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("PublicationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<int?>("RecordStatus")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GetUtcDate()");

                    b.Property<string>("UpdatedBy")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(50)")
                        .HasDefaultValueSql("'Anonymous creation'");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("GardenerId");

                    b.HasIndex("ProjectId");

                    b.ToTable("Feedbacks", (string)null);
                });

            modelBuilder.Entity("Models.DbEntities.GardenerProfile", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GetUtcDate()");

                    b.Property<string>("CreatedBy")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(50)")
                        .HasDefaultValue("'Anonymous creation'");

                    b.Property<string>("DescriptionOfExperience")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int?>("RecordStatus")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GetUtcDate()");

                    b.Property<string>("UpdatedBy")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(50)")
                        .HasDefaultValueSql("'Anonymous creation'");

                    b.HasKey("Id");

                    b.ToTable("GardenerProfiles", (string)null);
                });

            modelBuilder.Entity("Models.DbEntities.Media", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GetUtcDate()");

                    b.Property<string>("CreatedBy")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(50)")
                        .HasDefaultValue("'Anonymous creation'");

                    b.Property<int?>("RecordStatus")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("RegularExpression", "^(?i)(Image|Video)$");

                    b.Property<DateTime?>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GetUtcDate()");

                    b.Property<string>("UpdatedBy")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(50)")
                        .HasDefaultValueSql("'Anonymous creation'");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Medias", (string)null);
                });

            modelBuilder.Entity("Models.DbEntities.Note", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GetUtcDate()");

                    b.Property<string>("CreatedBy")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(50)")
                        .HasDefaultValue("'Anonymous creation'");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("OwnerEmail")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("RecordStatus")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GetUtcDate()");

                    b.Property<string>("UpdatedBy")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(50)")
                        .HasDefaultValueSql("'Anonymous creation'");

                    b.HasKey("Id");

                    b.ToTable("Notes", (string)null);
                });

            modelBuilder.Entity("Models.DbEntities.Project", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<decimal>("Budget")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GetUtcDate()");

                    b.Property<string>("CreatedBy")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(50)")
                        .HasDefaultValue("'Anonymous creation'");

                    b.Property<long>("CustomerId")
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<DateTimeOffset>("EndDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsVerified")
                        .HasColumnType("bit");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfRequriedGardeners")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("PublicationDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<int?>("RecordStatus")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("StartDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GetUtcDate()");

                    b.Property<string>("UpdatedBy")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(50)")
                        .HasDefaultValueSql("'Anonymous creation'");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Projects", (string)null);
                });

            modelBuilder.Entity("Models.DbEntities.ProjectMedia", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GetUtcDate()");

                    b.Property<string>("CreatedBy")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(50)")
                        .HasDefaultValue("'Anonymous creation'");

                    b.Property<long>("ProjectId")
                        .HasColumnType("bigint");

                    b.Property<int?>("RecordStatus")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("RegularExpression", "^(?i)(Image|Video)$");

                    b.Property<DateTime?>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GetUtcDate()");

                    b.Property<string>("UpdatedBy")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(50)")
                        .HasDefaultValueSql("'Anonymous creation'");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("ProjectMedias", (string)null);
                });

            modelBuilder.Entity("Models.DbEntities.UserProfile", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateOnly>("BirthDate")
                        .HasColumnType("date");

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GetUtcDate()");

                    b.Property<string>("CreatedBy")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(50)")
                        .HasDefaultValue("'Anonymous creation'");

                    b.Property<long>("CustomerProfileId")
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("GardenerProfileId")
                        .HasColumnType("bigint");

                    b.Property<long?>("IconId")
                        .HasColumnType("bigint");

                    b.Property<int>("IdentityId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RecordStatus")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GetUtcDate()");

                    b.Property<string>("UpdatedBy")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(50)")
                        .HasDefaultValueSql("'Anonymous creation'");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerProfileId");

                    b.HasIndex("GardenerProfileId");

                    b.HasIndex("IconId");

                    b.HasIndex("IdentityId")
                        .IsUnique();

                    b.ToTable("UserProfiles", (string)null);
                });

            modelBuilder.Entity("Models.DbEntities.WorkType", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GetUtcDate()");

                    b.Property<string>("CreatedBy")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(50)")
                        .HasDefaultValue("'Anonymous creation'");

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("ParentWorkTypeId")
                        .HasColumnType("bigint");

                    b.Property<int?>("RecordStatus")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GetUtcDate()");

                    b.Property<string>("UpdatedBy")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(50)")
                        .HasDefaultValueSql("'Anonymous creation'");

                    b.HasKey("Id");

                    b.HasIndex("ParentWorkTypeId");

                    b.ToTable("WorkTypes", (string)null);
                });

            modelBuilder.Entity("ProjectWorkType", b =>
                {
                    b.Property<long>("ProjectsId")
                        .HasColumnType("bigint");

                    b.Property<long>("WorkTypesId")
                        .HasColumnType("bigint");

                    b.HasKey("ProjectsId", "WorkTypesId");

                    b.HasIndex("WorkTypesId");

                    b.ToTable("ProjectWorkType");
                });

            modelBuilder.Entity("CityGardenerProfile", b =>
                {
                    b.HasOne("Models.DbEntities.City", null)
                        .WithMany()
                        .HasForeignKey("CitiesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.DbEntities.GardenerProfile", null)
                        .WithMany()
                        .HasForeignKey("GardenersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Data.IdentityModels.ApplicationRoleClaim", b =>
                {
                    b.HasOne("Data.IdentityModels.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Data.IdentityModels.ApplicationUserClaim", b =>
                {
                    b.HasOne("Data.IdentityModels.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Data.IdentityModels.ApplicationUserLogin", b =>
                {
                    b.HasOne("Data.IdentityModels.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Data.IdentityModels.ApplicationUserRole", b =>
                {
                    b.HasOne("Data.IdentityModels.ApplicationRole", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.IdentityModels.ApplicationUser", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Data.IdentityModels.ApplicationUserToken", b =>
                {
                    b.HasOne("Data.IdentityModels.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GardenerProfileProject", b =>
                {
                    b.HasOne("Models.DbEntities.GardenerProfile", null)
                        .WithMany()
                        .HasForeignKey("GardenersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.DbEntities.Project", null)
                        .WithMany()
                        .HasForeignKey("ProjectsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GardenerProfileWorkType", b =>
                {
                    b.HasOne("Models.DbEntities.GardenerProfile", null)
                        .WithMany()
                        .HasForeignKey("GardenerProfilesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.DbEntities.WorkType", null)
                        .WithMany()
                        .HasForeignKey("WorkTypesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Models.DbEntities.Feedback", b =>
                {
                    b.HasOne("Models.DbEntities.CustomerProfile", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");

                    b.HasOne("Models.DbEntities.GardenerProfile", "Gardener")
                        .WithMany("Feedbacks")
                        .HasForeignKey("GardenerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.DbEntities.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId");

                    b.Navigation("Customer");

                    b.Navigation("Gardener");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("Models.DbEntities.Project", b =>
                {
                    b.HasOne("Models.DbEntities.CustomerProfile", "Customer")
                        .WithMany("Projects")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Models.DbEntities.ProjectMedia", b =>
                {
                    b.HasOne("Models.DbEntities.Project", "Project")
                        .WithMany("Medias")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("Models.DbEntities.UserProfile", b =>
                {
                    b.HasOne("Models.DbEntities.CustomerProfile", "CustomerProfile")
                        .WithMany()
                        .HasForeignKey("CustomerProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.DbEntities.GardenerProfile", "GardenerProfile")
                        .WithMany()
                        .HasForeignKey("GardenerProfileId");

                    b.HasOne("Models.DbEntities.Media", "Icon")
                        .WithMany()
                        .HasForeignKey("IconId");

                    b.HasOne("Data.IdentityModels.ApplicationUser", null)
                        .WithOne("UserProfile")
                        .HasForeignKey("Models.DbEntities.UserProfile", "IdentityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CustomerProfile");

                    b.Navigation("GardenerProfile");

                    b.Navigation("Icon");
                });

            modelBuilder.Entity("Models.DbEntities.WorkType", b =>
                {
                    b.HasOne("Models.DbEntities.WorkType", "ParentWorkType")
                        .WithMany("DerivedWorkTypes")
                        .HasForeignKey("ParentWorkTypeId");

                    b.Navigation("ParentWorkType");
                });

            modelBuilder.Entity("ProjectWorkType", b =>
                {
                    b.HasOne("Models.DbEntities.Project", null)
                        .WithMany()
                        .HasForeignKey("ProjectsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.DbEntities.WorkType", null)
                        .WithMany()
                        .HasForeignKey("WorkTypesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Data.IdentityModels.ApplicationRole", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Data.IdentityModels.ApplicationUser", b =>
                {
                    b.Navigation("UserProfile");

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Models.DbEntities.CustomerProfile", b =>
                {
                    b.Navigation("Projects");
                });

            modelBuilder.Entity("Models.DbEntities.GardenerProfile", b =>
                {
                    b.Navigation("Feedbacks");
                });

            modelBuilder.Entity("Models.DbEntities.Project", b =>
                {
                    b.Navigation("Medias");
                });

            modelBuilder.Entity("Models.DbEntities.WorkType", b =>
                {
                    b.Navigation("DerivedWorkTypes");
                });
#pragma warning restore 612, 618
        }
    }
}

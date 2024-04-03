using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class Merge_Profile_Tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_CustomerProfiles_CustomerId",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_GardenerProfiles_GardenerId",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_CustomerProfiles_CustomerId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProfiles_CustomerProfiles_CustomerProfileId",
                table: "UserProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProfiles_GardenerProfiles_GardenerProfileId",
                table: "UserProfiles");

            migrationBuilder.DropTable(
                name: "CityGardenerProfile");

            migrationBuilder.DropTable(
                name: "CustomerProfiles");

            migrationBuilder.DropTable(
                name: "GardenerProfileProject");

            migrationBuilder.DropTable(
                name: "GardenerProfileWorkType");

            migrationBuilder.DropTable(
                name: "GardenerProfiles");

            migrationBuilder.DropIndex(
                name: "IX_UserProfiles_CustomerProfileId",
                table: "UserProfiles");

            migrationBuilder.DropIndex(
                name: "IX_UserProfiles_GardenerProfileId",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "CustomerProfileId",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "GardenerProfileId",
                table: "UserProfiles");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "WorkTypes",
                type: "nvarchar(50)",
                nullable: true,
                defaultValue: "'Anonymous creation'",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true,
                oldDefaultValueSql: "'Anonymous creation'");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "UserProfiles",
                type: "nvarchar(50)",
                nullable: true,
                defaultValue: "'Anonymous creation'",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true,
                oldDefaultValueSql: "'Anonymous creation'");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionOfExperience",
                table: "UserProfiles",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsGardener",
                table: "UserProfiles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "Projects",
                type: "nvarchar(50)",
                nullable: true,
                defaultValue: "'Anonymous creation'",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true,
                oldDefaultValueSql: "'Anonymous creation'");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "ProjectMedias",
                type: "nvarchar(50)",
                nullable: true,
                defaultValue: "'Anonymous creation'",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true,
                oldDefaultValueSql: "'Anonymous creation'");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "Notes",
                type: "nvarchar(50)",
                nullable: true,
                defaultValue: "'Anonymous creation'",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true,
                oldDefaultValueSql: "'Anonymous creation'");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "Medias",
                type: "nvarchar(50)",
                nullable: true,
                defaultValue: "'Anonymous creation'",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true,
                oldDefaultValueSql: "'Anonymous creation'");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "Feedbacks",
                type: "nvarchar(50)",
                nullable: true,
                defaultValue: "'Anonymous creation'",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true,
                oldDefaultValueSql: "'Anonymous creation'");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "Cities",
                type: "nvarchar(50)",
                nullable: true,
                defaultValue: "'Anonymous creation'",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true,
                oldDefaultValueSql: "'Anonymous creation'");

            migrationBuilder.CreateTable(
                name: "CityUserProfile",
                columns: table => new
                {
                    CitiesId = table.Column<long>(type: "bigint", nullable: false),
                    GardenersId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityUserProfile", x => new { x.CitiesId, x.GardenersId });
                    table.ForeignKey(
                        name: "FK_CityUserProfile_Cities_CitiesId",
                        column: x => x.CitiesId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CityUserProfile_UserProfiles_GardenersId",
                        column: x => x.GardenersId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectUserProfile",
                columns: table => new
                {
                    GardenerProjectsId = table.Column<long>(type: "bigint", nullable: false),
                    GardenersId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectUserProfile", x => new { x.GardenerProjectsId, x.GardenersId });
                    table.ForeignKey(
                        name: "FK_ProjectUserProfile_Projects_GardenerProjectsId",
                        column: x => x.GardenerProjectsId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectUserProfile_UserProfiles_GardenersId",
                        column: x => x.GardenersId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserProfileWorkType",
                columns: table => new
                {
                    GardenersId = table.Column<long>(type: "bigint", nullable: false),
                    WorkTypesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfileWorkType", x => new { x.GardenersId, x.WorkTypesId });
                    table.ForeignKey(
                        name: "FK_UserProfileWorkType_UserProfiles_GardenersId",
                        column: x => x.GardenersId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserProfileWorkType_WorkTypes_WorkTypesId",
                        column: x => x.WorkTypesId,
                        principalTable: "WorkTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CityUserProfile_GardenersId",
                table: "CityUserProfile",
                column: "GardenersId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectUserProfile_GardenersId",
                table: "ProjectUserProfile",
                column: "GardenersId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfileWorkType_WorkTypesId",
                table: "UserProfileWorkType",
                column: "WorkTypesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_UserProfiles_CustomerId",
                table: "Feedbacks",
                column: "CustomerId",
                principalTable: "UserProfiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_UserProfiles_GardenerId",
                table: "Feedbacks",
                column: "GardenerId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_UserProfiles_CustomerId",
                table: "Projects",
                column: "CustomerId",
                principalTable: "UserProfiles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_UserProfiles_CustomerId",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_UserProfiles_GardenerId",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_UserProfiles_CustomerId",
                table: "Projects");

            migrationBuilder.DropTable(
                name: "CityUserProfile");

            migrationBuilder.DropTable(
                name: "ProjectUserProfile");

            migrationBuilder.DropTable(
                name: "UserProfileWorkType");

            migrationBuilder.DropColumn(
                name: "DescriptionOfExperience",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "IsGardener",
                table: "UserProfiles");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "WorkTypes",
                type: "nvarchar(50)",
                nullable: true,
                defaultValueSql: "'Anonymous creation'",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true,
                oldDefaultValue: "'Anonymous creation'");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "UserProfiles",
                type: "nvarchar(50)",
                nullable: true,
                defaultValueSql: "'Anonymous creation'",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true,
                oldDefaultValue: "'Anonymous creation'");

            migrationBuilder.AddColumn<long>(
                name: "CustomerProfileId",
                table: "UserProfiles",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "GardenerProfileId",
                table: "UserProfiles",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "Projects",
                type: "nvarchar(50)",
                nullable: true,
                defaultValueSql: "'Anonymous creation'",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true,
                oldDefaultValue: "'Anonymous creation'");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "ProjectMedias",
                type: "nvarchar(50)",
                nullable: true,
                defaultValueSql: "'Anonymous creation'",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true,
                oldDefaultValue: "'Anonymous creation'");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "Notes",
                type: "nvarchar(50)",
                nullable: true,
                defaultValueSql: "'Anonymous creation'",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true,
                oldDefaultValue: "'Anonymous creation'");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "Medias",
                type: "nvarchar(50)",
                nullable: true,
                defaultValueSql: "'Anonymous creation'",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true,
                oldDefaultValue: "'Anonymous creation'");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "Feedbacks",
                type: "nvarchar(50)",
                nullable: true,
                defaultValueSql: "'Anonymous creation'",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true,
                oldDefaultValue: "'Anonymous creation'");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "Cities",
                type: "nvarchar(50)",
                nullable: true,
                defaultValueSql: "'Anonymous creation'",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true,
                oldDefaultValue: "'Anonymous creation'");

            migrationBuilder.CreateTable(
                name: "CustomerProfiles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GetUtcDate()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", nullable: true, defaultValue: "'Anonymous creation'"),
                    RecordStatus = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GetUtcDate()"),
                    UpdatedBy = table.Column<string>(type: "nvarchar(50)", nullable: true, defaultValueSql: "'Anonymous creation'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerProfiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GardenerProfiles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GetUtcDate()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", nullable: true, defaultValue: "'Anonymous creation'"),
                    DescriptionOfExperience = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    RecordStatus = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GetUtcDate()"),
                    UpdatedBy = table.Column<string>(type: "nvarchar(50)", nullable: true, defaultValueSql: "'Anonymous creation'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GardenerProfiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CityGardenerProfile",
                columns: table => new
                {
                    CitiesId = table.Column<long>(type: "bigint", nullable: false),
                    GardenersId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityGardenerProfile", x => new { x.CitiesId, x.GardenersId });
                    table.ForeignKey(
                        name: "FK_CityGardenerProfile_Cities_CitiesId",
                        column: x => x.CitiesId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CityGardenerProfile_GardenerProfiles_GardenersId",
                        column: x => x.GardenersId,
                        principalTable: "GardenerProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GardenerProfileProject",
                columns: table => new
                {
                    GardenersId = table.Column<long>(type: "bigint", nullable: false),
                    ProjectsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GardenerProfileProject", x => new { x.GardenersId, x.ProjectsId });
                    table.ForeignKey(
                        name: "FK_GardenerProfileProject_GardenerProfiles_GardenersId",
                        column: x => x.GardenersId,
                        principalTable: "GardenerProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GardenerProfileProject_Projects_ProjectsId",
                        column: x => x.ProjectsId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GardenerProfileWorkType",
                columns: table => new
                {
                    GardenerProfilesId = table.Column<long>(type: "bigint", nullable: false),
                    WorkTypesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GardenerProfileWorkType", x => new { x.GardenerProfilesId, x.WorkTypesId });
                    table.ForeignKey(
                        name: "FK_GardenerProfileWorkType_GardenerProfiles_GardenerProfilesId",
                        column: x => x.GardenerProfilesId,
                        principalTable: "GardenerProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GardenerProfileWorkType_WorkTypes_WorkTypesId",
                        column: x => x.WorkTypesId,
                        principalTable: "WorkTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_CustomerProfileId",
                table: "UserProfiles",
                column: "CustomerProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_GardenerProfileId",
                table: "UserProfiles",
                column: "GardenerProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_CityGardenerProfile_GardenersId",
                table: "CityGardenerProfile",
                column: "GardenersId");

            migrationBuilder.CreateIndex(
                name: "IX_GardenerProfileProject_ProjectsId",
                table: "GardenerProfileProject",
                column: "ProjectsId");

            migrationBuilder.CreateIndex(
                name: "IX_GardenerProfileWorkType_WorkTypesId",
                table: "GardenerProfileWorkType",
                column: "WorkTypesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_CustomerProfiles_CustomerId",
                table: "Feedbacks",
                column: "CustomerId",
                principalTable: "CustomerProfiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_GardenerProfiles_GardenerId",
                table: "Feedbacks",
                column: "GardenerId",
                principalTable: "GardenerProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_CustomerProfiles_CustomerId",
                table: "Projects",
                column: "CustomerId",
                principalTable: "CustomerProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfiles_CustomerProfiles_CustomerProfileId",
                table: "UserProfiles",
                column: "CustomerProfileId",
                principalTable: "CustomerProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfiles_GardenerProfiles_GardenerProfileId",
                table: "UserProfiles",
                column: "GardenerProfileId",
                principalTable: "GardenerProfiles",
                principalColumn: "Id");
        }
    }
}

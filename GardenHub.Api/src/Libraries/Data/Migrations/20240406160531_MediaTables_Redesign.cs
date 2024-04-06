using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class MediaTables_Redesign : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Projects_ProjectId",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_UserProfiles_CustomerId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectUserProfile_Projects_GardenerProjectsId",
                table: "ProjectUserProfile");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectWorkType_Projects_ProjectsId",
                table: "ProjectWorkType");

            migrationBuilder.DropTable(
                name: "ProjectMedias");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Projects",
                table: "Projects");

            migrationBuilder.RenameTable(
                name: "Projects",
                newName: "Project");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_CustomerId",
                table: "Project",
                newName: "IX_Project_CustomerId");

            migrationBuilder.AddColumn<long>(
                name: "ProjectId",
                table: "Medias",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "Project",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true,
                oldDefaultValue: "Anonymous creation");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Project",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValueSql: "GetUtcDate()");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Project",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Project",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true,
                oldDefaultValue: "Anonymous creation");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Project",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValueSql: "GetUtcDate()");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Project",
                table: "Project",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Medias_ProjectId",
                table: "Medias",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Project_ProjectId",
                table: "Feedbacks",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Medias_Project_ProjectId",
                table: "Medias",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_UserProfiles_CustomerId",
                table: "Project",
                column: "CustomerId",
                principalTable: "UserProfiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectUserProfile_Project_GardenerProjectsId",
                table: "ProjectUserProfile",
                column: "GardenerProjectsId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectWorkType_Project_ProjectsId",
                table: "ProjectWorkType",
                column: "ProjectsId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Project_ProjectId",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Medias_Project_ProjectId",
                table: "Medias");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_UserProfiles_CustomerId",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectUserProfile_Project_GardenerProjectsId",
                table: "ProjectUserProfile");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectWorkType_Project_ProjectsId",
                table: "ProjectWorkType");

            migrationBuilder.DropIndex(
                name: "IX_Medias_ProjectId",
                table: "Medias");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Project",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Medias");

            migrationBuilder.RenameTable(
                name: "Project",
                newName: "Projects");

            migrationBuilder.RenameIndex(
                name: "IX_Project_CustomerId",
                table: "Projects",
                newName: "IX_Projects_CustomerId");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "Projects",
                type: "nvarchar(50)",
                nullable: true,
                defaultValue: "Anonymous creation",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Projects",
                type: "datetime2",
                nullable: true,
                defaultValueSql: "GetUtcDate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Projects",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Projects",
                type: "nvarchar(50)",
                nullable: true,
                defaultValue: "Anonymous creation",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Projects",
                type: "datetime2",
                nullable: true,
                defaultValueSql: "GetUtcDate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Projects",
                table: "Projects",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ProjectMedias",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GetUtcDate()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", nullable: true, defaultValue: "Anonymous creation"),
                    RecordStatus = table.Column<int>(type: "int", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GetUtcDate()"),
                    UpdatedBy = table.Column<string>(type: "nvarchar(50)", nullable: true, defaultValue: "Anonymous creation"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectMedias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectMedias_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMedias_ProjectId",
                table: "ProjectMedias",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Projects_ProjectId",
                table: "Feedbacks",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_UserProfiles_CustomerId",
                table: "Projects",
                column: "CustomerId",
                principalTable: "UserProfiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectUserProfile_Projects_GardenerProjectsId",
                table: "ProjectUserProfile",
                column: "GardenerProjectsId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectWorkType_Projects_ProjectsId",
                table: "ProjectWorkType",
                column: "ProjectsId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

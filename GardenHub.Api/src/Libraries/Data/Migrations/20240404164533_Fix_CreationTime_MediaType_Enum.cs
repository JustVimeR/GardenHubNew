using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class Fix_CreationTime_MediaType_Enum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "WorkTypes",
                type: "nvarchar(50)",
                nullable: true,
                defaultValue: "Anonymous creation",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true,
                oldDefaultValue: "'Anonymous creation'");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "WorkTypes",
                type: "nvarchar(50)",
                nullable: true,
                defaultValue: "Anonymous creation",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true,
                oldDefaultValue: "'Anonymous creation'");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "UserProfiles",
                type: "nvarchar(50)",
                nullable: true,
                defaultValue: "Anonymous creation",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true,
                oldDefaultValue: "'Anonymous creation'");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "UserProfiles",
                type: "nvarchar(50)",
                nullable: true,
                defaultValue: "Anonymous creation",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true,
                oldDefaultValue: "'Anonymous creation'");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "Projects",
                type: "nvarchar(50)",
                nullable: true,
                defaultValue: "Anonymous creation",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true,
                oldDefaultValue: "'Anonymous creation'");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Projects",
                type: "nvarchar(50)",
                nullable: true,
                defaultValue: "Anonymous creation",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true,
                oldDefaultValue: "'Anonymous creation'");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "ProjectMedias",
                type: "nvarchar(50)",
                nullable: true,
                defaultValue: "Anonymous creation",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true,
                oldDefaultValue: "'Anonymous creation'");

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "ProjectMedias",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "ProjectMedias",
                type: "nvarchar(50)",
                nullable: true,
                defaultValue: "Anonymous creation",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true,
                oldDefaultValue: "'Anonymous creation'");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "Medias",
                type: "nvarchar(50)",
                nullable: true,
                defaultValue: "Anonymous creation",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true,
                oldDefaultValue: "'Anonymous creation'");

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "Medias",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Medias",
                type: "nvarchar(50)",
                nullable: true,
                defaultValue: "Anonymous creation",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true,
                oldDefaultValue: "'Anonymous creation'");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "Feedbacks",
                type: "nvarchar(50)",
                nullable: true,
                defaultValue: "Anonymous creation",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true,
                oldDefaultValue: "'Anonymous creation'");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Feedbacks",
                type: "nvarchar(50)",
                nullable: true,
                defaultValue: "Anonymous creation",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true,
                oldDefaultValue: "'Anonymous creation'");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "Cities",
                type: "nvarchar(50)",
                nullable: true,
                defaultValue: "Anonymous creation",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true,
                oldDefaultValue: "'Anonymous creation'");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Cities",
                type: "nvarchar(50)",
                nullable: true,
                defaultValue: "Anonymous creation",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true,
                oldDefaultValue: "'Anonymous creation'");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "Chats",
                type: "nvarchar(50)",
                nullable: true,
                defaultValue: "Anonymous creation",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true,
                oldDefaultValue: "'Anonymous creation'");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Chats",
                type: "nvarchar(50)",
                nullable: true,
                defaultValue: "Anonymous creation",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true,
                oldDefaultValue: "'Anonymous creation'");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "ChatMessage",
                type: "nvarchar(50)",
                nullable: true,
                defaultValue: "Anonymous creation",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true,
                oldDefaultValue: "'Anonymous creation'");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "ChatMessage",
                type: "nvarchar(50)",
                nullable: true,
                defaultValue: "Anonymous creation",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true,
                oldDefaultValue: "'Anonymous creation'");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "WorkTypes",
                type: "nvarchar(50)",
                nullable: true,
                defaultValue: "'Anonymous creation'",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true,
                oldDefaultValue: "Anonymous creation");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "WorkTypes",
                type: "nvarchar(50)",
                nullable: true,
                defaultValue: "'Anonymous creation'",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true,
                oldDefaultValue: "Anonymous creation");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "UserProfiles",
                type: "nvarchar(50)",
                nullable: true,
                defaultValue: "'Anonymous creation'",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true,
                oldDefaultValue: "Anonymous creation");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "UserProfiles",
                type: "nvarchar(50)",
                nullable: true,
                defaultValue: "'Anonymous creation'",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true,
                oldDefaultValue: "Anonymous creation");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "Projects",
                type: "nvarchar(50)",
                nullable: true,
                defaultValue: "'Anonymous creation'",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true,
                oldDefaultValue: "Anonymous creation");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Projects",
                type: "nvarchar(50)",
                nullable: true,
                defaultValue: "'Anonymous creation'",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true,
                oldDefaultValue: "Anonymous creation");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "ProjectMedias",
                type: "nvarchar(50)",
                nullable: true,
                defaultValue: "'Anonymous creation'",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true,
                oldDefaultValue: "Anonymous creation");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "ProjectMedias",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "ProjectMedias",
                type: "nvarchar(50)",
                nullable: true,
                defaultValue: "'Anonymous creation'",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true,
                oldDefaultValue: "Anonymous creation");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "Medias",
                type: "nvarchar(50)",
                nullable: true,
                defaultValue: "'Anonymous creation'",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true,
                oldDefaultValue: "Anonymous creation");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Medias",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Medias",
                type: "nvarchar(50)",
                nullable: true,
                defaultValue: "'Anonymous creation'",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true,
                oldDefaultValue: "Anonymous creation");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "Feedbacks",
                type: "nvarchar(50)",
                nullable: true,
                defaultValue: "'Anonymous creation'",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true,
                oldDefaultValue: "Anonymous creation");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Feedbacks",
                type: "nvarchar(50)",
                nullable: true,
                defaultValue: "'Anonymous creation'",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true,
                oldDefaultValue: "Anonymous creation");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "Cities",
                type: "nvarchar(50)",
                nullable: true,
                defaultValue: "'Anonymous creation'",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true,
                oldDefaultValue: "Anonymous creation");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Cities",
                type: "nvarchar(50)",
                nullable: true,
                defaultValue: "'Anonymous creation'",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true,
                oldDefaultValue: "Anonymous creation");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "Chats",
                type: "nvarchar(50)",
                nullable: true,
                defaultValue: "'Anonymous creation'",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true,
                oldDefaultValue: "Anonymous creation");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Chats",
                type: "nvarchar(50)",
                nullable: true,
                defaultValue: "'Anonymous creation'",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true,
                oldDefaultValue: "Anonymous creation");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "ChatMessage",
                type: "nvarchar(50)",
                nullable: true,
                defaultValue: "'Anonymous creation'",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true,
                oldDefaultValue: "Anonymous creation");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "ChatMessage",
                type: "nvarchar(50)",
                nullable: true,
                defaultValue: "'Anonymous creation'",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true,
                oldDefaultValue: "Anonymous creation");
        }
    }
}

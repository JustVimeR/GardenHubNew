using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class UserProfileTable_Add_FK_NotificationChatId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_UserProfiles_ChannelOwnerId",
                table: "Chats");

            migrationBuilder.DropIndex(
                name: "IX_Chats_ChannelOwnerId",
                table: "Chats");

            migrationBuilder.AlterColumn<long>(
                name: "NotificationChatId",
                table: "UserProfiles",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_NotificationChatId",
                table: "UserProfiles",
                column: "NotificationChatId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfiles_Chats_NotificationChatId",
                table: "UserProfiles",
                column: "NotificationChatId",
                principalTable: "Chats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfiles_Chats_NotificationChatId",
                table: "UserProfiles");

            migrationBuilder.DropIndex(
                name: "IX_UserProfiles_NotificationChatId",
                table: "UserProfiles");

            migrationBuilder.AlterColumn<long>(
                name: "NotificationChatId",
                table: "UserProfiles",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_ChannelOwnerId",
                table: "Chats",
                column: "ChannelOwnerId",
                unique: true,
                filter: "[ChannelOwnerId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_UserProfiles_ChannelOwnerId",
                table: "Chats",
                column: "ChannelOwnerId",
                principalTable: "UserProfiles",
                principalColumn: "Id");
        }
    }
}

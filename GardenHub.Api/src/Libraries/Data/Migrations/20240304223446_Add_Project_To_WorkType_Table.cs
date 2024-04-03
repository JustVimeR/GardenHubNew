using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_Project_To_WorkType_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkTypes_Projects_ProjectId",
                table: "WorkTypes");

            migrationBuilder.DropIndex(
                name: "IX_WorkTypes_ProjectId",
                table: "WorkTypes");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "WorkTypes");

            migrationBuilder.CreateTable(
                name: "ProjectWorkType",
                columns: table => new
                {
                    ProjectsId = table.Column<long>(type: "bigint", nullable: false),
                    WorkTypesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectWorkType", x => new { x.ProjectsId, x.WorkTypesId });
                    table.ForeignKey(
                        name: "FK_ProjectWorkType_Projects_ProjectsId",
                        column: x => x.ProjectsId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectWorkType_WorkTypes_WorkTypesId",
                        column: x => x.WorkTypesId,
                        principalTable: "WorkTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectWorkType_WorkTypesId",
                table: "ProjectWorkType",
                column: "WorkTypesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectWorkType");

            migrationBuilder.AddColumn<long>(
                name: "ProjectId",
                table: "WorkTypes",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkTypes_ProjectId",
                table: "WorkTypes",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkTypes_Projects_ProjectId",
                table: "WorkTypes",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");
        }
    }
}

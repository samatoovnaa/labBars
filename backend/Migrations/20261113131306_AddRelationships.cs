using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_incidents_assignee_user_id",
                table: "incidents",
                column: "assignee_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_incidents_category_id",
                table: "incidents",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_incidents_created_by_user_id",
                table: "incidents",
                column: "created_by_user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_incidents_Categories_category_id",
                table: "incidents",
                column: "category_id",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_incidents_Employees_assignee_user_id",
                table: "incidents",
                column: "assignee_user_id",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_incidents_Employees_created_by_user_id",
                table: "incidents",
                column: "created_by_user_id",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_incidents_Categories_category_id",
                table: "incidents");

            migrationBuilder.DropForeignKey(
                name: "FK_incidents_Employees_assignee_user_id",
                table: "incidents");

            migrationBuilder.DropForeignKey(
                name: "FK_incidents_Employees_created_by_user_id",
                table: "incidents");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_incidents_assignee_user_id",
                table: "incidents");

            migrationBuilder.DropIndex(
                name: "IX_incidents_category_id",
                table: "incidents");

            migrationBuilder.DropIndex(
                name: "IX_incidents_created_by_user_id",
                table: "incidents");
        }
    }
}

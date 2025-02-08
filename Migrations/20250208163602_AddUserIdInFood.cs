using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyFood.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdInFood : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "user_id",
                table: "food",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_food_user_id",
                table: "food",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_food_user_user_id",
                table: "food",
                column: "user_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_food_user_user_id",
                table: "food");

            migrationBuilder.DropIndex(
                name: "IX_food_user_id",
                table: "food");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "food");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyFood.Migrations
{
    /// <inheritdoc />
    public partial class RenameFoodTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_meal_food_Food_food_id",
                table: "meal_food");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Food",
                table: "Food");

            migrationBuilder.RenameTable(
                name: "Food",
                newName: "food");

            migrationBuilder.AddPrimaryKey(
                name: "PK_food",
                table: "food",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_meal_food_food_food_id",
                table: "meal_food",
                column: "food_id",
                principalTable: "food",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_meal_food_food_food_id",
                table: "meal_food");

            migrationBuilder.DropPrimaryKey(
                name: "PK_food",
                table: "food");

            migrationBuilder.RenameTable(
                name: "food",
                newName: "Food");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Food",
                table: "Food",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_meal_food_Food_food_id",
                table: "meal_food",
                column: "food_id",
                principalTable: "Food",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

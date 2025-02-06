using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyFood.Migrations
{
    /// <inheritdoc />
    public partial class RenameMealItemToMealFood : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MealItems_Foods_FoodId",
                table: "MealItems");

            migrationBuilder.DropForeignKey(
                name: "FK_MealItems_Meals_MealId",
                table: "MealItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MealItems",
                table: "MealItems");

            migrationBuilder.RenameTable(
                name: "MealItems",
                newName: "MealFoods");

            migrationBuilder.RenameIndex(
                name: "IX_MealItems_MealId",
                table: "MealFoods",
                newName: "IX_MealFoods_MealId");

            migrationBuilder.RenameIndex(
                name: "IX_MealItems_FoodId",
                table: "MealFoods",
                newName: "IX_MealFoods_FoodId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MealFoods",
                table: "MealFoods",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MealFoods_Foods_FoodId",
                table: "MealFoods",
                column: "FoodId",
                principalTable: "Foods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MealFoods_Meals_MealId",
                table: "MealFoods",
                column: "MealId",
                principalTable: "Meals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MealFoods_Foods_FoodId",
                table: "MealFoods");

            migrationBuilder.DropForeignKey(
                name: "FK_MealFoods_Meals_MealId",
                table: "MealFoods");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MealFoods",
                table: "MealFoods");

            migrationBuilder.RenameTable(
                name: "MealFoods",
                newName: "MealItems");

            migrationBuilder.RenameIndex(
                name: "IX_MealFoods_MealId",
                table: "MealItems",
                newName: "IX_MealItems_MealId");

            migrationBuilder.RenameIndex(
                name: "IX_MealFoods_FoodId",
                table: "MealItems",
                newName: "IX_MealItems_FoodId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MealItems",
                table: "MealItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MealItems_Foods_FoodId",
                table: "MealItems",
                column: "FoodId",
                principalTable: "Foods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MealItems_Meals_MealId",
                table: "MealItems",
                column: "MealId",
                principalTable: "Meals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

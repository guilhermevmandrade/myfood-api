using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyFood.Migrations
{
    /// <inheritdoc />
    public partial class RenameTablesAndColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MealFoods_Foods_FoodId",
                table: "MealFoods");

            migrationBuilder.DropForeignKey(
                name: "FK_MealFoods_Meals_MealId",
                table: "MealFoods");

            migrationBuilder.DropForeignKey(
                name: "FK_Meals_Users_UserId",
                table: "Meals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Meals",
                table: "Meals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MealFoods",
                table: "MealFoods");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Foods",
                table: "Foods");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "users");

            migrationBuilder.RenameTable(
                name: "Meals",
                newName: "meal");

            migrationBuilder.RenameTable(
                name: "MealFoods",
                newName: "meal_food");

            migrationBuilder.RenameTable(
                name: "Foods",
                newName: "Food");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "users",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "users",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "users",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "users",
                newName: "password_hash");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "users",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "meal",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "meal",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "meal",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "MealTime",
                table: "meal",
                newName: "meal_time");

            migrationBuilder.RenameIndex(
                name: "IX_Meals_UserId",
                table: "meal",
                newName: "IX_meal_user_id");

            migrationBuilder.RenameColumn(
                name: "Unit",
                table: "meal_food",
                newName: "unit");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "meal_food",
                newName: "quantity");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "meal_food",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "MealId",
                table: "meal_food",
                newName: "meal_id");

            migrationBuilder.RenameColumn(
                name: "FoodId",
                table: "meal_food",
                newName: "food_id");

            migrationBuilder.RenameIndex(
                name: "IX_MealFoods_MealId",
                table: "meal_food",
                newName: "IX_meal_food_meal_id");

            migrationBuilder.RenameIndex(
                name: "IX_MealFoods_FoodId",
                table: "meal_food",
                newName: "IX_meal_food_food_id");

            migrationBuilder.RenameColumn(
                name: "Protein",
                table: "Food",
                newName: "protein");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Food",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Fats",
                table: "Food",
                newName: "fats");

            migrationBuilder.RenameColumn(
                name: "Carbs",
                table: "Food",
                newName: "carbs");

            migrationBuilder.RenameColumn(
                name: "Calories",
                table: "Food",
                newName: "calories");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Food",
                newName: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                table: "users",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_meal",
                table: "meal",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_meal_food",
                table: "meal_food",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Food",
                table: "Food",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_meal_users_user_id",
                table: "meal",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_meal_food_Food_food_id",
                table: "meal_food",
                column: "food_id",
                principalTable: "Food",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_meal_food_meal_meal_id",
                table: "meal_food",
                column: "meal_id",
                principalTable: "meal",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_meal_users_user_id",
                table: "meal");

            migrationBuilder.DropForeignKey(
                name: "FK_meal_food_Food_food_id",
                table: "meal_food");

            migrationBuilder.DropForeignKey(
                name: "FK_meal_food_meal_meal_id",
                table: "meal_food");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_meal_food",
                table: "meal_food");

            migrationBuilder.DropPrimaryKey(
                name: "PK_meal",
                table: "meal");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Food",
                table: "Food");

            migrationBuilder.RenameTable(
                name: "users",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "meal_food",
                newName: "MealFoods");

            migrationBuilder.RenameTable(
                name: "meal",
                newName: "Meals");

            migrationBuilder.RenameTable(
                name: "Food",
                newName: "Foods");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Users",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Users",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "password_hash",
                table: "Users",
                newName: "PasswordHash");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Users",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "unit",
                table: "MealFoods",
                newName: "Unit");

            migrationBuilder.RenameColumn(
                name: "quantity",
                table: "MealFoods",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "MealFoods",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "meal_id",
                table: "MealFoods",
                newName: "MealId");

            migrationBuilder.RenameColumn(
                name: "food_id",
                table: "MealFoods",
                newName: "FoodId");

            migrationBuilder.RenameIndex(
                name: "IX_meal_food_meal_id",
                table: "MealFoods",
                newName: "IX_MealFoods_MealId");

            migrationBuilder.RenameIndex(
                name: "IX_meal_food_food_id",
                table: "MealFoods",
                newName: "IX_MealFoods_FoodId");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Meals",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Meals",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "Meals",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "meal_time",
                table: "Meals",
                newName: "MealTime");

            migrationBuilder.RenameIndex(
                name: "IX_meal_user_id",
                table: "Meals",
                newName: "IX_Meals_UserId");

            migrationBuilder.RenameColumn(
                name: "protein",
                table: "Foods",
                newName: "Protein");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Foods",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "fats",
                table: "Foods",
                newName: "Fats");

            migrationBuilder.RenameColumn(
                name: "carbs",
                table: "Foods",
                newName: "Carbs");

            migrationBuilder.RenameColumn(
                name: "calories",
                table: "Foods",
                newName: "Calories");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Foods",
                newName: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MealFoods",
                table: "MealFoods",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Meals",
                table: "Meals",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Foods",
                table: "Foods",
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

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_Users_UserId",
                table: "Meals",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

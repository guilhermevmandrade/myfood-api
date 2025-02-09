using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyFood.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFatsGoalColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "fat_percentage",
                table: "nutritional_goal",
                newName: "fats_percentage");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "fats_percentage",
                table: "nutritional_goal",
                newName: "fat_percentage");
        }
    }
}

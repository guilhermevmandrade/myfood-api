using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyFood.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProteinGoalColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "protein_percentage",
                table: "nutritional_goal",
                newName: "proteins_percentage");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "proteins_percentage",
                table: "nutritional_goal",
                newName: "protein_percentage");
        }
    }
}

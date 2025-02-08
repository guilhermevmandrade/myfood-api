using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MyFood.Migrations
{
    /// <inheritdoc />
    public partial class CreateNutritionalGoalTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "activity_level",
                table: "user",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "height",
                table: "user",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "weight",
                table: "user",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "nutritional_goal",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    daily_calories = table.Column<int>(type: "integer", nullable: false),
                    protein_percentage = table.Column<double>(type: "double precision", nullable: false),
                    carbs_percentage = table.Column<double>(type: "double precision", nullable: false),
                    fat_percentage = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nutritional_goal", x => x.id);
                    table.ForeignKey(
                        name: "FK_nutritional_goal_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_nutritional_goal_user_id",
                table: "nutritional_goal",
                column: "user_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "nutritional_goal");

            migrationBuilder.DropColumn(
                name: "activity_level",
                table: "user");

            migrationBuilder.DropColumn(
                name: "height",
                table: "user");

            migrationBuilder.DropColumn(
                name: "weight",
                table: "user");
        }
    }
}

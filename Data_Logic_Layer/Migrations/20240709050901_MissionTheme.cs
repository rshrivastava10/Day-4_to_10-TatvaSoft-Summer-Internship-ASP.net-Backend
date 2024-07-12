using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Logic_Layer.Migrations
{
    /// <inheritdoc />
    public partial class MissionTheme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ThemeDescription",
                table: "Themes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThemeImage",
                table: "Themes",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThemeDescription",
                table: "Themes");

            migrationBuilder.DropColumn(
                name: "ThemeImage",
                table: "Themes");
        }
    }
}

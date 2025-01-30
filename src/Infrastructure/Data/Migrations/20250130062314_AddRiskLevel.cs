using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RIMS.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRiskLevel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "Risks",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Level",
                table: "Risks");
        }
    }
}

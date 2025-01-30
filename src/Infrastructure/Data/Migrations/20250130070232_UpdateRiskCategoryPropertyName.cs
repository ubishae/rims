using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RIMS.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRiskCategoryPropertyName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Risks_RiskCategories_RiskCategoryId",
                table: "Risks");

            migrationBuilder.RenameColumn(
                name: "RiskCategoryId",
                table: "Risks",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Risks_RiskCategoryId",
                table: "Risks",
                newName: "IX_Risks_CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Risks_RiskCategories_CategoryId",
                table: "Risks",
                column: "CategoryId",
                principalTable: "RiskCategories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Risks_RiskCategories_CategoryId",
                table: "Risks");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Risks",
                newName: "RiskCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Risks_CategoryId",
                table: "Risks",
                newName: "IX_Risks_RiskCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Risks_RiskCategories_RiskCategoryId",
                table: "Risks",
                column: "RiskCategoryId",
                principalTable: "RiskCategories",
                principalColumn: "Id");
        }
    }
}

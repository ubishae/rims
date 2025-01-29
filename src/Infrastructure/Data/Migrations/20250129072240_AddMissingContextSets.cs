using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RIMS.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddMissingContextSets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Risk_RiskCategories_RiskCategoryId",
                table: "Risk");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Risk_RiskId",
                table: "Tickets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Risk",
                table: "Risk");

            migrationBuilder.RenameTable(
                name: "Risk",
                newName: "Risks");

            migrationBuilder.RenameIndex(
                name: "IX_Risk_RiskCategoryId",
                table: "Risks",
                newName: "IX_Risks_RiskCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Risks",
                table: "Risks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Risks_RiskCategories_RiskCategoryId",
                table: "Risks",
                column: "RiskCategoryId",
                principalTable: "RiskCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Risks_RiskId",
                table: "Tickets",
                column: "RiskId",
                principalTable: "Risks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Risks_RiskCategories_RiskCategoryId",
                table: "Risks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Risks_RiskId",
                table: "Tickets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Risks",
                table: "Risks");

            migrationBuilder.RenameTable(
                name: "Risks",
                newName: "Risk");

            migrationBuilder.RenameIndex(
                name: "IX_Risks_RiskCategoryId",
                table: "Risk",
                newName: "IX_Risk_RiskCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Risk",
                table: "Risk",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Risk_RiskCategories_RiskCategoryId",
                table: "Risk",
                column: "RiskCategoryId",
                principalTable: "RiskCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Risk_RiskId",
                table: "Tickets",
                column: "RiskId",
                principalTable: "Risk",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

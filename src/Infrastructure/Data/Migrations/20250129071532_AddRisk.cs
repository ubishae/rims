using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RIMS.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRisk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RiskId",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Risk",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImpactScore = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProbabilityScore = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RiskScore = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    RiskCategoryId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Risk", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Risk_RiskCategories_RiskCategoryId",
                        column: x => x.RiskCategoryId,
                        principalTable: "RiskCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_RiskId",
                table: "Tickets",
                column: "RiskId");

            migrationBuilder.CreateIndex(
                name: "IX_Risk_RiskCategoryId",
                table: "Risk",
                column: "RiskCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Risk_RiskId",
                table: "Tickets",
                column: "RiskId",
                principalTable: "Risk",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Risk_RiskId",
                table: "Tickets");

            migrationBuilder.DropTable(
                name: "Risk");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_RiskId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "RiskId",
                table: "Tickets");
        }
    }
}

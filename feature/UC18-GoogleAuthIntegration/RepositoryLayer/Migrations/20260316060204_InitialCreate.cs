using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepositoryLayer.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuantityMeasurements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OperationType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FirstValue = table.Column<double>(type: "float", nullable: true),
                    FirstUnit = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    FirstMeasurementType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SecondValue = table.Column<double>(type: "float", nullable: true),
                    SecondUnit = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    SecondMeasurementType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ResultValue = table.Column<double>(type: "float", nullable: true),
                    ResultUnit = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    ResultMeasurementType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    HasError = table.Column<bool>(type: "bit", nullable: false),
                    ErrorMessage = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuantityMeasurements", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuantityMeasurements_FirstMeasurementType",
                table: "QuantityMeasurements",
                column: "FirstMeasurementType");

            migrationBuilder.CreateIndex(
                name: "IX_QuantityMeasurements_OperationType",
                table: "QuantityMeasurements",
                column: "OperationType");

            migrationBuilder.CreateIndex(
                name: "IX_QuantityMeasurements_Timestamp",
                table: "QuantityMeasurements",
                column: "Timestamp");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuantityMeasurements");
        }
    }
}

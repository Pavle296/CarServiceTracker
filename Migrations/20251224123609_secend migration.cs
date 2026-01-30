using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarServiceTracker.Migrations
{
    /// <inheritdoc />
    public partial class secendmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CarId",
                table: "Maintenances",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Maintenances_CarId",
                table: "Maintenances",
                column: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Maintenances_Cars_CarId",
                table: "Maintenances",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Maintenances_Cars_CarId",
                table: "Maintenances");

            migrationBuilder.DropIndex(
                name: "IX_Maintenances_CarId",
                table: "Maintenances");

            migrationBuilder.DropColumn(
                name: "CarId",
                table: "Maintenances");
        }
    }
}

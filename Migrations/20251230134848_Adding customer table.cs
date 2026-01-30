using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarServiceTracker.Migrations
{
    /// <inheritdoc />
    public partial class Addingcustomertable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Maintenances");

            migrationBuilder.AddColumn<int>(
                name: "CurrentStatus",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FuelType",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CustomerId",
                table: "Cars",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Customers_CustomerId",
                table: "Cars",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Customers_CustomerId",
                table: "Cars");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Cars_CustomerId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "CurrentStatus",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "FuelType",
                table: "Cars");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Duration",
                table: "Maintenances",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }
    }
}

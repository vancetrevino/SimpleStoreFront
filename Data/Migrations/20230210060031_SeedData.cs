using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleStoreFront.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2023, 2, 10, 6, 0, 31, 262, DateTimeKind.Utc).AddTicks(2082));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2023, 2, 10, 5, 0, 45, 928, DateTimeKind.Utc).AddTicks(6393));
        }
    }
}

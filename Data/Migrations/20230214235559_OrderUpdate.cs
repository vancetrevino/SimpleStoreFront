using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleStoreFront.Migrations
{
    public partial class OrderUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2023, 2, 14, 23, 55, 59, 765, DateTimeKind.Utc).AddTicks(8172));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2023, 2, 14, 23, 48, 50, 348, DateTimeKind.Utc).AddTicks(2093));
        }
    }
}

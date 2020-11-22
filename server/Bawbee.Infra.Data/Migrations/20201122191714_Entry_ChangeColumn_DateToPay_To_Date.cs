using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bawbee.Infra.Data.Migrations
{
    public partial class Entry_ChangeColumn_DateToPay_To_Date : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateToPay",
                table: "Entries");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Entries",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Entries");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateToPay",
                table: "Entries",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}

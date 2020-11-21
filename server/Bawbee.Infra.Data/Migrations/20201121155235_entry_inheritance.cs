using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bawbee.Infra.Data.Migrations
{
    public partial class entry_inheritance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entries_EntryCategories_EntryCategoryId",
                table: "Entries");

            migrationBuilder.DropTable(
                name: "EntryCategories");

            migrationBuilder.DropIndex(
                name: "IX_Entries_EntryCategoryId",
                table: "Entries");

            migrationBuilder.DropColumn(
                name: "EntryCategoryId",
                table: "Entries");

            migrationBuilder.AlterColumn<string>(
                name: "Observations",
                table: "Entries",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Entries",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Entries",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Entry_Type",
                table: "Entries",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Entries_CategoryId",
                table: "Entries",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_UserId",
                table: "Categories",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Entries_Categories_CategoryId",
                table: "Entries",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entries_Categories_CategoryId",
                table: "Entries");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Entries_CategoryId",
                table: "Entries");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Entries");

            migrationBuilder.DropColumn(
                name: "Entry_Type",
                table: "Entries");

            migrationBuilder.AlterColumn<string>(
                name: "Observations",
                table: "Entries",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Entries",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EntryCategoryId",
                table: "Entries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "EntryCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntryCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntryCategories_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Entries_EntryCategoryId",
                table: "Entries",
                column: "EntryCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_EntryCategories_UserId",
                table: "EntryCategories",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Entries_EntryCategories_EntryCategoryId",
                table: "Entries",
                column: "EntryCategoryId",
                principalTable: "EntryCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PokemonAPI.DomainLayer.Migrations
{
    public partial class Seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Abilities",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("0faee6ac-1772-4bbe-9990-a7d9a22dd539"), "Throws a sharp leaf at opponent.", "Leaf throw" },
                    { new Guid("0faee6ac-1772-4bbe-9990-a7d9a22dd549"), "Hardens skin to repel enemy attacks.", "Harden" }
                });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "PasswordHash", "Username" },
                values: new object[] { new Guid("0faee6ac-1772-4bbe-9990-a7d9a22dd559"), null, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Abilities",
                keyColumn: "Id",
                keyValue: new Guid("0faee6ac-1772-4bbe-9990-a7d9a22dd539"));

            migrationBuilder.DeleteData(
                table: "Abilities",
                keyColumn: "Id",
                keyValue: new Guid("0faee6ac-1772-4bbe-9990-a7d9a22dd549"));

            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: new Guid("0faee6ac-1772-4bbe-9990-a7d9a22dd559"));
        }
    }
}

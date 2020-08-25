using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PokemonAPI.DomainLayer.Migrations
{
    public partial class TrainerSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Trainers",
                columns: new[] { "Id", "FirstName", "LastName", "Address_City", "Address_HouseNumber", "Address_Street" },
                values: new object[] { new Guid("00dc1dbd-4868-4b4a-934d-1d0b165ea104"), "Ash", "Ketcham", "Bogota", "10", "Las Venturas" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: new Guid("00dc1dbd-4868-4b4a-934d-1d0b165ea104"));
        }
    }
}

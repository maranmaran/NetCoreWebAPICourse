using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PokemonAPI.DomainLayer.Migrations
{
    public partial class Seed2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: new Guid("0faee6ac-1772-4bbe-9990-a7d9a22dd559"));

            migrationBuilder.AlterColumn<string>(
                name: "Avatar",
                table: "Pokemons",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Pokemons",
                columns: new[] { "Id", "Avatar", "Generation", "Height", "Name", "Type", "Weight", "BaseStats_Attack", "BaseStats_Defense", "BaseStats_HealthPoints", "BaseStats_SpecialAttack", "BaseStats_SpecialDefense", "BaseStats_Speed" },
                values: new object[] { new Guid("0faee6ac-1772-4bbe-9990-a7d9a22dd559"), "https://i.pinimg.com/originals/3b/78/47/3b7847675982776e5219e12a680ecd84.png", 0, 20f, "Bulbasaur", "Bug", 110f, 30, 20, 100, 60, 50, 90 });

            migrationBuilder.InsertData(
                table: "PokemonAbilities",
                columns: new[] { "Id", "AbilityId", "PokemonId" },
                values: new object[,]
                {
                    { new Guid("312c1c87-75ba-48ea-b7a0-6a31c19dae91"), new Guid("0faee6ac-1772-4bbe-9990-a7d9a22dd539"), new Guid("0faee6ac-1772-4bbe-9990-a7d9a22dd559") },
                    { new Guid("49f2b00a-9a79-4cbe-8001-c82d0bedfaa1"), new Guid("0faee6ac-1772-4bbe-9990-a7d9a22dd549"), new Guid("0faee6ac-1772-4bbe-9990-a7d9a22dd559") }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PokemonAbilities",
                keyColumn: "Id",
                keyValue: new Guid("312c1c87-75ba-48ea-b7a0-6a31c19dae91"));

            migrationBuilder.DeleteData(
                table: "PokemonAbilities",
                keyColumn: "Id",
                keyValue: new Guid("49f2b00a-9a79-4cbe-8001-c82d0bedfaa1"));

            migrationBuilder.DeleteData(
                table: "Pokemons",
                keyColumn: "Id",
                keyValue: new Guid("0faee6ac-1772-4bbe-9990-a7d9a22dd559"));

            migrationBuilder.AlterColumn<string>(
                name: "Avatar",
                table: "Pokemons",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "PasswordHash", "Username" },
                values: new object[] { new Guid("0faee6ac-1772-4bbe-9990-a7d9a22dd559"), null, null });
        }
    }
}

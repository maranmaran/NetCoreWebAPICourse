using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PokemonAPI.DomainLayer.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Abilities",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abilities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pokemons",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Avatar = table.Column<string>(maxLength: 50, nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Generation = table.Column<int>(nullable: true),
                    Height = table.Column<float>(nullable: false),
                    Weight = table.Column<float>(nullable: false),
                    Type = table.Column<string>(nullable: false),
                    BaseStats_HealthPoints = table.Column<int>(nullable: true),
                    BaseStats_Attack = table.Column<int>(nullable: true),
                    BaseStats_Defense = table.Column<int>(nullable: true),
                    BaseStats_SpecialAttack = table.Column<int>(nullable: true),
                    BaseStats_SpecialDefense = table.Column<int>(nullable: true),
                    BaseStats_Speed = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pokemons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PokemonAbilities",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AbilityId = table.Column<Guid>(nullable: false),
                    PokemonId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonAbilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PokemonAbilities_Abilities_AbilityId",
                        column: x => x.AbilityId,
                        principalTable: "Abilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PokemonAbilities_Pokemons_PokemonId",
                        column: x => x.PokemonId,
                        principalTable: "Pokemons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PokemonAbilities_AbilityId",
                table: "PokemonAbilities",
                column: "AbilityId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonAbilities_PokemonId",
                table: "PokemonAbilities",
                column: "PokemonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PokemonAbilities");

            migrationBuilder.DropTable(
                name: "Abilities");

            migrationBuilder.DropTable(
                name: "Pokemons");
        }
    }
}

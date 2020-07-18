using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PokemonAPI.DomainLayer.Migrations
{
    public partial class BaseStatValObject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BaseStats");

            migrationBuilder.DropColumn(
                name: "BaseStatsId",
                table: "Pokemons");

            migrationBuilder.AddColumn<int>(
                name: "BaseStats_Attack",
                table: "Pokemons",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BaseStats_Defense",
                table: "Pokemons",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BaseStats_HealthPoints",
                table: "Pokemons",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BaseStats_SpecialAttack",
                table: "Pokemons",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BaseStats_SpecialDefense",
                table: "Pokemons",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BaseStats_Speed",
                table: "Pokemons",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BaseStats_Attack",
                table: "Pokemons");

            migrationBuilder.DropColumn(
                name: "BaseStats_Defense",
                table: "Pokemons");

            migrationBuilder.DropColumn(
                name: "BaseStats_HealthPoints",
                table: "Pokemons");

            migrationBuilder.DropColumn(
                name: "BaseStats_SpecialAttack",
                table: "Pokemons");

            migrationBuilder.DropColumn(
                name: "BaseStats_SpecialDefense",
                table: "Pokemons");

            migrationBuilder.DropColumn(
                name: "BaseStats_Speed",
                table: "Pokemons");

            migrationBuilder.AddColumn<Guid>(
                name: "BaseStatsId",
                table: "Pokemons",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "BaseStats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Attack = table.Column<int>(type: "int", nullable: false),
                    Defense = table.Column<int>(type: "int", nullable: false),
                    HealthPoints = table.Column<int>(type: "int", nullable: false),
                    PokemonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SpecialAttack = table.Column<int>(type: "int", nullable: false),
                    SpecialDefense = table.Column<int>(type: "int", nullable: false),
                    Speed = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseStats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BaseStats_Pokemons_PokemonId",
                        column: x => x.PokemonId,
                        principalTable: "Pokemons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaseStats_PokemonId",
                table: "BaseStats",
                column: "PokemonId",
                unique: true);
        }
    }
}

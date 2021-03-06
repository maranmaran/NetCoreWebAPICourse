﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PokemonAPI.DomainLayer;

namespace PokemonAPI.DomainLayer.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20200824180518_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("PokemonAPI.DomainLayer.Entities.Ability", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Abilities");
                });

            modelBuilder.Entity("PokemonAPI.DomainLayer.Entities.Admin", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Admins");

                    b.HasData(
                        new
                        {
                            Id = new Guid("0faee6ac-1772-4bbe-9990-a7d9a22dd529"),
                            PasswordHash = "8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918",
                            Username = "admin"
                        });
                });

            modelBuilder.Entity("PokemonAPI.DomainLayer.Entities.Pokemon", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Avatar")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<int?>("Generation")
                        .HasColumnType("integer");

                    b.Property<float>("Height")
                        .HasColumnType("real");

                    b.Property<string>("Name")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<float>("Weight")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("Pokemons");
                });

            modelBuilder.Entity("PokemonAPI.DomainLayer.Entities.PokemonAbility", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AbilityId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("PokemonId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("AbilityId");

                    b.HasIndex("PokemonId");

                    b.ToTable("PokemonAbilities");
                });

            modelBuilder.Entity("PokemonAPI.DomainLayer.Entities.Pokemon", b =>
                {
                    b.OwnsOne("PokemonAPI.DomainLayer.ValueObjects.BaseStat", "BaseStats", b1 =>
                        {
                            b1.Property<Guid>("PokemonId")
                                .HasColumnType("uuid");

                            b1.Property<int>("Attack")
                                .HasColumnType("integer");

                            b1.Property<int>("Defense")
                                .HasColumnType("integer");

                            b1.Property<int>("HealthPoints")
                                .HasColumnType("integer");

                            b1.Property<int>("SpecialAttack")
                                .HasColumnType("integer");

                            b1.Property<int>("SpecialDefense")
                                .HasColumnType("integer");

                            b1.Property<int>("Speed")
                                .HasColumnType("integer");

                            b1.HasKey("PokemonId");

                            b1.ToTable("Pokemons");

                            b1.WithOwner()
                                .HasForeignKey("PokemonId");
                        });
                });

            modelBuilder.Entity("PokemonAPI.DomainLayer.Entities.PokemonAbility", b =>
                {
                    b.HasOne("PokemonAPI.DomainLayer.Entities.Ability", "Ability")
                        .WithMany("Pokemons")
                        .HasForeignKey("AbilityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PokemonAPI.DomainLayer.Entities.Pokemon", "Pokemon")
                        .WithMany("Abilities")
                        .HasForeignKey("PokemonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

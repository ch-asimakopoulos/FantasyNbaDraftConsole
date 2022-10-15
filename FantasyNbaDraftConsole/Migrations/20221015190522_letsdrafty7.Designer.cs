﻿// <auto-generated />
using System;
using FantasyNbaDraftConsole.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FantasyNbaDraftConsole.Migrations
{
    [DbContext(typeof(NBADbContext))]
    [Migration("20221015190522_letsdrafty7")]
    partial class letsdrafty7
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "position_type_id", new[] { "invalid", "point_guard", "shooting_guard", "small_forward", "power_forward", "center", "guard", "forward" });
            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "projection_totals_type_id", new[] { "invalid", "points", "assists", "rebounds", "blocks", "steals", "turnovers", "field_goals_made", "field_goals_attempted", "three_pointers_made", "three_pointers_attempted", "free_throws_made", "free_throws_attempted", "games_played", "minutes_per_game", "total_ranking" });
            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "projection_type_id", new[] { "invalid", "points", "assists", "rebounds", "blocks", "steals", "turnovers", "field_goal_percentage", "three_pointers_made", "free_throw_percentage" });
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FantasyNbaDraftConsole.Data.Models.DraftConfig", b =>
                {
                    b.Property<byte>("LeagueId")
                        .HasColumnType("smallint");

                    b.Property<byte>("Rounds")
                        .HasColumnType("smallint");

                    b.Property<bool>("Snake")
                        .HasColumnType("boolean");

                    b.HasKey("LeagueId");

                    b.ToTable("DraftConfig");
                });

            modelBuilder.Entity("FantasyNbaDraftConsole.Data.Models.League", b =>
                {
                    b.Property<byte>("LeagueId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<byte>("LeagueId"));

                    b.Property<bool>("LeagueInitialized")
                        .HasColumnType("boolean");

                    b.Property<string>("LeagueName")
                        .HasColumnType("text");

                    b.HasKey("LeagueId");

                    b.ToTable("Leagues");
                });

            modelBuilder.Entity("FantasyNbaDraftConsole.Data.Models.Player", b =>
                {
                    b.Property<int>("PlayerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("PlayerId"));

                    b.Property<bool>("Injured")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("NbaTeam")
                        .HasColumnType("text");

                    b.Property<bool>("Starred")
                        .HasColumnType("boolean");

                    b.Property<byte?>("TeamId")
                        .HasColumnType("smallint");

                    b.HasKey("PlayerId");

                    b.HasIndex("Name");

                    b.HasIndex("TeamId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("FantasyNbaDraftConsole.Data.Models.Position", b =>
                {
                    b.Property<int>("PlayerId")
                        .HasColumnType("integer");

                    b.Property<byte>("PositionTypeId")
                        .HasColumnType("smallint");

                    b.Property<int?>("PlayerId1")
                        .HasColumnType("integer");

                    b.HasKey("PlayerId", "PositionTypeId");

                    b.HasIndex("PlayerId1");

                    b.ToTable("Positions");
                });

            modelBuilder.Entity("FantasyNbaDraftConsole.Data.Models.Projection", b =>
                {
                    b.Property<int>("PlayerId")
                        .HasColumnType("integer");

                    b.Property<byte>("ProjectionTypeId")
                        .HasColumnType("smallint");

                    b.Property<decimal>("ProjectionValue")
                        .HasColumnType("numeric");

                    b.HasKey("PlayerId", "ProjectionTypeId");

                    b.ToTable("Projection");
                });

            modelBuilder.Entity("FantasyNbaDraftConsole.Data.Models.ProjectionTotal", b =>
                {
                    b.Property<int>("PlayerId")
                        .HasColumnType("integer");

                    b.Property<byte>("ProjectionTotalsTypeId")
                        .HasColumnType("smallint");

                    b.Property<decimal>("ProjectionValue")
                        .HasColumnType("numeric");

                    b.HasKey("PlayerId", "ProjectionTotalsTypeId");

                    b.ToTable("ProjectionTotal");
                });

            modelBuilder.Entity("FantasyNbaDraftConsole.Data.Models.Team", b =>
                {
                    b.Property<byte>("TeamId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<byte>("TeamId"));

                    b.Property<byte>("LeagueId")
                        .HasColumnType("smallint");

                    b.Property<byte>("TeamDraftPosition")
                        .HasColumnType("smallint");

                    b.Property<string>("TeamName")
                        .HasColumnType("text");

                    b.HasKey("TeamId");

                    b.HasIndex("LeagueId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("FantasyNbaDraftConsole.Data.Models.DraftConfig", b =>
                {
                    b.HasOne("FantasyNbaDraftConsole.Data.Models.League", "League")
                        .WithOne("DraftConfig")
                        .HasForeignKey("FantasyNbaDraftConsole.Data.Models.DraftConfig", "LeagueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("League");
                });

            modelBuilder.Entity("FantasyNbaDraftConsole.Data.Models.Player", b =>
                {
                    b.HasOne("FantasyNbaDraftConsole.Data.Models.Team", "Team")
                        .WithMany("DraftedPlayers")
                        .HasForeignKey("TeamId");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("FantasyNbaDraftConsole.Data.Models.Position", b =>
                {
                    b.HasOne("FantasyNbaDraftConsole.Data.Models.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FantasyNbaDraftConsole.Data.Models.Player", null)
                        .WithMany("Positions")
                        .HasForeignKey("PlayerId1");

                    b.Navigation("Player");
                });

            modelBuilder.Entity("FantasyNbaDraftConsole.Data.Models.Projection", b =>
                {
                    b.HasOne("FantasyNbaDraftConsole.Data.Models.Player", null)
                        .WithMany("Projections")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FantasyNbaDraftConsole.Data.Models.ProjectionTotal", b =>
                {
                    b.HasOne("FantasyNbaDraftConsole.Data.Models.Player", null)
                        .WithMany("ProjectionTotals")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FantasyNbaDraftConsole.Data.Models.Team", b =>
                {
                    b.HasOne("FantasyNbaDraftConsole.Data.Models.League", "League")
                        .WithMany("LeagueTeams")
                        .HasForeignKey("LeagueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("League");
                });

            modelBuilder.Entity("FantasyNbaDraftConsole.Data.Models.League", b =>
                {
                    b.Navigation("DraftConfig");

                    b.Navigation("LeagueTeams");
                });

            modelBuilder.Entity("FantasyNbaDraftConsole.Data.Models.Player", b =>
                {
                    b.Navigation("Positions");

                    b.Navigation("ProjectionTotals");

                    b.Navigation("Projections");
                });

            modelBuilder.Entity("FantasyNbaDraftConsole.Data.Models.Team", b =>
                {
                    b.Navigation("DraftedPlayers");
                });
#pragma warning restore 612, 618
        }
    }
}

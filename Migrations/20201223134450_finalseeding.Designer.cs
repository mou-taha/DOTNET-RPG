﻿// <auto-generated />
using System;
using DOTNET_RPG.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DOTNET_RPG.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20201223134450_finalseeding")]
    partial class finalseeding
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("DOTNET_RPG.Models.Character", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Class")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Defeats")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Defense")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Fights")
                        .HasColumnType("INTEGER");

                    b.Property<int>("HitPointes")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Intelligence")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int>("Strength")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Victories")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("characters");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Class = 1,
                            Defeats = 0,
                            Defense = 10,
                            Fights = 0,
                            HitPointes = 100,
                            Intelligence = 10,
                            Name = "Frodo",
                            Strength = 15,
                            UserId = 1,
                            Victories = 0
                        },
                        new
                        {
                            Id = 2,
                            Class = 2,
                            Defeats = 0,
                            Defense = 5,
                            Fights = 0,
                            HitPointes = 100,
                            Intelligence = 20,
                            Name = "Raistlin",
                            Strength = 5,
                            UserId = 2,
                            Victories = 0
                        });
                });

            modelBuilder.Entity("DOTNET_RPG.Models.CharacterSkill", b =>
                {
                    b.Property<int>("CharacterId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SkillId")
                        .HasColumnType("INTEGER");

                    b.HasKey("CharacterId", "SkillId");

                    b.HasIndex("SkillId");

                    b.ToTable("characterSkills");

                    b.HasData(
                        new
                        {
                            CharacterId = 1,
                            SkillId = 2
                        },
                        new
                        {
                            CharacterId = 2,
                            SkillId = 1
                        },
                        new
                        {
                            CharacterId = 2,
                            SkillId = 3
                        });
                });

            modelBuilder.Entity("DOTNET_RPG.Models.Skill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Damage")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Skills");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Damage = 30,
                            Name = "Fireball"
                        },
                        new
                        {
                            Id = 2,
                            Damage = 20,
                            Name = "Frenzy"
                        },
                        new
                        {
                            Id = 3,
                            Damage = 50,
                            Name = "Blizzard"
                        });
                });

            modelBuilder.Entity("DOTNET_RPG.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("BLOB");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("BLOB");

                    b.Property<string>("Role")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue("Player");

                    b.Property<string>("Username")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            PasswordHash = new byte[] { 17, 181, 177, 55, 108, 179, 101, 138, 201, 78, 15, 83, 112, 213, 136, 242, 198, 217, 104, 196, 61, 1, 22, 113, 230, 124, 201, 237, 181, 242, 92, 126, 146, 170, 226, 128, 18, 75, 177, 156, 139, 41, 251, 239, 84, 133, 137, 138, 144, 145, 206, 63, 248, 100, 150, 56, 130, 96, 50, 44, 6, 50, 8, 46 },
                            PasswordSalt = new byte[] { 18, 154, 184, 238, 218, 152, 204, 136, 115, 215, 158, 242, 205, 167, 100, 96, 183, 203, 166, 242, 117, 249, 6, 16, 65, 244, 221, 163, 102, 48, 8, 105, 164, 15, 8, 255, 191, 218, 1, 146, 4, 74, 253, 141, 40, 76, 5, 91, 63, 214, 254, 2, 140, 233, 210, 247, 5, 110, 220, 204, 193, 12, 151, 103, 168, 232, 6, 202, 78, 76, 71, 59, 104, 30, 188, 88, 114, 133, 84, 206, 156, 170, 138, 157, 158, 37, 221, 190, 148, 112, 36, 13, 30, 146, 159, 214, 188, 178, 212, 30, 250, 91, 239, 95, 9, 162, 99, 129, 11, 152, 174, 204, 112, 150, 55, 1, 95, 10, 250, 159, 17, 158, 71, 77, 54, 216, 39, 86 },
                            Username = "User1"
                        },
                        new
                        {
                            Id = 2,
                            PasswordHash = new byte[] { 17, 181, 177, 55, 108, 179, 101, 138, 201, 78, 15, 83, 112, 213, 136, 242, 198, 217, 104, 196, 61, 1, 22, 113, 230, 124, 201, 237, 181, 242, 92, 126, 146, 170, 226, 128, 18, 75, 177, 156, 139, 41, 251, 239, 84, 133, 137, 138, 144, 145, 206, 63, 248, 100, 150, 56, 130, 96, 50, 44, 6, 50, 8, 46 },
                            PasswordSalt = new byte[] { 18, 154, 184, 238, 218, 152, 204, 136, 115, 215, 158, 242, 205, 167, 100, 96, 183, 203, 166, 242, 117, 249, 6, 16, 65, 244, 221, 163, 102, 48, 8, 105, 164, 15, 8, 255, 191, 218, 1, 146, 4, 74, 253, 141, 40, 76, 5, 91, 63, 214, 254, 2, 140, 233, 210, 247, 5, 110, 220, 204, 193, 12, 151, 103, 168, 232, 6, 202, 78, 76, 71, 59, 104, 30, 188, 88, 114, 133, 84, 206, 156, 170, 138, 157, 158, 37, 221, 190, 148, 112, 36, 13, 30, 146, 159, 214, 188, 178, 212, 30, 250, 91, 239, 95, 9, 162, 99, 129, 11, 152, 174, 204, 112, 150, 55, 1, 95, 10, 250, 159, 17, 158, 71, 77, 54, 216, 39, 86 },
                            Username = "User2"
                        });
                });

            modelBuilder.Entity("DOTNET_RPG.Models.Weapon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CharacterId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Damage")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CharacterId")
                        .IsUnique();

                    b.ToTable("Weapons");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CharacterId = 1,
                            Damage = 20,
                            Name = "The Master Sword"
                        },
                        new
                        {
                            Id = 2,
                            CharacterId = 2,
                            Damage = 5,
                            Name = "Crystal Wand"
                        });
                });

            modelBuilder.Entity("DOTNET_RPG.Models.Character", b =>
                {
                    b.HasOne("DOTNET_RPG.Models.User", "User")
                        .WithMany("Characters")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DOTNET_RPG.Models.CharacterSkill", b =>
                {
                    b.HasOne("DOTNET_RPG.Models.Character", "Character")
                        .WithMany("CharacterSkills")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DOTNET_RPG.Models.Skill", "Skill")
                        .WithMany("CharacterSkills")
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Character");

                    b.Navigation("Skill");
                });

            modelBuilder.Entity("DOTNET_RPG.Models.Weapon", b =>
                {
                    b.HasOne("DOTNET_RPG.Models.Character", "Character")
                        .WithOne("Weapon")
                        .HasForeignKey("DOTNET_RPG.Models.Weapon", "CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Character");
                });

            modelBuilder.Entity("DOTNET_RPG.Models.Character", b =>
                {
                    b.Navigation("CharacterSkills");

                    b.Navigation("Weapon");
                });

            modelBuilder.Entity("DOTNET_RPG.Models.Skill", b =>
                {
                    b.Navigation("CharacterSkills");
                });

            modelBuilder.Entity("DOTNET_RPG.Models.User", b =>
                {
                    b.Navigation("Characters");
                });
#pragma warning restore 612, 618
        }
    }
}

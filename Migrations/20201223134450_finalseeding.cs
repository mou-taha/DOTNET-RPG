using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DOTNET_RPG.Migrations
{
    public partial class finalseeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "PasswordHash", "PasswordSalt", "Username" },
                values: new object[] { 1, new byte[] { 17, 181, 177, 55, 108, 179, 101, 138, 201, 78, 15, 83, 112, 213, 136, 242, 198, 217, 104, 196, 61, 1, 22, 113, 230, 124, 201, 237, 181, 242, 92, 126, 146, 170, 226, 128, 18, 75, 177, 156, 139, 41, 251, 239, 84, 133, 137, 138, 144, 145, 206, 63, 248, 100, 150, 56, 130, 96, 50, 44, 6, 50, 8, 46 }, new byte[] { 18, 154, 184, 238, 218, 152, 204, 136, 115, 215, 158, 242, 205, 167, 100, 96, 183, 203, 166, 242, 117, 249, 6, 16, 65, 244, 221, 163, 102, 48, 8, 105, 164, 15, 8, 255, 191, 218, 1, 146, 4, 74, 253, 141, 40, 76, 5, 91, 63, 214, 254, 2, 140, 233, 210, 247, 5, 110, 220, 204, 193, 12, 151, 103, 168, 232, 6, 202, 78, 76, 71, 59, 104, 30, 188, 88, 114, 133, 84, 206, 156, 170, 138, 157, 158, 37, 221, 190, 148, 112, 36, 13, 30, 146, 159, 214, 188, 178, 212, 30, 250, 91, 239, 95, 9, 162, 99, 129, 11, 152, 174, 204, 112, 150, 55, 1, 95, 10, 250, 159, 17, 158, 71, 77, 54, 216, 39, 86 }, "User1" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "PasswordHash", "PasswordSalt", "Username" },
                values: new object[] { 2, new byte[] { 17, 181, 177, 55, 108, 179, 101, 138, 201, 78, 15, 83, 112, 213, 136, 242, 198, 217, 104, 196, 61, 1, 22, 113, 230, 124, 201, 237, 181, 242, 92, 126, 146, 170, 226, 128, 18, 75, 177, 156, 139, 41, 251, 239, 84, 133, 137, 138, 144, 145, 206, 63, 248, 100, 150, 56, 130, 96, 50, 44, 6, 50, 8, 46 }, new byte[] { 18, 154, 184, 238, 218, 152, 204, 136, 115, 215, 158, 242, 205, 167, 100, 96, 183, 203, 166, 242, 117, 249, 6, 16, 65, 244, 221, 163, 102, 48, 8, 105, 164, 15, 8, 255, 191, 218, 1, 146, 4, 74, 253, 141, 40, 76, 5, 91, 63, 214, 254, 2, 140, 233, 210, 247, 5, 110, 220, 204, 193, 12, 151, 103, 168, 232, 6, 202, 78, 76, 71, 59, 104, 30, 188, 88, 114, 133, 84, 206, 156, 170, 138, 157, 158, 37, 221, 190, 148, 112, 36, 13, 30, 146, 159, 214, 188, 178, 212, 30, 250, 91, 239, 95, 9, 162, 99, 129, 11, 152, 174, 204, 112, 150, 55, 1, 95, 10, 250, 159, 17, 158, 71, 77, 54, 216, 39, 86 }, "User2" });

            migrationBuilder.InsertData(
                table: "characters",
                columns: new[] { "Id", "Class", "Defeats", "Defense", "Fights", "HitPointes", "Intelligence", "Name", "Strength", "UserId", "Victories" },
                values: new object[] { 1, 1, 0, 10, 0, 100, 10, "Frodo", 15, 1, 0 });

            migrationBuilder.InsertData(
                table: "characters",
                columns: new[] { "Id", "Class", "Defeats", "Defense", "Fights", "HitPointes", "Intelligence", "Name", "Strength", "UserId", "Victories" },
                values: new object[] { 2, 2, 0, 5, 0, 100, 20, "Raistlin", 5, 2, 0 });

            migrationBuilder.InsertData(
                table: "Weapons",
                columns: new[] { "Id", "CharacterId", "Damage", "Name" },
                values: new object[] { 1, 1, 20, "The Master Sword" });

            migrationBuilder.InsertData(
                table: "Weapons",
                columns: new[] { "Id", "CharacterId", "Damage", "Name" },
                values: new object[] { 2, 2, 5, "Crystal Wand" });

            migrationBuilder.InsertData(
                table: "characterSkills",
                columns: new[] { "CharacterId", "SkillId" },
                values: new object[] { 1, 2 });

            migrationBuilder.InsertData(
                table: "characterSkills",
                columns: new[] { "CharacterId", "SkillId" },
                values: new object[] { 2, 1 });

            migrationBuilder.InsertData(
                table: "characterSkills",
                columns: new[] { "CharacterId", "SkillId" },
                values: new object[] { 2, 3 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Weapons",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Weapons",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "characterSkills",
                keyColumns: new[] { "CharacterId", "SkillId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "characterSkills",
                keyColumns: new[] { "CharacterId", "SkillId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "characterSkills",
                keyColumns: new[] { "CharacterId", "SkillId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "characters",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "characters",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DrugPreventionSystem.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddContentToLesson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("44924a66-1181-4270-92d7-677f15e9915b"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("8eb84ac4-3f7d-4532-8201-ea1bbe6cb53f"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("ce4ad697-ae92-48f7-b143-cc0ac147718e"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("e8efb403-2ffa-4c86-a88a-20bff0200045"));

            migrationBuilder.AddColumn<string>(
                name: "content",
                table: "lessons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 6, 26, 8, 31, 1, 717, DateTimeKind.Local).AddTicks(2479));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 6, 26, 8, 31, 1, 717, DateTimeKind.Local).AddTicks(2527));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2025, 6, 26, 8, 31, 1, 717, DateTimeKind.Local).AddTicks(2529));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 4,
                column: "created_at",
                value: new DateTime(2025, 6, 26, 8, 31, 1, 717, DateTimeKind.Local).AddTicks(2529));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 5,
                column: "created_at",
                value: new DateTime(2025, 6, 26, 8, 31, 1, 717, DateTimeKind.Local).AddTicks(2530));

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "user_id", "created_at", "email", "email_verified", "is_active", "last_login", "password_hash", "role_id", "updated_at", "username" },
                values: new object[,]
                {
                    { new Guid("39db358c-4663-4217-ac02-a60ee2d05eb5"), new DateTime(2025, 6, 26, 8, 31, 1, 880, DateTimeKind.Local).AddTicks(905), "staff@example.com", true, true, null, "$2a$11$367l6rCe95ld9BD/2FlIyuE3obqEfRms53aADdP4aYecAbLctNWrW", 3, null, "staff_user" },
                    { new Guid("3d09cd51-7a72-4986-b238-35a2530caf28"), new DateTime(2025, 6, 26, 8, 31, 1, 880, DateTimeKind.Local).AddTicks(927), "consultant@example.com", true, true, null, "$2a$11$367l6rCe95ld9BD/2FlIyuE3obqEfRms53aADdP4aYecAbLctNWrW", 4, null, "consultant_user" },
                    { new Guid("c01a681a-bb0f-48b9-9c87-cfa3b11380d8"), new DateTime(2025, 6, 26, 8, 31, 1, 880, DateTimeKind.Local).AddTicks(890), "admin@example.com", true, true, null, "$2a$11$367l6rCe95ld9BD/2FlIyuE3obqEfRms53aADdP4aYecAbLctNWrW", 1, null, "admin_user" },
                    { new Guid("c984d4a5-71eb-4442-a0a1-4ab5dbd0f781"), new DateTime(2025, 6, 26, 8, 31, 1, 880, DateTimeKind.Local).AddTicks(900), "manager@example.com", true, true, null, "$2a$11$367l6rCe95ld9BD/2FlIyuE3obqEfRms53aADdP4aYecAbLctNWrW", 2, null, "manager_user" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("39db358c-4663-4217-ac02-a60ee2d05eb5"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("3d09cd51-7a72-4986-b238-35a2530caf28"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("c01a681a-bb0f-48b9-9c87-cfa3b11380d8"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("c984d4a5-71eb-4442-a0a1-4ab5dbd0f781"));

            migrationBuilder.DropColumn(
                name: "content",
                table: "lessons");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 6, 25, 11, 23, 1, 740, DateTimeKind.Local).AddTicks(3153));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 6, 25, 11, 23, 1, 740, DateTimeKind.Local).AddTicks(3212));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2025, 6, 25, 11, 23, 1, 740, DateTimeKind.Local).AddTicks(3215));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 4,
                column: "created_at",
                value: new DateTime(2025, 6, 25, 11, 23, 1, 740, DateTimeKind.Local).AddTicks(3217));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 5,
                column: "created_at",
                value: new DateTime(2025, 6, 25, 11, 23, 1, 740, DateTimeKind.Local).AddTicks(3220));

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "user_id", "created_at", "email", "email_verified", "is_active", "last_login", "password_hash", "role_id", "updated_at", "username" },
                values: new object[,]
                {
                    { new Guid("44924a66-1181-4270-92d7-677f15e9915b"), new DateTime(2025, 6, 25, 11, 23, 2, 48, DateTimeKind.Local).AddTicks(5311), "staff@example.com", true, true, null, "$2a$11$u.Ltube1u7iljDUX4uM/9eqQRteTus9.fGIJ0JlcXOgrU3FiR2vRu", 3, null, "staff_user" },
                    { new Guid("8eb84ac4-3f7d-4532-8201-ea1bbe6cb53f"), new DateTime(2025, 6, 25, 11, 23, 2, 48, DateTimeKind.Local).AddTicks(5288), "admin@example.com", true, true, null, "$2a$11$u.Ltube1u7iljDUX4uM/9eqQRteTus9.fGIJ0JlcXOgrU3FiR2vRu", 1, null, "admin_user" },
                    { new Guid("ce4ad697-ae92-48f7-b143-cc0ac147718e"), new DateTime(2025, 6, 25, 11, 23, 2, 48, DateTimeKind.Local).AddTicks(5302), "manager@example.com", true, true, null, "$2a$11$u.Ltube1u7iljDUX4uM/9eqQRteTus9.fGIJ0JlcXOgrU3FiR2vRu", 2, null, "manager_user" },
                    { new Guid("e8efb403-2ffa-4c86-a88a-20bff0200045"), new DateTime(2025, 6, 25, 11, 23, 2, 48, DateTimeKind.Local).AddTicks(5319), "consultant@example.com", true, true, null, "$2a$11$u.Ltube1u7iljDUX4uM/9eqQRteTus9.fGIJ0JlcXOgrU3FiR2vRu", 4, null, "consultant_user" }
                });
        }
    }
}

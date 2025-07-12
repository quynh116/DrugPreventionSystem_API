using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DrugPreventionSystem.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class EditToNvarchar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("121d18c7-a22c-463f-a212-6ab4a9118dc6"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("2bd597dd-87da-46d1-afd6-fb9f0d3f164b"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("951ebd31-e6da-49e7-b576-8b7b23c19c3a"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("ee824d09-2c82-48f1-b491-bf38a9ac97a3"));

            migrationBuilder.AlterColumn<string>(
                name: "excerpt",
                table: "blogs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "content",
                table: "blogs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "blog_categories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 7, 10, 13, 32, 28, 97, DateTimeKind.Local).AddTicks(5062));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 7, 10, 13, 32, 28, 97, DateTimeKind.Local).AddTicks(5094));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2025, 7, 10, 13, 32, 28, 97, DateTimeKind.Local).AddTicks(5095));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 4,
                column: "created_at",
                value: new DateTime(2025, 7, 10, 13, 32, 28, 97, DateTimeKind.Local).AddTicks(5096));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 5,
                column: "created_at",
                value: new DateTime(2025, 7, 10, 13, 32, 28, 97, DateTimeKind.Local).AddTicks(5097));

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "user_id", "created_at", "email", "email_verified", "is_active", "last_login", "password_hash", "role_id", "updated_at", "username" },
                values: new object[,]
                {
                    { new Guid("77eafa7e-5ada-4f43-9de6-03fc52ce2d85"), new DateTime(2025, 7, 10, 13, 32, 28, 234, DateTimeKind.Local).AddTicks(314), "staff@example.com", true, true, null, "$2a$11$KVV4cfp.51lASUQU5lIQA.MMvbGiFPh75WkVrsni4EM.nS6is73om", 3, null, "staff_user" },
                    { new Guid("7bf0c9b7-ca66-43b1-8bea-ebdad750b9a9"), new DateTime(2025, 7, 10, 13, 32, 28, 234, DateTimeKind.Local).AddTicks(328), "consultant@example.com", true, true, null, "$2a$11$KVV4cfp.51lASUQU5lIQA.MMvbGiFPh75WkVrsni4EM.nS6is73om", 4, null, "consultant_user" },
                    { new Guid("7db4b73f-2246-432b-b653-81cddf6263ae"), new DateTime(2025, 7, 10, 13, 32, 28, 234, DateTimeKind.Local).AddTicks(310), "manager@example.com", true, true, null, "$2a$11$KVV4cfp.51lASUQU5lIQA.MMvbGiFPh75WkVrsni4EM.nS6is73om", 2, null, "manager_user" },
                    { new Guid("d649f0f1-3713-47c3-af4a-cac0014f9e9a"), new DateTime(2025, 7, 10, 13, 32, 28, 234, DateTimeKind.Local).AddTicks(300), "admin@example.com", true, true, null, "$2a$11$KVV4cfp.51lASUQU5lIQA.MMvbGiFPh75WkVrsni4EM.nS6is73om", 1, null, "admin_user" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("77eafa7e-5ada-4f43-9de6-03fc52ce2d85"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("7bf0c9b7-ca66-43b1-8bea-ebdad750b9a9"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("7db4b73f-2246-432b-b653-81cddf6263ae"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("d649f0f1-3713-47c3-af4a-cac0014f9e9a"));

            migrationBuilder.AlterColumn<string>(
                name: "excerpt",
                table: "blogs",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "content",
                table: "blogs",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "blog_categories",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 7, 8, 16, 23, 34, 711, DateTimeKind.Local).AddTicks(3788));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 7, 8, 16, 23, 34, 711, DateTimeKind.Local).AddTicks(3805));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2025, 7, 8, 16, 23, 34, 711, DateTimeKind.Local).AddTicks(3807));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 4,
                column: "created_at",
                value: new DateTime(2025, 7, 8, 16, 23, 34, 711, DateTimeKind.Local).AddTicks(3808));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 5,
                column: "created_at",
                value: new DateTime(2025, 7, 8, 16, 23, 34, 711, DateTimeKind.Local).AddTicks(3809));

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "user_id", "created_at", "email", "email_verified", "is_active", "last_login", "password_hash", "role_id", "updated_at", "username" },
                values: new object[,]
                {
                    { new Guid("121d18c7-a22c-463f-a212-6ab4a9118dc6"), new DateTime(2025, 7, 8, 16, 23, 34, 843, DateTimeKind.Local).AddTicks(8756), "consultant@example.com", true, true, null, "$2a$11$0RPQOs.f5HRmOIS47a1Q3.dItw6WsZdD5vQPS5tv3uGxlqgdDfisO", 4, null, "consultant_user" },
                    { new Guid("2bd597dd-87da-46d1-afd6-fb9f0d3f164b"), new DateTime(2025, 7, 8, 16, 23, 34, 843, DateTimeKind.Local).AddTicks(8730), "admin@example.com", true, true, null, "$2a$11$0RPQOs.f5HRmOIS47a1Q3.dItw6WsZdD5vQPS5tv3uGxlqgdDfisO", 1, null, "admin_user" },
                    { new Guid("951ebd31-e6da-49e7-b576-8b7b23c19c3a"), new DateTime(2025, 7, 8, 16, 23, 34, 843, DateTimeKind.Local).AddTicks(8751), "staff@example.com", true, true, null, "$2a$11$0RPQOs.f5HRmOIS47a1Q3.dItw6WsZdD5vQPS5tv3uGxlqgdDfisO", 3, null, "staff_user" },
                    { new Guid("ee824d09-2c82-48f1-b491-bf38a9ac97a3"), new DateTime(2025, 7, 8, 16, 23, 34, 843, DateTimeKind.Local).AddTicks(8745), "manager@example.com", true, true, null, "$2a$11$0RPQOs.f5HRmOIS47a1Q3.dItw6WsZdD5vQPS5tv3uGxlqgdDfisO", 2, null, "manager_user" }
                });
        }
    }
}

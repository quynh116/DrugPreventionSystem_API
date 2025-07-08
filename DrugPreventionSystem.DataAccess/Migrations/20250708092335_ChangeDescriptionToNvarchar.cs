using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DrugPreventionSystem.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDescriptionToNvarchar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("365a61e7-594a-41d0-ac55-f47e805a8e8e"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("429cc640-d6df-418b-8a6d-980876270fa9"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("76e5e3d1-c625-43ef-842a-b9e9af0bcde0"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("827faccc-6774-4425-b33e-2da9bbc18544"));

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "community_programs",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "description",
                table: "community_programs",
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
                value: new DateTime(2025, 7, 7, 22, 43, 40, 410, DateTimeKind.Local).AddTicks(3232));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 7, 7, 22, 43, 40, 410, DateTimeKind.Local).AddTicks(3253));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2025, 7, 7, 22, 43, 40, 410, DateTimeKind.Local).AddTicks(3255));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 4,
                column: "created_at",
                value: new DateTime(2025, 7, 7, 22, 43, 40, 410, DateTimeKind.Local).AddTicks(3256));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 5,
                column: "created_at",
                value: new DateTime(2025, 7, 7, 22, 43, 40, 410, DateTimeKind.Local).AddTicks(3257));

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "user_id", "created_at", "email", "email_verified", "is_active", "last_login", "password_hash", "role_id", "updated_at", "username" },
                values: new object[,]
                {
                    { new Guid("365a61e7-594a-41d0-ac55-f47e805a8e8e"), new DateTime(2025, 7, 7, 22, 43, 40, 802, DateTimeKind.Local).AddTicks(1981), "admin@example.com", true, true, null, "$2a$11$nNJbE2qpk81MvMAORi15luB55.ELZ9NB3natUkLvuWSuyC0mwyCV2", 1, null, "admin_user" },
                    { new Guid("429cc640-d6df-418b-8a6d-980876270fa9"), new DateTime(2025, 7, 7, 22, 43, 40, 802, DateTimeKind.Local).AddTicks(2076), "consultant@example.com", true, true, null, "$2a$11$nNJbE2qpk81MvMAORi15luB55.ELZ9NB3natUkLvuWSuyC0mwyCV2", 4, null, "consultant_user" },
                    { new Guid("76e5e3d1-c625-43ef-842a-b9e9af0bcde0"), new DateTime(2025, 7, 7, 22, 43, 40, 802, DateTimeKind.Local).AddTicks(2072), "staff@example.com", true, true, null, "$2a$11$nNJbE2qpk81MvMAORi15luB55.ELZ9NB3natUkLvuWSuyC0mwyCV2", 3, null, "staff_user" },
                    { new Guid("827faccc-6774-4425-b33e-2da9bbc18544"), new DateTime(2025, 7, 7, 22, 43, 40, 802, DateTimeKind.Local).AddTicks(2068), "manager@example.com", true, true, null, "$2a$11$nNJbE2qpk81MvMAORi15luB55.ELZ9NB3natUkLvuWSuyC0mwyCV2", 2, null, "manager_user" }
                });
        }
    }
}

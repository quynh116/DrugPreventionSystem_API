using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DrugPreventionSystem.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddMaxParticipantsToCommunityProgram : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("10d29045-bfea-4295-bf3a-27a2c6524bcf"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("27d739e6-3a43-4bdb-b0ab-1d98dded74bc"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("84233e83-739f-4901-ad79-0ee3ac020a17"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("e29f2784-2683-44af-aab5-6a1df890a91a"));

            migrationBuilder.AddColumn<int>(
                name: "max_participants",
                table: "community_programs",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 7, 2, 22, 16, 46, 477, DateTimeKind.Local).AddTicks(6829));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 7, 2, 22, 16, 46, 477, DateTimeKind.Local).AddTicks(6853));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2025, 7, 2, 22, 16, 46, 477, DateTimeKind.Local).AddTicks(6855));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 4,
                column: "created_at",
                value: new DateTime(2025, 7, 2, 22, 16, 46, 477, DateTimeKind.Local).AddTicks(6856));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 5,
                column: "created_at",
                value: new DateTime(2025, 7, 2, 22, 16, 46, 477, DateTimeKind.Local).AddTicks(6857));

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "user_id", "created_at", "email", "email_verified", "is_active", "last_login", "password_hash", "role_id", "updated_at", "username" },
                values: new object[,]
                {
                    { new Guid("27df626c-9720-4ebb-b687-e4f3be178d76"), new DateTime(2025, 7, 2, 22, 16, 46, 891, DateTimeKind.Local).AddTicks(9400), "staff@example.com", true, true, null, "$2a$11$CSVdsYv41R0zdnEejPMvRO0m0Bq4xA42gsXQAyrmmNelvIKo299D2", 3, null, "staff_user" },
                    { new Guid("3250262c-d654-4904-9370-977e4cc9403c"), new DateTime(2025, 7, 2, 22, 16, 46, 891, DateTimeKind.Local).AddTicks(9406), "consultant@example.com", true, true, null, "$2a$11$CSVdsYv41R0zdnEejPMvRO0m0Bq4xA42gsXQAyrmmNelvIKo299D2", 4, null, "consultant_user" },
                    { new Guid("5b98f9e2-dcc4-482a-b12e-ccf88541de96"), new DateTime(2025, 7, 2, 22, 16, 46, 891, DateTimeKind.Local).AddTicks(9394), "manager@example.com", true, true, null, "$2a$11$CSVdsYv41R0zdnEejPMvRO0m0Bq4xA42gsXQAyrmmNelvIKo299D2", 2, null, "manager_user" },
                    { new Guid("d553fdc0-8794-425d-93d6-8476a90fd5bd"), new DateTime(2025, 7, 2, 22, 16, 46, 891, DateTimeKind.Local).AddTicks(9350), "admin@example.com", true, true, null, "$2a$11$CSVdsYv41R0zdnEejPMvRO0m0Bq4xA42gsXQAyrmmNelvIKo299D2", 1, null, "admin_user" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("27df626c-9720-4ebb-b687-e4f3be178d76"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("3250262c-d654-4904-9370-977e4cc9403c"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("5b98f9e2-dcc4-482a-b12e-ccf88541de96"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("d553fdc0-8794-425d-93d6-8476a90fd5bd"));

            migrationBuilder.DropColumn(
                name: "max_participants",
                table: "community_programs");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 7, 1, 23, 8, 5, 652, DateTimeKind.Local).AddTicks(8120));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 7, 1, 23, 8, 5, 652, DateTimeKind.Local).AddTicks(8156));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2025, 7, 1, 23, 8, 5, 652, DateTimeKind.Local).AddTicks(8158));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 4,
                column: "created_at",
                value: new DateTime(2025, 7, 1, 23, 8, 5, 652, DateTimeKind.Local).AddTicks(8159));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 5,
                column: "created_at",
                value: new DateTime(2025, 7, 1, 23, 8, 5, 652, DateTimeKind.Local).AddTicks(8160));

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "user_id", "created_at", "email", "email_verified", "is_active", "last_login", "password_hash", "role_id", "updated_at", "username" },
                values: new object[,]
                {
                    { new Guid("10d29045-bfea-4295-bf3a-27a2c6524bcf"), new DateTime(2025, 7, 1, 23, 8, 5, 761, DateTimeKind.Local).AddTicks(936), "consultant@example.com", true, true, null, "$2a$11$I3P03LIaVKSaA2zDcJQdY./9TLjabFpOZI3U.VVn3kx.dYPQx98Ku", 4, null, "consultant_user" },
                    { new Guid("27d739e6-3a43-4bdb-b0ab-1d98dded74bc"), new DateTime(2025, 7, 1, 23, 8, 5, 761, DateTimeKind.Local).AddTicks(920), "staff@example.com", true, true, null, "$2a$11$I3P03LIaVKSaA2zDcJQdY./9TLjabFpOZI3U.VVn3kx.dYPQx98Ku", 3, null, "staff_user" },
                    { new Guid("84233e83-739f-4901-ad79-0ee3ac020a17"), new DateTime(2025, 7, 1, 23, 8, 5, 761, DateTimeKind.Local).AddTicks(907), "admin@example.com", true, true, null, "$2a$11$I3P03LIaVKSaA2zDcJQdY./9TLjabFpOZI3U.VVn3kx.dYPQx98Ku", 1, null, "admin_user" },
                    { new Guid("e29f2784-2683-44af-aab5-6a1df890a91a"), new DateTime(2025, 7, 1, 23, 8, 5, 761, DateTimeKind.Local).AddTicks(917), "manager@example.com", true, true, null, "$2a$11$I3P03LIaVKSaA2zDcJQdY./9TLjabFpOZI3U.VVn3kx.dYPQx98Ku", 2, null, "manager_user" }
                });
        }
    }
}

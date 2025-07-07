using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DrugPreventionSystem.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddUserToBlog : Migration
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

            migrationBuilder.AddColumn<Guid>(
                name: "user_id",
                table: "blogs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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

            migrationBuilder.CreateIndex(
                name: "IX_blogs_user_id",
                table: "blogs",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_blogs_users_user_id",
                table: "blogs",
                column: "user_id",
                principalTable: "users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_blogs_users_user_id",
                table: "blogs");

            migrationBuilder.DropIndex(
                name: "IX_blogs_user_id",
                table: "blogs");

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

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "blogs");

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

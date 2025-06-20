using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DrugPreventionSystem.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddPropertyToCourseAndSurvey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("1e3e644e-0a16-4fcf-a13e-01da8af7fe2e"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("580ed3cc-9be3-4fb4-b5b6-2a6c825a2632"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("669fd5db-893c-46eb-9a18-1b8608f73d29"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("abedc5db-2621-46f0-b9af-c7e1438d0443"));

            migrationBuilder.AddColumn<string>(
                name: "estimated_duration",
                table: "surveys",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "thumbnail_url",
                table: "courses",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 6, 20, 13, 6, 0, 911, DateTimeKind.Local).AddTicks(3464));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 6, 20, 13, 6, 0, 911, DateTimeKind.Local).AddTicks(3477));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2025, 6, 20, 13, 6, 0, 911, DateTimeKind.Local).AddTicks(3479));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 4,
                column: "created_at",
                value: new DateTime(2025, 6, 20, 13, 6, 0, 911, DateTimeKind.Local).AddTicks(3480));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 5,
                column: "created_at",
                value: new DateTime(2025, 6, 20, 13, 6, 0, 911, DateTimeKind.Local).AddTicks(3482));

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "user_id", "created_at", "email", "email_verified", "is_active", "last_login", "password_hash", "role_id", "updated_at", "username" },
                values: new object[,]
                {
                    { new Guid("9dda1fc6-e4b0-4637-952e-8a6cb2ba02f0"), new DateTime(2025, 6, 20, 13, 6, 1, 49, DateTimeKind.Local).AddTicks(4262), "consultant@example.com", true, true, null, "$2a$11$QTOCZBWg12KGi8SyL0mJ.OR1sdAkGhebmCRcaoh7CHUX6gII7py6K", 4, null, "consultant_user" },
                    { new Guid("a31fda90-14e5-4f75-a18f-ceded1ac1957"), new DateTime(2025, 6, 20, 13, 6, 1, 49, DateTimeKind.Local).AddTicks(4247), "manager@example.com", true, true, null, "$2a$11$QTOCZBWg12KGi8SyL0mJ.OR1sdAkGhebmCRcaoh7CHUX6gII7py6K", 2, null, "manager_user" },
                    { new Guid("d723ae06-ca8a-4cb3-a420-5b68d0ea7a3b"), new DateTime(2025, 6, 20, 13, 6, 1, 49, DateTimeKind.Local).AddTicks(4259), "staff@example.com", true, true, null, "$2a$11$QTOCZBWg12KGi8SyL0mJ.OR1sdAkGhebmCRcaoh7CHUX6gII7py6K", 3, null, "staff_user" },
                    { new Guid("dedc4d53-6e23-42c1-b30a-9f26ef736102"), new DateTime(2025, 6, 20, 13, 6, 1, 49, DateTimeKind.Local).AddTicks(4240), "admin@example.com", true, true, null, "$2a$11$QTOCZBWg12KGi8SyL0mJ.OR1sdAkGhebmCRcaoh7CHUX6gII7py6K", 1, null, "admin_user" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("9dda1fc6-e4b0-4637-952e-8a6cb2ba02f0"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("a31fda90-14e5-4f75-a18f-ceded1ac1957"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("d723ae06-ca8a-4cb3-a420-5b68d0ea7a3b"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("dedc4d53-6e23-42c1-b30a-9f26ef736102"));

            migrationBuilder.DropColumn(
                name: "estimated_duration",
                table: "surveys");

            migrationBuilder.DropColumn(
                name: "thumbnail_url",
                table: "courses");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 6, 18, 19, 27, 18, 701, DateTimeKind.Local).AddTicks(3986));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 6, 18, 19, 27, 18, 701, DateTimeKind.Local).AddTicks(4000));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2025, 6, 18, 19, 27, 18, 701, DateTimeKind.Local).AddTicks(4001));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 4,
                column: "created_at",
                value: new DateTime(2025, 6, 18, 19, 27, 18, 701, DateTimeKind.Local).AddTicks(4003));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 5,
                column: "created_at",
                value: new DateTime(2025, 6, 18, 19, 27, 18, 701, DateTimeKind.Local).AddTicks(4004));

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "user_id", "created_at", "email", "email_verified", "is_active", "last_login", "password_hash", "role_id", "updated_at", "username" },
                values: new object[,]
                {
                    { new Guid("1e3e644e-0a16-4fcf-a13e-01da8af7fe2e"), new DateTime(2025, 6, 18, 19, 27, 18, 833, DateTimeKind.Local).AddTicks(641), "staff@example.com", true, true, null, "$2a$11$T2sZUHavZAhym.mOqYnSxObC7XC4mVVLwlEjwgeLdULx0PPBB42Y6", 3, null, "staff_user" },
                    { new Guid("580ed3cc-9be3-4fb4-b5b6-2a6c825a2632"), new DateTime(2025, 6, 18, 19, 27, 18, 833, DateTimeKind.Local).AddTicks(645), "consultant@example.com", true, true, null, "$2a$11$T2sZUHavZAhym.mOqYnSxObC7XC4mVVLwlEjwgeLdULx0PPBB42Y6", 4, null, "consultant_user" },
                    { new Guid("669fd5db-893c-46eb-9a18-1b8608f73d29"), new DateTime(2025, 6, 18, 19, 27, 18, 833, DateTimeKind.Local).AddTicks(632), "admin@example.com", true, true, null, "$2a$11$T2sZUHavZAhym.mOqYnSxObC7XC4mVVLwlEjwgeLdULx0PPBB42Y6", 1, null, "admin_user" },
                    { new Guid("abedc5db-2621-46f0-b9af-c7e1438d0443"), new DateTime(2025, 6, 18, 19, 27, 18, 833, DateTimeKind.Local).AddTicks(638), "manager@example.com", true, true, null, "$2a$11$T2sZUHavZAhym.mOqYnSxObC7XC4mVVLwlEjwgeLdULx0PPBB42Y6", 2, null, "manager_user" }
                });
        }
    }
}

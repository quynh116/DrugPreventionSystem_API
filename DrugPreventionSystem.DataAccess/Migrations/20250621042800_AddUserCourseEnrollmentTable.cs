using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DrugPreventionSystem.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddUserCourseEnrollmentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "user_course_enrollments",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    course_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    enrolled_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    completed_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_course_enrollments", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_course_enrollments_courses_course_id",
                        column: x => x.course_id,
                        principalTable: "courses",
                        principalColumn: "course_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_user_course_enrollments_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 6, 21, 11, 27, 58, 838, DateTimeKind.Local).AddTicks(4318));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 6, 21, 11, 27, 58, 838, DateTimeKind.Local).AddTicks(4341));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2025, 6, 21, 11, 27, 58, 838, DateTimeKind.Local).AddTicks(4344));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 4,
                column: "created_at",
                value: new DateTime(2025, 6, 21, 11, 27, 58, 838, DateTimeKind.Local).AddTicks(4430));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 5,
                column: "created_at",
                value: new DateTime(2025, 6, 21, 11, 27, 58, 838, DateTimeKind.Local).AddTicks(4433));

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "user_id", "created_at", "email", "email_verified", "is_active", "last_login", "password_hash", "role_id", "updated_at", "username" },
                values: new object[,]
                {
                    { new Guid("12813d0e-e931-43f7-a254-56a66463c628"), new DateTime(2025, 6, 21, 11, 27, 59, 255, DateTimeKind.Local).AddTicks(6407), "admin@example.com", true, true, null, "$2a$11$bfEzuHPU2EImIL0wtiklg.bAz1xw/CWu.BPwWnyeO.NqIVZTHOpWy", 1, null, "admin_user" },
                    { new Guid("29e967c9-01cb-422a-ba62-9f77fec15b79"), new DateTime(2025, 6, 21, 11, 27, 59, 255, DateTimeKind.Local).AddTicks(6437), "staff@example.com", true, true, null, "$2a$11$bfEzuHPU2EImIL0wtiklg.bAz1xw/CWu.BPwWnyeO.NqIVZTHOpWy", 3, null, "staff_user" },
                    { new Guid("736133db-9061-4576-b4cc-9a93cc237c91"), new DateTime(2025, 6, 21, 11, 27, 59, 255, DateTimeKind.Local).AddTicks(6427), "manager@example.com", true, true, null, "$2a$11$bfEzuHPU2EImIL0wtiklg.bAz1xw/CWu.BPwWnyeO.NqIVZTHOpWy", 2, null, "manager_user" },
                    { new Guid("f45d32b3-1c1b-4193-9683-279a82d96a9f"), new DateTime(2025, 6, 21, 11, 27, 59, 255, DateTimeKind.Local).AddTicks(6446), "consultant@example.com", true, true, null, "$2a$11$bfEzuHPU2EImIL0wtiklg.bAz1xw/CWu.BPwWnyeO.NqIVZTHOpWy", 4, null, "consultant_user" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_user_course_enrollments_course_id",
                table: "user_course_enrollments",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_course_enrollments_user_id_course_id",
                table: "user_course_enrollments",
                columns: new[] { "user_id", "course_id" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_course_enrollments");

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("12813d0e-e931-43f7-a254-56a66463c628"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("29e967c9-01cb-422a-ba62-9f77fec15b79"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("736133db-9061-4576-b4cc-9a93cc237c91"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("f45d32b3-1c1b-4193-9683-279a82d96a9f"));

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
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DrugPreventionSystem.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddProgramTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "community_programs",
                columns: table => new
                {
                    program_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    target_audience = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    start_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    end_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    location = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_community_programs", x => x.program_id);
                });

            migrationBuilder.CreateTable(
                name: "program_feedback",
                columns: table => new
                {
                    feedback_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    program_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    rating = table.Column<int>(type: "int", nullable: false),
                    comments = table.Column<string>(type: "text", nullable: true),
                    submitted_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_program_feedback", x => x.feedback_id);
                    table.ForeignKey(
                        name: "FK_program_feedback_community_programs_program_id",
                        column: x => x.program_id,
                        principalTable: "community_programs",
                        principalColumn: "program_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_program_feedback_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "program_participants",
                columns: table => new
                {
                    participant_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    program_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    registered_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    attended = table.Column<bool>(type: "bit", nullable: false),
                    feedback_submitted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_program_participants", x => x.participant_id);
                    table.ForeignKey(
                        name: "FK_program_participants_community_programs_program_id",
                        column: x => x.program_id,
                        principalTable: "community_programs",
                        principalColumn: "program_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_program_participants_users_user_id",
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

            migrationBuilder.CreateIndex(
                name: "IX_program_feedback_program_id",
                table: "program_feedback",
                column: "program_id");

            migrationBuilder.CreateIndex(
                name: "IX_program_feedback_user_id",
                table: "program_feedback",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_program_participants_program_id",
                table: "program_participants",
                column: "program_id");

            migrationBuilder.CreateIndex(
                name: "IX_program_participants_user_id",
                table: "program_participants",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "program_feedback");

            migrationBuilder.DropTable(
                name: "program_participants");

            migrationBuilder.DropTable(
                name: "community_programs");

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
        }
    }
}

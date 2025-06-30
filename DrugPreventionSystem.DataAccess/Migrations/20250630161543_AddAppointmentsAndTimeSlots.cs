using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DrugPreventionSystem.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddAppointmentsAndTimeSlots : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "time_slots",
                columns: table => new
                {
                    time_slot_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    consultant_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    slot_date = table.Column<DateTime>(type: "date", nullable: false),
                    start_time = table.Column<TimeSpan>(type: "time", nullable: false),
                    end_time = table.Column<TimeSpan>(type: "time", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_time_slots", x => x.time_slot_id);
                    table.ForeignKey(
                        name: "FK_time_slots_consultants_consultant_id",
                        column: x => x.consultant_id,
                        principalTable: "consultants",
                        principalColumn: "consultant_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "appointments",
                columns: table => new
                {
                    appointment_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    consultant_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    time_slot_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    reason_for_consultation = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    consultation_type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    has_previous_consultation = table.Column<bool>(type: "bit", nullable: false),
                    status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    notes = table.Column<string>(type: "text", nullable: true),
                    meet_url = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_appointments", x => x.appointment_id);
                    table.ForeignKey(
                        name: "FK_appointments_consultants_consultant_id",
                        column: x => x.consultant_id,
                        principalTable: "consultants",
                        principalColumn: "consultant_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_appointments_time_slots_time_slot_id",
                        column: x => x.time_slot_id,
                        principalTable: "time_slots",
                        principalColumn: "time_slot_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_appointments_users_user_id",
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
                value: new DateTime(2025, 6, 30, 23, 15, 41, 237, DateTimeKind.Local).AddTicks(2815));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 6, 30, 23, 15, 41, 237, DateTimeKind.Local).AddTicks(2833));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2025, 6, 30, 23, 15, 41, 237, DateTimeKind.Local).AddTicks(2834));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 4,
                column: "created_at",
                value: new DateTime(2025, 6, 30, 23, 15, 41, 237, DateTimeKind.Local).AddTicks(2834));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 5,
                column: "created_at",
                value: new DateTime(2025, 6, 30, 23, 15, 41, 237, DateTimeKind.Local).AddTicks(2835));

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "user_id", "created_at", "email", "email_verified", "is_active", "last_login", "password_hash", "role_id", "updated_at", "username" },
                values: new object[,]
                {
                    { new Guid("74efb02d-98e9-439b-a4af-4a4c0318db20"), new DateTime(2025, 6, 30, 23, 15, 41, 424, DateTimeKind.Local).AddTicks(8201), "staff@example.com", true, true, null, "$2a$11$q4dsqqEp2OgFtMunWpt6B.wIVTt8MBWvCaTU8CSjBO91Tmwug0H6e", 3, null, "staff_user" },
                    { new Guid("89fdb205-8e45-44c3-970e-80b1726eedf0"), new DateTime(2025, 6, 30, 23, 15, 41, 424, DateTimeKind.Local).AddTicks(8182), "admin@example.com", true, true, null, "$2a$11$q4dsqqEp2OgFtMunWpt6B.wIVTt8MBWvCaTU8CSjBO91Tmwug0H6e", 1, null, "admin_user" },
                    { new Guid("de00b292-5014-442f-85c2-73bef7c991ad"), new DateTime(2025, 6, 30, 23, 15, 41, 424, DateTimeKind.Local).AddTicks(8194), "manager@example.com", true, true, null, "$2a$11$q4dsqqEp2OgFtMunWpt6B.wIVTt8MBWvCaTU8CSjBO91Tmwug0H6e", 2, null, "manager_user" },
                    { new Guid("f6ed0576-ab74-4fca-bb8d-2af1816d6821"), new DateTime(2025, 6, 30, 23, 15, 41, 424, DateTimeKind.Local).AddTicks(8363), "consultant@example.com", true, true, null, "$2a$11$q4dsqqEp2OgFtMunWpt6B.wIVTt8MBWvCaTU8CSjBO91Tmwug0H6e", 4, null, "consultant_user" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_appointments_consultant_id",
                table: "appointments",
                column: "consultant_id");

            migrationBuilder.CreateIndex(
                name: "IX_appointments_time_slot_id",
                table: "appointments",
                column: "time_slot_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_appointments_user_id",
                table: "appointments",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_time_slots_consultant_id_slot_date_start_time",
                table: "time_slots",
                columns: new[] { "consultant_id", "slot_date", "start_time" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "appointments");

            migrationBuilder.DropTable(
                name: "time_slots");

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("74efb02d-98e9-439b-a4af-4a4c0318db20"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("89fdb205-8e45-44c3-970e-80b1726eedf0"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("de00b292-5014-442f-85c2-73bef7c991ad"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("f6ed0576-ab74-4fca-bb8d-2af1816d6821"));

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
    }
}

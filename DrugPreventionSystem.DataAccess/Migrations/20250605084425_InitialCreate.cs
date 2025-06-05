using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DrugPreventionSystem.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    role_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    role_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.role_id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    password_hash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    role_id = table.Column<int>(type: "int", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    email_verified = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    last_login = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.user_id);
                    table.ForeignKey(
                        name: "FK_users_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "role_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "consultants",
                columns: table => new
                {
                    consultant_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    license_number = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    specialization = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    years_of_experience = table.Column<int>(type: "int", nullable: true),
                    qualifications = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    bio = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    consultation_fee = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    is_available = table.Column<bool>(type: "bit", nullable: false),
                    working_hours = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    rating = table.Column<decimal>(type: "decimal(3,2)", nullable: true),
                    total_consultations = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_consultants", x => x.consultant_id);
                    table.ForeignKey(
                        name: "FK_consultants_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_profiles",
                columns: table => new
                {
                    profile_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    first_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    last_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    date_of_birth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    phone_number = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    city = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    occupation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    education_level = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    age_group = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    avatar_url = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_profiles", x => x.profile_id);
                    table.ForeignKey(
                        name: "FK_user_profiles_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "role_id", "created_at", "description", "is_active", "role_name", "updated_at" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 6, 5, 15, 44, 24, 416, DateTimeKind.Local).AddTicks(8457), "System Administrator", true, "Admin", null },
                    { 2, new DateTime(2025, 6, 5, 15, 44, 24, 416, DateTimeKind.Local).AddTicks(8470), "System Manager", true, "Manager", null },
                    { 3, new DateTime(2025, 6, 5, 15, 44, 24, 416, DateTimeKind.Local).AddTicks(8472), "Staff Member", true, "Staff", null },
                    { 4, new DateTime(2025, 6, 5, 15, 44, 24, 416, DateTimeKind.Local).AddTicks(8473), "Professional Consultant", true, "Consultant", null },
                    { 5, new DateTime(2025, 6, 5, 15, 44, 24, 416, DateTimeKind.Local).AddTicks(8474), "Registered Member", true, "Member", null }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "user_id", "created_at", "email", "email_verified", "is_active", "last_login", "password_hash", "role_id", "updated_at", "username" },
                values: new object[,]
                {
                    { new Guid("697a6963-2641-46de-a266-b46c685cf8ba"), new DateTime(2025, 6, 5, 15, 44, 24, 553, DateTimeKind.Local).AddTicks(3957), "admin@example.com", true, true, null, "$2a$11$o19Ma.8k2U6PEXAXMnKUNeHxmzkPRl39YMVNmyDo5SMnNINW4J57.", 1, null, "admin_user" },
                    { new Guid("7385b13a-ccff-4213-92e6-0f20421c95c4"), new DateTime(2025, 6, 5, 15, 44, 24, 553, DateTimeKind.Local).AddTicks(3964), "staff@example.com", true, true, null, "$2a$11$o19Ma.8k2U6PEXAXMnKUNeHxmzkPRl39YMVNmyDo5SMnNINW4J57.", 3, null, "staff_user" },
                    { new Guid("b5b82f90-6d92-45ea-804e-ab55ec5395e8"), new DateTime(2025, 6, 5, 15, 44, 24, 553, DateTimeKind.Local).AddTicks(3966), "consultant@example.com", true, true, null, "$2a$11$o19Ma.8k2U6PEXAXMnKUNeHxmzkPRl39YMVNmyDo5SMnNINW4J57.", 4, null, "consultant_user" },
                    { new Guid("dfe5c967-ed12-4f41-9f3a-ef3d9db32f80"), new DateTime(2025, 6, 5, 15, 44, 24, 553, DateTimeKind.Local).AddTicks(3961), "manager@example.com", true, true, null, "$2a$11$o19Ma.8k2U6PEXAXMnKUNeHxmzkPRl39YMVNmyDo5SMnNINW4J57.", 2, null, "manager_user" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_consultants_user_id",
                table: "consultants",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_profiles_user_id",
                table: "user_profiles",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_email",
                table: "users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_role_id",
                table: "users",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_username",
                table: "users",
                column: "username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "consultants");

            migrationBuilder.DropTable(
                name: "user_profiles");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "roles");
        }
    }
}

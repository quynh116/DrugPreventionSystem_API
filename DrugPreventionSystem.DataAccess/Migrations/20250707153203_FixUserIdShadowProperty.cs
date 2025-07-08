using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DrugPreventionSystem.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FixUserIdShadowProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_program_survey_responses_users_UserId1",
                table: "program_survey_responses");

            migrationBuilder.DropForeignKey(
                name: "FK_program_survey_responses_users_user_id",
                table: "program_survey_responses");

            migrationBuilder.DropIndex(
                name: "IX_program_survey_responses_UserId1",
                table: "program_survey_responses");

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("22d87189-c9bf-47d0-83c6-85e48170c8e9"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("4db4493d-b742-47b7-809c-13402b1c96aa"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("8094ff79-029f-45e0-9c5f-b7807ab1a744"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("ac31395f-89b4-4754-865a-c4176bb0c0c9"));

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "program_survey_responses");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 7, 7, 22, 32, 1, 809, DateTimeKind.Local).AddTicks(3976));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 7, 7, 22, 32, 1, 809, DateTimeKind.Local).AddTicks(4031));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2025, 7, 7, 22, 32, 1, 809, DateTimeKind.Local).AddTicks(4033));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 4,
                column: "created_at",
                value: new DateTime(2025, 7, 7, 22, 32, 1, 809, DateTimeKind.Local).AddTicks(4034));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 5,
                column: "created_at",
                value: new DateTime(2025, 7, 7, 22, 32, 1, 809, DateTimeKind.Local).AddTicks(4035));

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "user_id", "created_at", "email", "email_verified", "is_active", "last_login", "password_hash", "role_id", "updated_at", "username" },
                values: new object[,]
                {
                    { new Guid("148fe853-e52a-4461-ad26-3129f59668ba"), new DateTime(2025, 7, 7, 22, 32, 1, 969, DateTimeKind.Local).AddTicks(2475), "admin@example.com", true, true, null, "$2a$11$CbncEfTSpvr8S4wwUEhDN.PJyZXoATys0ZDOef7qFEVjUqEHcr8A.", 1, null, "admin_user" },
                    { new Guid("96fa3324-4f1b-4aeb-a2da-c7ae4a2abd4c"), new DateTime(2025, 7, 7, 22, 32, 1, 969, DateTimeKind.Local).AddTicks(2487), "manager@example.com", true, true, null, "$2a$11$CbncEfTSpvr8S4wwUEhDN.PJyZXoATys0ZDOef7qFEVjUqEHcr8A.", 2, null, "manager_user" },
                    { new Guid("b1bf71e3-09c4-4668-81db-7f68d7844c22"), new DateTime(2025, 7, 7, 22, 32, 1, 969, DateTimeKind.Local).AddTicks(2494), "staff@example.com", true, true, null, "$2a$11$CbncEfTSpvr8S4wwUEhDN.PJyZXoATys0ZDOef7qFEVjUqEHcr8A.", 3, null, "staff_user" },
                    { new Guid("d008a2ce-9696-4147-b80f-ec74fb32f7d2"), new DateTime(2025, 7, 7, 22, 32, 1, 969, DateTimeKind.Local).AddTicks(2499), "consultant@example.com", true, true, null, "$2a$11$CbncEfTSpvr8S4wwUEhDN.PJyZXoATys0ZDOef7qFEVjUqEHcr8A.", 4, null, "consultant_user" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_program_survey_responses_users_user_id",
                table: "program_survey_responses",
                column: "user_id",
                principalTable: "users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_program_survey_responses_users_user_id",
                table: "program_survey_responses");

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("148fe853-e52a-4461-ad26-3129f59668ba"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("96fa3324-4f1b-4aeb-a2da-c7ae4a2abd4c"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("b1bf71e3-09c4-4668-81db-7f68d7844c22"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("d008a2ce-9696-4147-b80f-ec74fb32f7d2"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "program_survey_responses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 7, 7, 22, 13, 43, 320, DateTimeKind.Local).AddTicks(4538));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 7, 7, 22, 13, 43, 320, DateTimeKind.Local).AddTicks(4627));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2025, 7, 7, 22, 13, 43, 320, DateTimeKind.Local).AddTicks(4631));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 4,
                column: "created_at",
                value: new DateTime(2025, 7, 7, 22, 13, 43, 320, DateTimeKind.Local).AddTicks(4634));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 5,
                column: "created_at",
                value: new DateTime(2025, 7, 7, 22, 13, 43, 320, DateTimeKind.Local).AddTicks(4636));

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "user_id", "created_at", "email", "email_verified", "is_active", "last_login", "password_hash", "role_id", "updated_at", "username" },
                values: new object[,]
                {
                    { new Guid("22d87189-c9bf-47d0-83c6-85e48170c8e9"), new DateTime(2025, 7, 7, 22, 13, 43, 688, DateTimeKind.Local).AddTicks(9894), "manager@example.com", true, true, null, "$2a$11$713sEeYH5F.8lIS/aJsl2ewpp/wnahfgQZVGb4R7jArt1vO9C/tAu", 2, null, "manager_user" },
                    { new Guid("4db4493d-b742-47b7-809c-13402b1c96aa"), new DateTime(2025, 7, 7, 22, 13, 43, 688, DateTimeKind.Local).AddTicks(9909), "staff@example.com", true, true, null, "$2a$11$713sEeYH5F.8lIS/aJsl2ewpp/wnahfgQZVGb4R7jArt1vO9C/tAu", 3, null, "staff_user" },
                    { new Guid("8094ff79-029f-45e0-9c5f-b7807ab1a744"), new DateTime(2025, 7, 7, 22, 13, 43, 688, DateTimeKind.Local).AddTicks(9887), "admin@example.com", true, true, null, "$2a$11$713sEeYH5F.8lIS/aJsl2ewpp/wnahfgQZVGb4R7jArt1vO9C/tAu", 1, null, "admin_user" },
                    { new Guid("ac31395f-89b4-4754-865a-c4176bb0c0c9"), new DateTime(2025, 7, 7, 22, 13, 43, 688, DateTimeKind.Local).AddTicks(9913), "consultant@example.com", true, true, null, "$2a$11$713sEeYH5F.8lIS/aJsl2ewpp/wnahfgQZVGb4R7jArt1vO9C/tAu", 4, null, "consultant_user" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_program_survey_responses_UserId1",
                table: "program_survey_responses",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_program_survey_responses_users_UserId1",
                table: "program_survey_responses",
                column: "UserId1",
                principalTable: "users",
                principalColumn: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_program_survey_responses_users_user_id",
                table: "program_survey_responses",
                column: "user_id",
                principalTable: "users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

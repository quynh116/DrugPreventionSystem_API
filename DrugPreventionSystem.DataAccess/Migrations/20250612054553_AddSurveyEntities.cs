using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DrugPreventionSystem.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddSurveyEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("697a6963-2641-46de-a266-b46c685cf8ba"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("7385b13a-ccff-4213-92e6-0f20421c95c4"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("b5b82f90-6d92-45ea-804e-ab55ec5395e8"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("dfe5c967-ed12-4f41-9f3a-ef3d9db32f80"));

            migrationBuilder.CreateTable(
                name: "surveys",
                columns: table => new
                {
                    survey_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_surveys", x => x.survey_id);
                });

            migrationBuilder.CreateTable(
                name: "survey_questions",
                columns: table => new
                {
                    question_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    survey_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    question_text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    question_type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    sequence = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_survey_questions", x => x.question_id);
                    table.ForeignKey(
                        name: "FK_survey_questions_surveys_survey_id",
                        column: x => x.survey_id,
                        principalTable: "surveys",
                        principalColumn: "survey_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_survey_responses",
                columns: table => new
                {
                    response_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    survey_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    taken_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    risk_level = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    recommended_action = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_survey_responses", x => x.response_id);
                    table.ForeignKey(
                        name: "FK_user_survey_responses_surveys_survey_id",
                        column: x => x.survey_id,
                        principalTable: "surveys",
                        principalColumn: "survey_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_survey_responses_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SurveyOptions",
                columns: table => new
                {
                    option_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    question_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    option_text = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    score_value = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyOptions", x => x.option_id);
                    table.ForeignKey(
                        name: "FK_SurveyOptions_survey_questions_question_id",
                        column: x => x.question_id,
                        principalTable: "survey_questions",
                        principalColumn: "question_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_survey_answers",
                columns: table => new
                {
                    answer_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    response_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    question_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    option_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    answer_text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    answered_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_survey_answers", x => x.answer_id);
                    table.ForeignKey(
                        name: "FK_user_survey_answers_SurveyOptions_option_id",
                        column: x => x.option_id,
                        principalTable: "SurveyOptions",
                        principalColumn: "option_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_user_survey_answers_survey_questions_question_id",
                        column: x => x.question_id,
                        principalTable: "survey_questions",
                        principalColumn: "question_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_user_survey_answers_user_survey_responses_response_id",
                        column: x => x.response_id,
                        principalTable: "user_survey_responses",
                        principalColumn: "response_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 6, 12, 12, 45, 52, 364, DateTimeKind.Local).AddTicks(5990));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 6, 12, 12, 45, 52, 364, DateTimeKind.Local).AddTicks(6003));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2025, 6, 12, 12, 45, 52, 364, DateTimeKind.Local).AddTicks(6005));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 4,
                column: "created_at",
                value: new DateTime(2025, 6, 12, 12, 45, 52, 364, DateTimeKind.Local).AddTicks(6006));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 5,
                column: "created_at",
                value: new DateTime(2025, 6, 12, 12, 45, 52, 364, DateTimeKind.Local).AddTicks(6007));

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "user_id", "created_at", "email", "email_verified", "is_active", "last_login", "password_hash", "role_id", "updated_at", "username" },
                values: new object[,]
                {
                    { new Guid("1cd03061-7c79-45aa-9538-a55c99c1cd1d"), new DateTime(2025, 6, 12, 12, 45, 52, 514, DateTimeKind.Local).AddTicks(2808), "manager@example.com", true, true, null, "$2a$11$y9Y6Zpx5AbJ0psfr/rb4fuyRuEtNDk8DF/ovzq1O.fYPap/HS8B/K", 2, null, "manager_user" },
                    { new Guid("88dfbcab-e0d0-4ae1-9a01-8a5069e04b07"), new DateTime(2025, 6, 12, 12, 45, 52, 514, DateTimeKind.Local).AddTicks(2811), "staff@example.com", true, true, null, "$2a$11$y9Y6Zpx5AbJ0psfr/rb4fuyRuEtNDk8DF/ovzq1O.fYPap/HS8B/K", 3, null, "staff_user" },
                    { new Guid("9bc77185-699b-475a-9cc3-a806d7c4bfd7"), new DateTime(2025, 6, 12, 12, 45, 52, 514, DateTimeKind.Local).AddTicks(2814), "consultant@example.com", true, true, null, "$2a$11$y9Y6Zpx5AbJ0psfr/rb4fuyRuEtNDk8DF/ovzq1O.fYPap/HS8B/K", 4, null, "consultant_user" },
                    { new Guid("c58f63b3-6b28-4b8b-a599-8482aeada58b"), new DateTime(2025, 6, 12, 12, 45, 52, 514, DateTimeKind.Local).AddTicks(2803), "admin@example.com", true, true, null, "$2a$11$y9Y6Zpx5AbJ0psfr/rb4fuyRuEtNDk8DF/ovzq1O.fYPap/HS8B/K", 1, null, "admin_user" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_survey_questions_survey_id",
                table: "survey_questions",
                column: "survey_id");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyOptions_question_id",
                table: "SurveyOptions",
                column: "question_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_survey_answers_option_id",
                table: "user_survey_answers",
                column: "option_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_survey_answers_question_id",
                table: "user_survey_answers",
                column: "question_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_survey_answers_response_id",
                table: "user_survey_answers",
                column: "response_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_survey_responses_survey_id",
                table: "user_survey_responses",
                column: "survey_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_survey_responses_user_id",
                table: "user_survey_responses",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_survey_answers");

            migrationBuilder.DropTable(
                name: "SurveyOptions");

            migrationBuilder.DropTable(
                name: "user_survey_responses");

            migrationBuilder.DropTable(
                name: "survey_questions");

            migrationBuilder.DropTable(
                name: "surveys");

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("1cd03061-7c79-45aa-9538-a55c99c1cd1d"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("88dfbcab-e0d0-4ae1-9a01-8a5069e04b07"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("9bc77185-699b-475a-9cc3-a806d7c4bfd7"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("c58f63b3-6b28-4b8b-a599-8482aeada58b"));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 6, 5, 15, 44, 24, 416, DateTimeKind.Local).AddTicks(8457));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 6, 5, 15, 44, 24, 416, DateTimeKind.Local).AddTicks(8470));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2025, 6, 5, 15, 44, 24, 416, DateTimeKind.Local).AddTicks(8472));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 4,
                column: "created_at",
                value: new DateTime(2025, 6, 5, 15, 44, 24, 416, DateTimeKind.Local).AddTicks(8473));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 5,
                column: "created_at",
                value: new DateTime(2025, 6, 5, 15, 44, 24, 416, DateTimeKind.Local).AddTicks(8474));

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
        }
    }
}

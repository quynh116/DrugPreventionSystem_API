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

            migrationBuilder.AddColumn<Guid>(
                name: "survey_id",
                table: "community_programs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "program_surveys",
                columns: table => new
                {
                    survey_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_program_surveys", x => x.survey_id);
                });

            migrationBuilder.CreateTable(
                name: "program_survey_questions",
                columns: table => new
                {
                    question_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    survey_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    question_text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    question_type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_program_survey_questions", x => x.question_id);
                    table.ForeignKey(
                        name: "FK_program_survey_questions_program_surveys_survey_id",
                        column: x => x.survey_id,
                        principalTable: "program_surveys",
                        principalColumn: "survey_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "program_survey_responses",
                columns: table => new
                {
                    response_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    survey_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    program_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    submitted_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_program_survey_responses", x => x.response_id);
                    table.ForeignKey(
                        name: "FK_program_survey_responses_community_programs_program_id",
                        column: x => x.program_id,
                        principalTable: "community_programs",
                        principalColumn: "program_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_program_survey_responses_program_surveys_survey_id",
                        column: x => x.survey_id,
                        principalTable: "program_surveys",
                        principalColumn: "survey_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_program_survey_responses_users_UserId1",
                        column: x => x.UserId1,
                        principalTable: "users",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "FK_program_survey_responses_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "program_survey_answer_options",
                columns: table => new
                {
                    option_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    question_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    option_text = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_program_survey_answer_options", x => x.option_id);
                    table.ForeignKey(
                        name: "FK_program_survey_answer_options_program_survey_questions_question_id",
                        column: x => x.question_id,
                        principalTable: "program_survey_questions",
                        principalColumn: "question_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "program_survey_answers",
                columns: table => new
                {
                    answer_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    response_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    question_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    answer_text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    selected_option_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_program_survey_answers", x => x.answer_id);
                    table.ForeignKey(
                        name: "FK_program_survey_answers_program_survey_answer_options_selected_option_id",
                        column: x => x.selected_option_id,
                        principalTable: "program_survey_answer_options",
                        principalColumn: "option_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_program_survey_answers_program_survey_questions_question_id",
                        column: x => x.question_id,
                        principalTable: "program_survey_questions",
                        principalColumn: "question_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_program_survey_answers_program_survey_responses_response_id",
                        column: x => x.response_id,
                        principalTable: "program_survey_responses",
                        principalColumn: "response_id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                name: "IX_community_programs_survey_id",
                table: "community_programs",
                column: "survey_id");

            migrationBuilder.CreateIndex(
                name: "IX_program_survey_answer_options_question_id",
                table: "program_survey_answer_options",
                column: "question_id");

            migrationBuilder.CreateIndex(
                name: "IX_program_survey_answers_question_id",
                table: "program_survey_answers",
                column: "question_id");

            migrationBuilder.CreateIndex(
                name: "IX_program_survey_answers_response_id",
                table: "program_survey_answers",
                column: "response_id");

            migrationBuilder.CreateIndex(
                name: "IX_program_survey_answers_selected_option_id",
                table: "program_survey_answers",
                column: "selected_option_id");

            migrationBuilder.CreateIndex(
                name: "IX_program_survey_questions_survey_id",
                table: "program_survey_questions",
                column: "survey_id");

            migrationBuilder.CreateIndex(
                name: "IX_program_survey_responses_program_id",
                table: "program_survey_responses",
                column: "program_id");

            migrationBuilder.CreateIndex(
                name: "IX_program_survey_responses_survey_id",
                table: "program_survey_responses",
                column: "survey_id");

            migrationBuilder.CreateIndex(
                name: "IX_program_survey_responses_user_id",
                table: "program_survey_responses",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_program_survey_responses_UserId1",
                table: "program_survey_responses",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_community_programs_program_surveys_survey_id",
                table: "community_programs",
                column: "survey_id",
                principalTable: "program_surveys",
                principalColumn: "survey_id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_community_programs_program_surveys_survey_id",
                table: "community_programs");

            migrationBuilder.DropTable(
                name: "program_survey_answers");

            migrationBuilder.DropTable(
                name: "program_survey_answer_options");

            migrationBuilder.DropTable(
                name: "program_survey_responses");

            migrationBuilder.DropTable(
                name: "program_survey_questions");

            migrationBuilder.DropTable(
                name: "program_surveys");

            migrationBuilder.DropIndex(
                name: "IX_community_programs_survey_id",
                table: "community_programs");

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
                name: "survey_id",
                table: "community_programs");

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
    }
}

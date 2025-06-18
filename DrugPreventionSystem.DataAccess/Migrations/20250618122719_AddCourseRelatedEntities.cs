using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DrugPreventionSystem.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddCourseRelatedEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<string>(
                name: "target_audience",
                table: "surveys",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "instructors",
                columns: table => new
                {
                    instructor_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    full_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    bio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    profile_image_url = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    experience_years = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_instructors", x => x.instructor_id);
                });

            migrationBuilder.CreateTable(
                name: "courses",
                columns: table => new
                {
                    course_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    title = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    age_group = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    total_duration = table.Column<int>(type: "int", nullable: true),
                    lesson_count = table.Column<int>(type: "int", nullable: true),
                    student_count = table.Column<int>(type: "int", nullable: true),
                    instructor_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    requirements = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    certificate_available = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_courses", x => x.course_id);
                    table.ForeignKey(
                        name: "FK_courses_instructors_instructor_id",
                        column: x => x.instructor_id,
                        principalTable: "instructors",
                        principalColumn: "instructor_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "course_certificates",
                columns: table => new
                {
                    certificate_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    course_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    issued_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    certificate_url = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_course_certificates", x => x.certificate_id);
                    table.ForeignKey(
                        name: "FK_course_certificates_courses_course_id",
                        column: x => x.course_id,
                        principalTable: "courses",
                        principalColumn: "course_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_course_certificates_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "course_weeks",
                columns: table => new
                {
                    week_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    course_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    week_number = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_course_weeks", x => x.week_id);
                    table.ForeignKey(
                        name: "FK_course_weeks_courses_course_id",
                        column: x => x.course_id,
                        principalTable: "courses",
                        principalColumn: "course_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "survey_course_recommendations",
                columns: table => new
                {
                    recommendation_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    survey_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    risk_level = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    recommended_action_keyword = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    course_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    priority = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_survey_course_recommendations", x => x.recommendation_id);
                    table.ForeignKey(
                        name: "FK_survey_course_recommendations_courses_course_id",
                        column: x => x.course_id,
                        principalTable: "courses",
                        principalColumn: "course_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_survey_course_recommendations_surveys_survey_id",
                        column: x => x.survey_id,
                        principalTable: "surveys",
                        principalColumn: "survey_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_response_course_recommendations",
                columns: table => new
                {
                    user_rec_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    response_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    course_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    recommended_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_response_course_recommendations", x => x.user_rec_id);
                    table.ForeignKey(
                        name: "FK_user_response_course_recommendations_courses_course_id",
                        column: x => x.course_id,
                        principalTable: "courses",
                        principalColumn: "course_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_user_response_course_recommendations_user_survey_responses_response_id",
                        column: x => x.response_id,
                        principalTable: "user_survey_responses",
                        principalColumn: "response_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "lessons",
                columns: table => new
                {
                    lesson_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    week_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    duration_minutes = table.Column<int>(type: "int", nullable: true),
                    sequence = table.Column<int>(type: "int", nullable: false),
                    has_quiz = table.Column<bool>(type: "bit", nullable: false),
                    has_practice = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lessons", x => x.lesson_id);
                    table.ForeignKey(
                        name: "FK_lessons_course_weeks_week_id",
                        column: x => x.week_id,
                        principalTable: "course_weeks",
                        principalColumn: "week_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "lesson_resources",
                columns: table => new
                {
                    resource_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    lesson_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    resource_type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    resource_url = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lesson_resources", x => x.resource_id);
                    table.ForeignKey(
                        name: "FK_lesson_resources_lessons_lesson_id",
                        column: x => x.lesson_id,
                        principalTable: "lessons",
                        principalColumn: "lesson_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "practice_exercises",
                columns: table => new
                {
                    exercise_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    lesson_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    instruction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    attachment_url = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_practice_exercises", x => x.exercise_id);
                    table.ForeignKey(
                        name: "FK_practice_exercises_lessons_lesson_id",
                        column: x => x.lesson_id,
                        principalTable: "lessons",
                        principalColumn: "lesson_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "quizzes",
                columns: table => new
                {
                    quiz_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    lesson_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    total_questions = table.Column<int>(type: "int", nullable: true),
                    passing_score = table.Column<float>(type: "real", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_quizzes", x => x.quiz_id);
                    table.ForeignKey(
                        name: "FK_quizzes_lessons_lesson_id",
                        column: x => x.lesson_id,
                        principalTable: "lessons",
                        principalColumn: "lesson_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_lesson_progress",
                columns: table => new
                {
                    progress_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    lesson_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    completed_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    quiz_score = table.Column<float>(type: "real", nullable: true),
                    passed = table.Column<bool>(type: "bit", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_lesson_progress", x => x.progress_id);
                    table.ForeignKey(
                        name: "FK_user_lesson_progress_lessons_lesson_id",
                        column: x => x.lesson_id,
                        principalTable: "lessons",
                        principalColumn: "lesson_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_lesson_progress_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "user_module_quiz_result",
                columns: table => new
                {
                    result_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    lesson_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    total_score = table.Column<float>(type: "real", nullable: false),
                    correct_count = table.Column<int>(type: "int", nullable: false),
                    total_questions = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    taken_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_module_quiz_result", x => x.result_id);
                    table.ForeignKey(
                        name: "FK_user_module_quiz_result_lessons_lesson_id",
                        column: x => x.lesson_id,
                        principalTable: "lessons",
                        principalColumn: "lesson_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_module_quiz_result_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "quiz_questions",
                columns: table => new
                {
                    question_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    quiz_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    question_text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    question_type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    sequence = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_quiz_questions", x => x.question_id);
                    table.ForeignKey(
                        name: "FK_quiz_questions_quizzes_quiz_id",
                        column: x => x.quiz_id,
                        principalTable: "quizzes",
                        principalColumn: "quiz_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "quiz_options",
                columns: table => new
                {
                    option_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    question_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    option_text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    is_correct = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_quiz_options", x => x.option_id);
                    table.ForeignKey(
                        name: "FK_quiz_options_quiz_questions_question_id",
                        column: x => x.question_id,
                        principalTable: "quiz_questions",
                        principalColumn: "question_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_quiz_answers",
                columns: table => new
                {
                    user_quiz_answer_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    question_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    selected_option_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    answer_text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    answered_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_quiz_answers", x => x.user_quiz_answer_id);
                    table.ForeignKey(
                        name: "FK_user_quiz_answers_quiz_options_selected_option_id",
                        column: x => x.selected_option_id,
                        principalTable: "quiz_options",
                        principalColumn: "option_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_user_quiz_answers_quiz_questions_question_id",
                        column: x => x.question_id,
                        principalTable: "quiz_questions",
                        principalColumn: "question_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_user_quiz_answers_users_user_id",
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

            migrationBuilder.CreateIndex(
                name: "IX_course_certificates_course_id",
                table: "course_certificates",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "IX_course_certificates_user_id",
                table: "course_certificates",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_course_weeks_course_id",
                table: "course_weeks",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "IX_courses_instructor_id",
                table: "courses",
                column: "instructor_id");

            migrationBuilder.CreateIndex(
                name: "IX_lesson_resources_lesson_id",
                table: "lesson_resources",
                column: "lesson_id");

            migrationBuilder.CreateIndex(
                name: "IX_lessons_week_id",
                table: "lessons",
                column: "week_id");

            migrationBuilder.CreateIndex(
                name: "IX_practice_exercises_lesson_id",
                table: "practice_exercises",
                column: "lesson_id");

            migrationBuilder.CreateIndex(
                name: "IX_quiz_options_question_id",
                table: "quiz_options",
                column: "question_id");

            migrationBuilder.CreateIndex(
                name: "IX_quiz_questions_quiz_id",
                table: "quiz_questions",
                column: "quiz_id");

            migrationBuilder.CreateIndex(
                name: "IX_quizzes_lesson_id",
                table: "quizzes",
                column: "lesson_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_survey_course_recommendations_course_id",
                table: "survey_course_recommendations",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "IX_survey_course_recommendations_survey_id",
                table: "survey_course_recommendations",
                column: "survey_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_lesson_progress_lesson_id",
                table: "user_lesson_progress",
                column: "lesson_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_lesson_progress_user_id",
                table: "user_lesson_progress",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_module_quiz_result_lesson_id",
                table: "user_module_quiz_result",
                column: "lesson_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_module_quiz_result_user_id",
                table: "user_module_quiz_result",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_quiz_answers_question_id",
                table: "user_quiz_answers",
                column: "question_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_quiz_answers_selected_option_id",
                table: "user_quiz_answers",
                column: "selected_option_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_quiz_answers_user_id",
                table: "user_quiz_answers",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_response_course_recommendations_course_id",
                table: "user_response_course_recommendations",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_response_course_recommendations_response_id",
                table: "user_response_course_recommendations",
                column: "response_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "course_certificates");

            migrationBuilder.DropTable(
                name: "lesson_resources");

            migrationBuilder.DropTable(
                name: "practice_exercises");

            migrationBuilder.DropTable(
                name: "survey_course_recommendations");

            migrationBuilder.DropTable(
                name: "user_lesson_progress");

            migrationBuilder.DropTable(
                name: "user_module_quiz_result");

            migrationBuilder.DropTable(
                name: "user_quiz_answers");

            migrationBuilder.DropTable(
                name: "user_response_course_recommendations");

            migrationBuilder.DropTable(
                name: "quiz_options");

            migrationBuilder.DropTable(
                name: "quiz_questions");

            migrationBuilder.DropTable(
                name: "quizzes");

            migrationBuilder.DropTable(
                name: "lessons");

            migrationBuilder.DropTable(
                name: "course_weeks");

            migrationBuilder.DropTable(
                name: "courses");

            migrationBuilder.DropTable(
                name: "instructors");

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

            migrationBuilder.DropColumn(
                name: "target_audience",
                table: "surveys");

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
        }
    }
}

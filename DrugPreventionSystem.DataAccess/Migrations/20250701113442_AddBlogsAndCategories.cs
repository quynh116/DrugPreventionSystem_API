using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DrugPreventionSystem.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddBlogsAndCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "blog_categories",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_blog_categories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "blogs",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    content = table.Column<string>(type: "text", nullable: true),
                    excerpt = table.Column<string>(type: "text", nullable: true),
                    thumbnail_url = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    category_id = table.Column<int>(type: "int", nullable: true),
                    tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    published_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    views_count = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_blogs", x => x.id);
                    table.ForeignKey(
                        name: "FK_blogs_blog_categories_category_id",
                        column: x => x.category_id,
                        principalTable: "blog_categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 7, 1, 18, 34, 40, 204, DateTimeKind.Local).AddTicks(7968));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 7, 1, 18, 34, 40, 204, DateTimeKind.Local).AddTicks(7986));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2025, 7, 1, 18, 34, 40, 204, DateTimeKind.Local).AddTicks(7987));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 4,
                column: "created_at",
                value: new DateTime(2025, 7, 1, 18, 34, 40, 204, DateTimeKind.Local).AddTicks(7989));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 5,
                column: "created_at",
                value: new DateTime(2025, 7, 1, 18, 34, 40, 204, DateTimeKind.Local).AddTicks(7990));

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "user_id", "created_at", "email", "email_verified", "is_active", "last_login", "password_hash", "role_id", "updated_at", "username" },
                values: new object[,]
                {
                    { new Guid("15e77e65-8a7d-4b65-ba57-2096de2f4113"), new DateTime(2025, 7, 1, 18, 34, 40, 585, DateTimeKind.Local).AddTicks(3666), "manager@example.com", true, true, null, "$2a$11$MhNDpi0jAwePOcXrZQvD7On1garPEQiQv57P4LWpg/f/jq.z4ZZNS", 2, null, "manager_user" },
                    { new Guid("56887662-6319-4687-b6ba-193932676e5d"), new DateTime(2025, 7, 1, 18, 34, 40, 585, DateTimeKind.Local).AddTicks(3673), "consultant@example.com", true, true, null, "$2a$11$MhNDpi0jAwePOcXrZQvD7On1garPEQiQv57P4LWpg/f/jq.z4ZZNS", 4, null, "consultant_user" },
                    { new Guid("c88de323-c7e9-49ea-998b-5aa07d7db957"), new DateTime(2025, 7, 1, 18, 34, 40, 585, DateTimeKind.Local).AddTicks(3670), "staff@example.com", true, true, null, "$2a$11$MhNDpi0jAwePOcXrZQvD7On1garPEQiQv57P4LWpg/f/jq.z4ZZNS", 3, null, "staff_user" },
                    { new Guid("d1500a7c-3b87-4b48-a8d5-e0cf0e8ac3df"), new DateTime(2025, 7, 1, 18, 34, 40, 585, DateTimeKind.Local).AddTicks(3575), "admin@example.com", true, true, null, "$2a$11$MhNDpi0jAwePOcXrZQvD7On1garPEQiQv57P4LWpg/f/jq.z4ZZNS", 1, null, "admin_user" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_blog_categories_name",
                table: "blog_categories",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_blogs_category_id",
                table: "blogs",
                column: "category_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "blogs");

            migrationBuilder.DropTable(
                name: "blog_categories");

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("15e77e65-8a7d-4b65-ba57-2096de2f4113"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("56887662-6319-4687-b6ba-193932676e5d"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("c88de323-c7e9-49ea-998b-5aa07d7db957"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("d1500a7c-3b87-4b48-a8d5-e0cf0e8ac3df"));

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
        }
    }
}

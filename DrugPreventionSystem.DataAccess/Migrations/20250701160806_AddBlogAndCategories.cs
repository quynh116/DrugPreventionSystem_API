using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DrugPreventionSystem.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddBlogAndCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "blog_categories",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    title = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    content = table.Column<string>(type: "text", nullable: true),
                    excerpt = table.Column<string>(type: "text", nullable: true),
                    thumbnail_url = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    category_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                value: new DateTime(2025, 7, 1, 23, 8, 5, 652, DateTimeKind.Local).AddTicks(8120));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 7, 1, 23, 8, 5, 652, DateTimeKind.Local).AddTicks(8156));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2025, 7, 1, 23, 8, 5, 652, DateTimeKind.Local).AddTicks(8158));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 4,
                column: "created_at",
                value: new DateTime(2025, 7, 1, 23, 8, 5, 652, DateTimeKind.Local).AddTicks(8159));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 5,
                column: "created_at",
                value: new DateTime(2025, 7, 1, 23, 8, 5, 652, DateTimeKind.Local).AddTicks(8160));

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "user_id", "created_at", "email", "email_verified", "is_active", "last_login", "password_hash", "role_id", "updated_at", "username" },
                values: new object[,]
                {
                    { new Guid("10d29045-bfea-4295-bf3a-27a2c6524bcf"), new DateTime(2025, 7, 1, 23, 8, 5, 761, DateTimeKind.Local).AddTicks(936), "consultant@example.com", true, true, null, "$2a$11$I3P03LIaVKSaA2zDcJQdY./9TLjabFpOZI3U.VVn3kx.dYPQx98Ku", 4, null, "consultant_user" },
                    { new Guid("27d739e6-3a43-4bdb-b0ab-1d98dded74bc"), new DateTime(2025, 7, 1, 23, 8, 5, 761, DateTimeKind.Local).AddTicks(920), "staff@example.com", true, true, null, "$2a$11$I3P03LIaVKSaA2zDcJQdY./9TLjabFpOZI3U.VVn3kx.dYPQx98Ku", 3, null, "staff_user" },
                    { new Guid("84233e83-739f-4901-ad79-0ee3ac020a17"), new DateTime(2025, 7, 1, 23, 8, 5, 761, DateTimeKind.Local).AddTicks(907), "admin@example.com", true, true, null, "$2a$11$I3P03LIaVKSaA2zDcJQdY./9TLjabFpOZI3U.VVn3kx.dYPQx98Ku", 1, null, "admin_user" },
                    { new Guid("e29f2784-2683-44af-aab5-6a1df890a91a"), new DateTime(2025, 7, 1, 23, 8, 5, 761, DateTimeKind.Local).AddTicks(917), "manager@example.com", true, true, null, "$2a$11$I3P03LIaVKSaA2zDcJQdY./9TLjabFpOZI3U.VVn3kx.dYPQx98Ku", 2, null, "manager_user" }
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
                keyValue: new Guid("10d29045-bfea-4295-bf3a-27a2c6524bcf"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("27d739e6-3a43-4bdb-b0ab-1d98dded74bc"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("84233e83-739f-4901-ad79-0ee3ac020a17"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("e29f2784-2683-44af-aab5-6a1df890a91a"));

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
        }
    }
}

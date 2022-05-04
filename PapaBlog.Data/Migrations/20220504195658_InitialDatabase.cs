using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace PapaBlog.Data.Migrations
{
    public partial class InitialDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(500)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Thumbnail = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ViewsCount = table.Column<int>(type: "int", nullable: false),
                    CommentCount = table.Column<int>(type: "int", nullable: false),
                    SeoAuther = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SeoDescription = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    SeoTags = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Articles_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Articles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ArticleId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedByName", "CreatedDate", "Description", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedDate", "Name", "Note" },
                values: new object[,]
                {
                    { 1, "System", new DateTime(2022, 5, 4, 23, 56, 57, 368, DateTimeKind.Local).AddTicks(9097), "C#", true, false, "System", new DateTime(2022, 5, 4, 23, 56, 57, 368, DateTimeKind.Local).AddTicks(9112), "C#", "C#" },
                    { 2, "System", new DateTime(2022, 5, 4, 23, 56, 57, 368, DateTimeKind.Local).AddTicks(9150), "Java", true, false, "System", new DateTime(2022, 5, 4, 23, 56, 57, 368, DateTimeKind.Local).AddTicks(9152), "Java", "Java" },
                    { 3, "System", new DateTime(2022, 5, 4, 23, 56, 57, 368, DateTimeKind.Local).AddTicks(9160), "Python", true, false, "System", new DateTime(2022, 5, 4, 23, 56, 57, 368, DateTimeKind.Local).AddTicks(9162), "Python", "Python" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedByName", "CreatedDate", "Description", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedDate", "Name", "Note" },
                values: new object[,]
                {
                    { 1, "Admin", new DateTime(2022, 5, 4, 23, 56, 57, 374, DateTimeKind.Local).AddTicks(8284), "Admin", true, false, "Admin", new DateTime(2022, 5, 4, 23, 56, 57, 374, DateTimeKind.Local).AddTicks(8295), "Admin", "Admin" },
                    { 2, "Admin", new DateTime(2022, 5, 4, 23, 56, 57, 374, DateTimeKind.Local).AddTicks(8329), "User", true, false, "Admin", new DateTime(2022, 5, 4, 23, 56, 57, 374, DateTimeKind.Local).AddTicks(8330), "User", "User" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedByName", "CreatedDate", "Description", "Email", "FirstName", "IsActive", "IsDeleted", "LastName", "ModifiedByName", "ModifiedDate", "Note", "PasswordHash", "Picture", "RoleId", "UserName" },
                values: new object[] { 1, "Admin", new DateTime(2022, 5, 4, 23, 56, 57, 388, DateTimeKind.Local).AddTicks(8061), "Ilk admin kullanıcısı", "tanjubo@gmail.com", "Tanju", true, false, "BO", "Admin", new DateTime(2022, 5, 4, 23, 56, 57, 388, DateTimeKind.Local).AddTicks(8078), "Admin kullanıcısı", new byte[] { 48, 49, 57, 50, 48, 50, 51, 97, 55, 98, 98, 100, 55, 51, 50, 53, 48, 53, 49, 54, 102, 48, 54, 57, 100, 102, 49, 56, 98, 53, 48, 48 }, "https://www.gravatar.com/avatar/205e460b479e2e5b48aec07710c08d50?s=200&r=pg&d=mm", 1, "tanjubo" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "CategoryId", "CommentCount", "Content", "CreatedByName", "CreatedDate", "Date", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedDate", "Note", "SeoAuther", "SeoDescription", "SeoTags", "Thumbnail", "Title", "UserId", "ViewsCount" },
                values: new object[] { 1, 1, 120, "Test Article Content", "Test CreatedByName", new DateTime(2022, 5, 4, 23, 56, 57, 362, DateTimeKind.Local).AddTicks(8149), new DateTime(2022, 5, 4, 23, 56, 57, 362, DateTimeKind.Local).AddTicks(3464), true, false, "Test ModifiedByName", new DateTime(2022, 5, 4, 23, 56, 57, 362, DateTimeKind.Local).AddTicks(8741), "Test Note", "Test SeoAuther", "Test SeoDescription", "Test SeoTags", "default.jpg", "Test Article", 1, 10 });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "CategoryId", "CommentCount", "Content", "CreatedByName", "CreatedDate", "Date", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedDate", "Note", "SeoAuther", "SeoDescription", "SeoTags", "Thumbnail", "Title", "UserId", "ViewsCount" },
                values: new object[] { 2, 2, 220, "Test Article Content", "Test CreatedByName", new DateTime(2022, 5, 4, 23, 56, 57, 363, DateTimeKind.Local).AddTicks(2039), new DateTime(2022, 5, 4, 23, 56, 57, 363, DateTimeKind.Local).AddTicks(2033), true, false, "Test ModifiedByName", new DateTime(2022, 5, 4, 23, 56, 57, 363, DateTimeKind.Local).AddTicks(2041), "Test Note", "Test SeoAuther", "Test SeoDescription", "Test SeoTags", "default.jpg", "Test Article", 1, 20 });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "CategoryId", "CommentCount", "Content", "CreatedByName", "CreatedDate", "Date", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedDate", "Note", "SeoAuther", "SeoDescription", "SeoTags", "Thumbnail", "Title", "UserId", "ViewsCount" },
                values: new object[] { 3, 3, 320, "Test Article Content", "Test CreatedByName", new DateTime(2022, 5, 4, 23, 56, 57, 363, DateTimeKind.Local).AddTicks(2057), new DateTime(2022, 5, 4, 23, 56, 57, 363, DateTimeKind.Local).AddTicks(2051), true, false, "Test ModifiedByName", new DateTime(2022, 5, 4, 23, 56, 57, 363, DateTimeKind.Local).AddTicks(2059), "Test Note", "Test SeoAuther", "Test SeoDescription", "Test SeoTags", "default.jpg", "Test Article", 1, 30 });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "ArticleId", "CreatedByName", "CreatedDate", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedDate", "Note", "Text" },
                values: new object[] { 1, 1, "Admin", new DateTime(2022, 5, 4, 23, 56, 57, 372, DateTimeKind.Local).AddTicks(2666), true, false, "Admin", new DateTime(2022, 5, 4, 23, 56, 57, 372, DateTimeKind.Local).AddTicks(2681), "This is a note", "This is a comment" });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "ArticleId", "CreatedByName", "CreatedDate", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedDate", "Note", "Text" },
                values: new object[] { 2, 2, "Admin", new DateTime(2022, 5, 4, 23, 56, 57, 372, DateTimeKind.Local).AddTicks(2718), true, false, "Admin", new DateTime(2022, 5, 4, 23, 56, 57, 372, DateTimeKind.Local).AddTicks(2720), "This is a note", "This is a comment" });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "ArticleId", "CreatedByName", "CreatedDate", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedDate", "Note", "Text" },
                values: new object[] { 3, 3, "Admin", new DateTime(2022, 5, 4, 23, 56, 57, 372, DateTimeKind.Local).AddTicks(2727), true, false, "Admin", new DateTime(2022, 5, 4, 23, 56, 57, 372, DateTimeKind.Local).AddTicks(2729), "This is a note", "This is a comment" });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_CategoryId",
                table: "Articles",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_UserId",
                table: "Articles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ArticleId",
                table: "Comments",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                table: "Users",
                column: "UserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}

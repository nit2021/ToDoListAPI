using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ToDoDAL.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Labels",
                columns: table => new
                {
                    LabelId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(maxLength: 50, nullable: false),
                    ToDoItemID = table.Column<int>(nullable: true),
                    ToDoListID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Labels", x => x.LabelId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(maxLength: 50, nullable: false),
                    Password = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "ToDoLists",
                columns: table => new
                {
                    ListId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(maxLength: 50, nullable: false),
                    OwnerID = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToDoLists", x => x.ListId);
                    table.ForeignKey(
                        name: "FK_ToDoLists_Users_OwnerID",
                        column: x => x.OwnerID,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ToDoItems",
                columns: table => new
                {
                    ItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(maxLength: 50, nullable: false),
                    ToDoListID = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToDoItems", x => x.ItemId);
                    table.ForeignKey(
                        name: "FK_ToDoItems_ToDoLists_ToDoListID",
                        column: x => x.ToDoListID,
                        principalTable: "ToDoLists",
                        principalColumn: "ListId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Labels",
                columns: new[] { "LabelId", "Description", "ToDoItemID", "ToDoListID" },
                values: new object[] { 401, "Label1", 301, 201 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Password", "UserName" },
                values: new object[] { 101, "EV9zkHMWIr+8VQ7eqzBpDesrvqKYB4PCXTAa9C/Bcng=", "Admin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Password", "UserName" },
                values: new object[] { 102, "EV9zkHMWIr+8VQ7eqzBpDZ/8mb+J0unVgxlb1UV8IdY=", "Standard" });

            migrationBuilder.InsertData(
                table: "ToDoLists",
                columns: new[] { "ListId", "CreatedDate", "Description", "OwnerID", "UpdatedDate" },
                values: new object[] { 201, new DateTime(2021, 11, 19, 11, 35, 35, 417, DateTimeKind.Utc).AddTicks(6196), "ListItem1", 101, null });

            migrationBuilder.InsertData(
                table: "ToDoItems",
                columns: new[] { "ItemId", "CreatedDate", "Description", "ToDoListID", "UpdatedDate" },
                values: new object[] { 301, new DateTime(2021, 11, 19, 11, 35, 35, 417, DateTimeKind.Utc).AddTicks(7172), "Item1", 201, null });

            migrationBuilder.CreateIndex(
                name: "IX_ToDoItems_ToDoListID",
                table: "ToDoItems",
                column: "ToDoListID");

            migrationBuilder.CreateIndex(
                name: "IX_ToDoLists_OwnerID",
                table: "ToDoLists",
                column: "OwnerID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Labels");

            migrationBuilder.DropTable(
                name: "ToDoItems");

            migrationBuilder.DropTable(
                name: "ToDoLists");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

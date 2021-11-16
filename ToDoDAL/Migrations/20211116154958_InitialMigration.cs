using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ToDoDAL.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    Owner = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToDoLists", x => x.ListId);
                    table.ForeignKey(
                        name: "FK_ToDoLists_Users_Owner",
                        column: x => x.Owner,
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
                    TaskOwner = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToDoItems", x => x.ItemId);
                    table.ForeignKey(
                        name: "FK_ToDoItems_ToDoLists_TaskOwner",
                        column: x => x.TaskOwner,
                        principalTable: "ToDoLists",
                        principalColumn: "ListId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Labels",
                columns: table => new
                {
                    LabelId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(maxLength: 50, nullable: false),
                    ItemOwner = table.Column<int>(nullable: false),
                    ToDoListID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Labels", x => x.LabelId);
                    table.ForeignKey(
                        name: "FK_Labels_ToDoItems_ItemOwner",
                        column: x => x.ItemOwner,
                        principalTable: "ToDoItems",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Password", "UserName" },
                values: new object[] { 101, "Pa$$w0rd", "Admin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Password", "UserName" },
                values: new object[] { 102, "Pa$$w0rd", "Standard" });

            migrationBuilder.InsertData(
                table: "ToDoLists",
                columns: new[] { "ListId", "CreatedDate", "Description", "Owner", "UpdatedDate" },
                values: new object[] { 201, new DateTime(2021, 11, 16, 15, 49, 58, 359, DateTimeKind.Utc).AddTicks(8472), "ListItem1", 101, null });

            migrationBuilder.InsertData(
                table: "ToDoItems",
                columns: new[] { "ItemId", "CreatedDate", "Description", "TaskOwner", "UpdatedDate" },
                values: new object[] { 301, new DateTime(2021, 11, 16, 15, 49, 58, 359, DateTimeKind.Utc).AddTicks(9309), "Item1", 201, null });

            migrationBuilder.InsertData(
                table: "Labels",
                columns: new[] { "LabelId", "Description", "ItemOwner", "ToDoListID" },
                values: new object[] { 401, "Label1", 301, 201 });

            migrationBuilder.CreateIndex(
                name: "IX_Labels_ItemOwner",
                table: "Labels",
                column: "ItemOwner");

            migrationBuilder.CreateIndex(
                name: "IX_ToDoItems_TaskOwner",
                table: "ToDoItems",
                column: "TaskOwner");

            migrationBuilder.CreateIndex(
                name: "IX_ToDoLists_Owner",
                table: "ToDoLists",
                column: "Owner");
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

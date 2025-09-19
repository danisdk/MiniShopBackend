using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MiniShop.Migrations
{
    /// <inheritdoc />
    public partial class MigrationNo2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_User_AuthorCreatedId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_User_AuthorUpdatedId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_User_AuthorCreatedId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_User_AuthorUpdatedId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_User_AuthorCreatedId",
                table: "OrderProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_User_AuthorUpdatedId",
                table: "OrderProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_User_AuthorCreatedId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_User_AuthorUpdatedId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_User_UserId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_User_AuthorCreatedId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_User_AuthorUpdatedId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Token = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Expires = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Revoked = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Users_AuthorCreatedId",
                table: "Categories",
                column: "AuthorCreatedId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Users_AuthorUpdatedId",
                table: "Categories",
                column: "AuthorUpdatedId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Users_AuthorCreatedId",
                table: "Images",
                column: "AuthorCreatedId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Users_AuthorUpdatedId",
                table: "Images",
                column: "AuthorUpdatedId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_Users_AuthorCreatedId",
                table: "OrderProducts",
                column: "AuthorCreatedId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_Users_AuthorUpdatedId",
                table: "OrderProducts",
                column: "AuthorUpdatedId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_AuthorCreatedId",
                table: "Orders",
                column: "AuthorCreatedId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_AuthorUpdatedId",
                table: "Orders",
                column: "AuthorUpdatedId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_AuthorCreatedId",
                table: "Products",
                column: "AuthorCreatedId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_AuthorUpdatedId",
                table: "Products",
                column: "AuthorUpdatedId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Users_AuthorCreatedId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Users_AuthorUpdatedId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Users_AuthorCreatedId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Users_AuthorUpdatedId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_Users_AuthorCreatedId",
                table: "OrderProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_Users_AuthorUpdatedId",
                table: "OrderProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_AuthorCreatedId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_AuthorUpdatedId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_UserId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_AuthorCreatedId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_AuthorUpdatedId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_User_AuthorCreatedId",
                table: "Categories",
                column: "AuthorCreatedId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_User_AuthorUpdatedId",
                table: "Categories",
                column: "AuthorUpdatedId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_User_AuthorCreatedId",
                table: "Images",
                column: "AuthorCreatedId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_User_AuthorUpdatedId",
                table: "Images",
                column: "AuthorUpdatedId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_User_AuthorCreatedId",
                table: "OrderProducts",
                column: "AuthorCreatedId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_User_AuthorUpdatedId",
                table: "OrderProducts",
                column: "AuthorUpdatedId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_User_AuthorCreatedId",
                table: "Orders",
                column: "AuthorCreatedId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_User_AuthorUpdatedId",
                table: "Orders",
                column: "AuthorUpdatedId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_User_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_User_AuthorCreatedId",
                table: "Products",
                column: "AuthorCreatedId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_User_AuthorUpdatedId",
                table: "Products",
                column: "AuthorUpdatedId",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}

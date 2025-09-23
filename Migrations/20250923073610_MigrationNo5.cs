using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniShop.Migrations
{
    /// <inheritdoc />
    public partial class MigrationNo5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Images_ImageId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_AuthorCreatedId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_AuthorUpdatedId",
                table: "Products");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Users_AuthorCreatedId",
                table: "Categories",
                column: "AuthorCreatedId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Users_AuthorUpdatedId",
                table: "Categories",
                column: "AuthorUpdatedId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Users_AuthorCreatedId",
                table: "Images",
                column: "AuthorCreatedId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Users_AuthorUpdatedId",
                table: "Images",
                column: "AuthorUpdatedId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_Users_AuthorCreatedId",
                table: "OrderProducts",
                column: "AuthorCreatedId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_Users_AuthorUpdatedId",
                table: "OrderProducts",
                column: "AuthorUpdatedId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_AuthorCreatedId",
                table: "Orders",
                column: "AuthorCreatedId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_AuthorUpdatedId",
                table: "Orders",
                column: "AuthorUpdatedId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Images_ImageId",
                table: "Products",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_AuthorCreatedId",
                table: "Products",
                column: "AuthorCreatedId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_AuthorUpdatedId",
                table: "Products",
                column: "AuthorUpdatedId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
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
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Images_ImageId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_AuthorCreatedId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_AuthorUpdatedId",
                table: "Products");

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
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Images_ImageId",
                table: "Products",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id");

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
    }
}

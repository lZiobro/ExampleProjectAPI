using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Model.Migrations
{
    public partial class userdetails_mistype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUsersDetials_AspNetUsers_UserId",
                table: "ApplicationUsersDetials");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUsersDetials",
                table: "ApplicationUsersDetials");

            migrationBuilder.RenameTable(
                name: "ApplicationUsersDetials",
                newName: "ApplicationUsersDetails");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUsersDetails",
                table: "ApplicationUsersDetails",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUsersDetails_AspNetUsers_UserId",
                table: "ApplicationUsersDetails",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUsersDetails_AspNetUsers_UserId",
                table: "ApplicationUsersDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUsersDetails",
                table: "ApplicationUsersDetails");

            migrationBuilder.RenameTable(
                name: "ApplicationUsersDetails",
                newName: "ApplicationUsersDetials");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUsersDetials",
                table: "ApplicationUsersDetials",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUsersDetials_AspNetUsers_UserId",
                table: "ApplicationUsersDetials",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

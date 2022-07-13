using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Model.Migrations
{
    public partial class message_username_change : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SenderUserName",
                table: "Messages",
                newName: "SenderName");

            migrationBuilder.RenameColumn(
                name: "ReceiverUserName",
                table: "Messages",
                newName: "ReceiverName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SenderName",
                table: "Messages",
                newName: "SenderUserName");

            migrationBuilder.RenameColumn(
                name: "ReceiverName",
                table: "Messages",
                newName: "ReceiverUserName");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Model.Migrations
{
    public partial class userdetails_as_separate_entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AboutMe",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Dislikes",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Experience",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "HasEquipment",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Home",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Likes",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Occupation",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Race",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Specialty",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "ApplicationUsersDetials",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Race = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Occupation = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Experience = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Home = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    HasEquipment = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Likes = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Dislikes = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Specialty = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AboutMe = table.Column<string>(type: "nvarchar(max)", maxLength: 4095, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUsersDetials", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_ApplicationUsersDetials_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUsersDetials");

            migrationBuilder.AddColumn<string>(
                name: "AboutMe",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                maxLength: 4095,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Dislikes",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Experience",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "HasEquipment",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Home",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Likes",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Occupation",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Race",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Specialty",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }
    }
}

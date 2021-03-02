using Microsoft.EntityFrameworkCore.Migrations;

namespace PadoruHelperBotDAL.Migrations.Migrations
{
    public partial class AddTr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<ulong>(
                name: "GuildId",
                table: "UserSubscriptions",
                type: "bigint unsigned",
                nullable: false,
                defaultValue: 0ul);

            migrationBuilder.AddColumn<bool>(
                name: "Training",
                table: "UserSubscriptions",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GuildId",
                table: "UserSubscriptions");

            migrationBuilder.DropColumn(
                name: "Training",
                table: "UserSubscriptions");
        }
    }
}

using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PadoruHelperBotDAL.Migrations.Migrations
{
    public partial class Teams : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserSubscriptions",
                table: "UserSubscriptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AlertPetition",
                table: "AlertPetition");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserSubscriptions");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "AlertPetition");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserSubscriptions",
                table: "UserSubscriptions",
                columns: new[] { "UserId", "GuildId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AlertPetition",
                table: "AlertPetition",
                columns: new[] { "UserId", "AlertType" });

            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    Id = table.Column<ulong>(type: "bigint unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true),
                    RoleId = table.Column<ulong>(type: "bigint unsigned", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<ulong>(type: "bigint unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TeamId = table.Column<ulong>(type: "bigint unsigned", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_TeamId",
                table: "Users",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSubscriptions_Users_UserId",
                table: "UserSubscriptions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSubscriptions_Users_UserId",
                table: "UserSubscriptions");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Team");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserSubscriptions",
                table: "UserSubscriptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AlertPetition",
                table: "AlertPetition");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UserSubscriptions",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "AlertPetition",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserSubscriptions",
                table: "UserSubscriptions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AlertPetition",
                table: "AlertPetition",
                column: "Id");
        }
    }
}

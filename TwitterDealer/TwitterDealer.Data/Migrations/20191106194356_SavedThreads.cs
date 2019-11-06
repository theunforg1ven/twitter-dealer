using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TwitterDealer.Data.Migrations
{
    public partial class SavedThreads : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SavedThreads",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsFavourite = table.Column<bool>(nullable: false),
                    RetweetCount = table.Column<int>(nullable: false),
                    TweetText = table.Column<string>(nullable: true),
                    Language = table.Column<string>(nullable: true),
                    IsPossiblySensitive = table.Column<bool>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    FavoriteCount = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    ApplicationUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavedThreads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SavedThreads_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SavedThreads_ApplicationUserId",
                table: "SavedThreads",
                column: "ApplicationUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SavedThreads");
        }
    }
}

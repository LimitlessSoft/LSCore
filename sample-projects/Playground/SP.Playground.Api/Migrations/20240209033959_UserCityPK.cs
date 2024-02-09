using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SP.Playground.Api.Migrations
{
    public partial class UserCityPK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "UserEntity",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserEntity_CityId",
                table: "UserEntity",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserEntity_CityEntity_CityId",
                table: "UserEntity",
                column: "CityId",
                principalTable: "CityEntity",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserEntity_CityEntity_CityId",
                table: "UserEntity");

            migrationBuilder.DropIndex(
                name: "IX_UserEntity_CityId",
                table: "UserEntity");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "UserEntity");
        }
    }
}

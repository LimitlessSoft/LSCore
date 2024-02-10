using Microsoft.EntityFrameworkCore.Migrations;
using SP.Playground.Contracts;

#nullable disable

namespace SP.Playground.Api.Migrations
{
    public partial class HousesSeeds : Migration
    {
        private readonly string UpFile_001 = Path.Combine(Constants.DbSeedsRoot, "HousesSeed_001.sql");
        private readonly string DownFile_001 = Path.Combine(Constants.DbSeedsRoot, "Down", "HousesSeed_001.sql");
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(File.ReadAllText(UpFile_001));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(File.ReadAllText(DownFile_001));
        }
    }
}

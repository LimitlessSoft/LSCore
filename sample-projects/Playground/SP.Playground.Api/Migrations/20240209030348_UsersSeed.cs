using Microsoft.EntityFrameworkCore.Migrations;
using SP.Playground.Contracts;

#nullable disable

namespace SP.Playground.Api.Migrations
{
    public partial class UsersSeed : Migration
    {
        private readonly string UpFile_001 = Path.Combine(Constants.DbSeedsRoot, "UsersSeed_001.sql");
        private readonly string DownFile_001 = Path.Combine(Constants.DbSeedsRoot, "Down", "UsersSeed_001.sql");
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

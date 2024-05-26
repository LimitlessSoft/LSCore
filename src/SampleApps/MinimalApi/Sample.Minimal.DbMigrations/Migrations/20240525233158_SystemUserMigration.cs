using Microsoft.EntityFrameworkCore.Migrations;
using Sample.Minimal.Contracts.Constants;

#nullable disable

namespace Sample.Minimal.DbMigrations.Migrations
{
    /// <inheritdoc />
    public partial class SystemUserMigration : Migration
    {
        private string _upScript = Path.Combine(Constants.DbMigrations.UpSeeds, "SystemUser.sql");
        private string _downScript = Path.Combine(Constants.DbMigrations.DownSeeds, "SystemUser.sql");
        
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(File.ReadAllText(_upScript));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(File.ReadAllText(_downScript));
        }
    }
}

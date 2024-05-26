namespace Sample.Minimal.Contracts.Constants;

public static partial class Constants
{
    public static class DbMigrations
    {
        public static readonly string DbSeedsRoot = Path.Combine(Environment.CurrentDirectory, "Seeds");
        public static readonly string UpSeeds = Path.Combine(DbSeedsRoot, "Up");
        public static readonly string DownSeeds = Path.Combine(DbSeedsRoot, "Down");
    }
}
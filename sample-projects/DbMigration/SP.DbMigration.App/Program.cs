using LSCore.Framework.Extensions;

namespace SP.DbMigration.App
{
    internal class Program
    {
        static void Main(string[] args) =>
            LSCoreStartupExtensions.InitializeLSCoreApplication<Startup>(args);
    }
}
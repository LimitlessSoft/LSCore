using LSCore.Framework.Extensions;

namespace SP.Simple.Api
{
    public class Program
    {
        public static void Main(string[] args) =>
            LSCoreStartupExtensions.InitializeLSCoreApplication<Startup>(args);
    }
}
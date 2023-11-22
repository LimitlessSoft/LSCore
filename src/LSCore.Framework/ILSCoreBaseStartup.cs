using Microsoft.Extensions.Configuration;

namespace LSCore.Framework
{
    public interface ILSCoreBaseStartup
    {
        IConfigurationRoot ConfigurationRoot { get; set; }
    }
}

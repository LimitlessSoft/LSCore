using Microsoft.Extensions.Configuration;

namespace TD.Core.Framework
{
    public interface ILSCoreBaseStartup
    {
        IConfigurationRoot ConfigurationRoot { get; set; }
    }
}

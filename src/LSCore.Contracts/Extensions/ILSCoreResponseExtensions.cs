using LSCore.Contracts.Http.Interfaces;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace LSCore.Contracts.Extensions
{
    public static class ILSCoreResponseExtensions
    {
        public static void LogError(this ILSCoreResponse response,
            ILogger logger,
            [CallerFilePath] string callerFilePath = "",
            [CallerLineNumber] int callerLineNumber = 0)
        {
            logger.LogError(string.Format(LSCoreContractsConstants.ResponseLogMessages.LogErrorBaseMessageFormat, response.Status, callerFilePath, callerLineNumber));
            logger.LogError(string.Format(LSCoreContractsConstants.ResponseLogMessages.LogErrorObjectFormat, JsonConvert.SerializeObject(response)));
        }

        public static void LogError<TLoggerClass>(this ILSCoreResponse response,
            ILogger<TLoggerClass> logger,
            [CallerFilePath] string callerFilePath = "",
            [CallerLineNumber] int callerLineNumber = 0)
        {
            logger.LogError(string.Format(LSCoreContractsConstants.ResponseLogMessages.LogErrorBaseMessageFormat, response.Status, callerFilePath, callerLineNumber));
            logger.LogError(string.Format(LSCoreContractsConstants.ResponseLogMessages.LogErrorObjectFormat, JsonConvert.SerializeObject(response)));
        }
    }
}

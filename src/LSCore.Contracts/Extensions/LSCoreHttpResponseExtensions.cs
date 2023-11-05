using LSCore.Contracts.Http.Interfaces;
using System.Net;

namespace LSCore.Contracts.Extensions
{
    public static class LSCoreHttpResponseExtensions
    {
        public static bool NotOk(this HttpResponseMessage sender)
        {
            return Convert.ToInt16(sender.StatusCode).ToString()[0] != '2';
        }

        public static void Merge(this ILSCoreResponse source, ILSCoreResponse response) => source.Status = response.NotOk ? response.Status == HttpStatusCode.NotFound ? HttpStatusCode.NotFound : HttpStatusCode.BadRequest : source.Status;
    }
}

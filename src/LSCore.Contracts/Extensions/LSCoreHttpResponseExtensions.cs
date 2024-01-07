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

        public static void Merge(this ILSCoreResponse source, ILSCoreResponse response)
        {
            source.Status = response.NotOk ? response.Status == HttpStatusCode.NotFound ? HttpStatusCode.NotFound : HttpStatusCode.BadRequest : source.Status;
            if (source.NotOk && response.Errors != null)
            {
                if(source.Errors == null)
                    source.Errors = new List<string>();

                source.Errors.AddRange(response.Errors);
            }
        }

        public static void Merge<TPayload>(this ILSCoreResponse<TPayload> source, ILSCoreResponse response)
        {
            (source as ILSCoreResponse).Merge(response);
            if (source.NotOk)
                source.Payload = default(TPayload);
        }
    }
}

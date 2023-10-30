using LSCore.Contracts.Http.Interfaces;
using LSCore.Contracts.Requests;

namespace LSCore.Contracts.Extensions
{
    public static class RequestExtensions
    {
        public static bool IdsNotMatch(this LSCoreIdRequest request, int id, bool allowNullAsId = true)
        {
            var mockSaveRequest = new LSCoreSaveRequest(request.Id);
            var notMatch = mockSaveRequest.IdsNotMatch(id, allowNullAsId);

            request.Id = mockSaveRequest.Id ?? -1;

            return notMatch;
        }
        public static bool IdsNotMatch(this LSCoreSaveRequest request, int id, bool allowNullAsId = true)
        {
            if (request == null)
                return true;

            var notMatch = allowNullAsId == false && request.Id != id;

            if(request.Id == null)
                request.Id = id;

            return notMatch;
        }

        public static bool IdsNotMatch<TResponse>(this LSCoreIdRequest request, int id, TResponse response, bool allowNullAsId = true)
            where TResponse : ILSCoreResponse
        {
            var mockSaveRequest = new LSCoreSaveRequest(request.Id);
            var notMatch = mockSaveRequest.IdsNotMatch(id, allowNullAsId);

            request.Id = mockSaveRequest.Id ?? -1;

            return notMatch;
        }

        public static bool IdsNotMatch<TResponse>(this LSCoreSaveRequest request, int id, TResponse response, bool allowNullAsId = true)
            where TResponse : ILSCoreResponse
        {
            var notMatch = request.IdsNotMatch(id, allowNullAsId);
            if(notMatch)
            {
                response.Status = System.Net.HttpStatusCode.BadRequest;
                response.Errors = new List<string>()
                {
                    string.Format("Ids do no match!", nameof(request))
                };
                return true;
            }

            return notMatch;
        }
    }
}

using System.Web;

namespace LSCore.ApiClient.Rest;

public static class HttpClientExtensions
{
    private static string ConvertToQueryParameters(object obj, bool prepandWithQuestionMark = true) {
        var properties = from p in obj.GetType().GetProperties()
            where p.GetValue(obj, null) != null
            select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null).ToString());

        return (prepandWithQuestionMark ? "?" : "") + string.Join("&", properties.ToArray());
    }
    
    public static Task<HttpResponseMessage> GetAsJsonAsync(this HttpClient client, string requestUri, object obj) =>
        client.GetAsync(requestUri + ConvertToQueryParameters(obj));
}
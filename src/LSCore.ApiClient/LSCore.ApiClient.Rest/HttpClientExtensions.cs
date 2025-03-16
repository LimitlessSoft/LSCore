using System.Web;

namespace LSCore.ApiClient.Rest;

public static class HttpClientExtensions
{
    private static string ConvertToQueryParameters(object obj, bool prependWithQuestionMark = true) 
    {
        var properties = obj.GetType().GetProperties()
            .Where(p => p.GetValue(obj) != null)
            .SelectMany(p => 
            {
                var value = p.GetValue(obj);
                if(value is null)
                    return [];
                
                if (value is System.Collections.IEnumerable enumerable and not string)
                    return enumerable.Cast<object>()
                        .Select(item => $"{p.Name}={HttpUtility.UrlEncode(item.ToString())}");
                
                return [ $"{p.Name}={HttpUtility.UrlEncode(value.ToString())}" ];
            });

        return (prependWithQuestionMark ? "?" : "") + string.Join("&", properties);
    }
    
    public static Task<HttpResponseMessage> GetAsJsonAsync(this HttpClient client, string requestUri, object obj) =>
        client.GetAsync(requestUri + ConvertToQueryParameters(obj));
}
namespace LSCore.Contracts.Http
{
    public static class LSCoreExtensions
    {
        public static Task<HttpResponseMessage> GetAsJsonAsync<TRequest>(this HttpClient httpClient, string endpoint, TRequest request)
        {
            string finalEndpoint = endpoint + "?";
            foreach (var propertyInfo in request.GetType().GetProperties())
            {
                var value = propertyInfo.GetValue(request);
                if (value == null)
                    continue;

                string sValue = value.ToString();
                if (value.GetType() == typeof(DateTime) || value.GetType() == typeof(DateTime?))
                {
                    sValue = (TimeZoneInfo.ConvertTimeToUtc((DateTime)value)).ToString("yyyy-MM-ddTHH:mm:ss.fffffffZ");
                }

                finalEndpoint += $"{propertyInfo.Name}={sValue}&";
            }

            return httpClient.GetAsync(finalEndpoint);
        }
    }
}

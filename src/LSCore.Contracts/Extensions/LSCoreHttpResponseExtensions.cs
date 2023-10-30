namespace LSCore.Contracts.Extensions
{
    public static class LSCoreHttpResponseExtensions
    {
        public static bool NotOk(this HttpResponseMessage sender)
        {
            return Convert.ToInt16(sender.StatusCode).ToString()[0] != '2';
        }
    }
}

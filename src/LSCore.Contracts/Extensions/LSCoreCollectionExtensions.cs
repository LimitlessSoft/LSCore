using System.Collections;

namespace LSCore.Contracts.Extensions
{
    public static class LSCoreCollectionExtensions
    {
        public static bool IsEmpty(this ICollection sender)
        {
            if (sender.Count == 0)
                return true;

            return false;
        }
    }
}

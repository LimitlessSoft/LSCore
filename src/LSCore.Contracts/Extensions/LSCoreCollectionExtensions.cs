using System.Collections;

namespace LSCore.Contracts.Extensions;

public static class LSCoreCollectionExtensions
{
    public static bool IsEmpty(this ICollection sender) =>
        sender.Count == 0;
}

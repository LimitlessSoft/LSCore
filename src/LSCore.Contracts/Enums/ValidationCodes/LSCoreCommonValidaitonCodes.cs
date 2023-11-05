using System.ComponentModel;

namespace LSCore.Contracts.Enums.ValidationCodes
{
    public enum LSCoreCommonValidaitonCodes
    {
        [Description("Value '{0}' must be greater than '{1}'.")]
        COMM_001,
        [Description("Parameter '{0}' must not be null!")]
        COMM_002,
        [Description("Parameter '{0}' must contain less than {1} characters.")]
        COMM_003,
        [Description("Parameter '{0}' must contain more than {1} characters.")]
        COMM_004,
        [Description("DBContext is not set!")]
        COMM_005,
    }
}

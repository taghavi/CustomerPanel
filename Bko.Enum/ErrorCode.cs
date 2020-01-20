using System.ComponentModel;

namespace Enum
{
    public enum ErrorCode
    {
        [Description("خطای داخلی ")]
        Internal = 0,
        [Description("خطای سیستمی ")]
        Systematic = 2,
        [Description("خطای کاربری ")]
        UserFault = 3
    }
}
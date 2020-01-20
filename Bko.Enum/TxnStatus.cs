using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enum
{
    public enum TxnStatus : short
    {
        [Description("نامشخص")]
        Undefined = 0,
        [Description("موفق")]
        Successfull,
        [Description("برگشت خورده موفق")]
        SuccessfulReverse,
        [Description("منتظر ابطال")]
        WaitingForReject,
        [Description("تسویه شده")]
        Setteled,
        [Description("منتظر تایید")]
        WaitingForConfirm
    }
}

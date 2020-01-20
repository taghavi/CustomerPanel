using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bko.Service.DTOs
{
    public class CreditHistoryDTO
    {
        public DateTime? DateCreated { get; set; }
        public long Amount { get; set; }
        public int RespCode { get; set; }

    }
}
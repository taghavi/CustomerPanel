using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bko.Service.DTOs
{
    public class ApprovalResponse
    {
        public string Status { get; set; }
        public long ReturnId { get; set; }
        public string Message { get; set; }
        public long Amount { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bko.Service.DTOs
{
    public class ApprovalRequest
    {
        public string TnxId { get; set; }
        public long Tid { get; set; }
    }
}
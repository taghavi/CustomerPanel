using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bko.Service.DTOs
{
    public class InvoiceDTO
    {
        public string TerminalID { get; set; }
        public long Amount { get; set; }
        public string CallBackUrl { get; set; }
        public int InvoiceId { get; set; }
        public string PayLoad { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bko.Service.DTOs
{
    public class PaymentResponseDTO
    {
        public int paymenttransactionid { get; set; }
        public int respcode { get; set; }
        public long amount { get; set; }

        public string payload { get; set; }
        public int terminalid { get; set; }
        public int tracenumber { get; set; }
        public string rrn { get; set; }
        public string datepaid { get; set; }

        public string digitalreceipt { get; set; }
        public string issuerbank { get; set; }
        public string advicestatus { get; set; }
        public long advicereturnId { get; set; }
        public int invoiceid { get; set; }

    }
}
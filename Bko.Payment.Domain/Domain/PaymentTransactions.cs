using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Bko.Payment.Domain.Domain
{
    public class PaymentTransactions : BaseEntity
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]
        public int PaymentTransactionId { get; set; }
        public int RespCode { get; set; }
        public long Amount { get; set; }
        
        public string Payload { get; set; }
        public int TerminalID { get; set; }
        public int TraceNumber { get; set; }
        public string RRN { get; set; }
        public string DatePaid { get; set; }
                
        public string DigitalReceipt { get; set; }
        public string IssuerBank { get; set; }
        public string AdviceStatus { get; set; }
        public long AdviceReturnId { get; set; }
        public string RollBackStatus { get; set; }
        public long RollBackReturnId { get; set; }
        public int InvoiceId { get; set; }
        public virtual Invoice Invoice { get; set; }
    }
}

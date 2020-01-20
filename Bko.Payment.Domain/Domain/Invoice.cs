using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Bko.Payment.Domain.Domain
{
    public class Invoice : BaseEntity
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]
        public int InvoiceId { get; set; }
        public long Amount { get; set; }
        public string UserName { get; set; }
        public int UserId { get; set; }
        public virtual ICollection<TerminalInfo> TerminalInfos { get; set; }
        public virtual ICollection<PaymentTransactions> PaymentTransactions { get; set; }
        public int TerminalInfoId { get; set; }
        public string CallBackUrl { get; set; }
        public string PayLoad { get; set; }
    }
}

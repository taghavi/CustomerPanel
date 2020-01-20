using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bko.Service.DTOs
{
    public class ParametersofTransactions
    {
        public string TransactionDate { get; set; }
        public string PhoneNumber { get; set; }
        public string CustomerTransactionId { get; set; }        
        public string CustomerName { get; set; }
        public string TransactionType { get; set; }        
        public char? TransactionStatus { get; set; }        
        public string MobileCellOperator { get; set; }        
        public string SystemTransactionId { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
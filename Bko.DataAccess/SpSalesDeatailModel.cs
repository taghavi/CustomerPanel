using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Bko.DataAccess
{
    public partial class SpSalesDetail2Model
    {
        [DisplayAttribute(Name = "مشتری")]
        public string Customer { get; set; }
        [DisplayAttribute(Name = "تاریخ")]
        public string Date { get; set; }
        [DisplayAttribute(Name = "کد اپراتور")]
        public Nullable<byte> OperatorId { get; set; }
        [DisplayAttribute(Name = "عنوان اپراتور")]
        public string OperatorTitle { get; set; }
        [DisplayAttribute(Name = "جمع مبلغ")]
        public Nullable<decimal> OriginalAmount { get; set; }
        [DisplayAttribute(Name = "نوع شارژ")]
        public string ChargeType { get; set; }
        [DisplayAttribute(Name ="کد مشتری")]
        public long AccountID { get; set; }
        [DisplayAttribute(Name = "تعداد")]
        public Nullable<int> Count { get; set; }

        [DisplayAttribute(Name = "مبلغ")]
        public decimal Amount { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bko.DataAccess
{
    public partial class SpSimSale
    {
        [DisplayAttribute(Name ="نام استان")]
        public string province_title { get; set; }
        [DisplayAttribute(Name = "نوع سیم کارت")]
        public String SimType { get; set; }
        [DisplayAttribute(Name = "تعداد")]
        public int Count { get; set; }

    }
    public partial class SpPinSalesClass
    {
        
        [DisplayAttribute(Name ="مشتری")]
        public string Customer { get; set; }
        [DisplayAttribute(Name = "تاریخ فروش")]
        public string Date { get; set; }
        [DisplayAttribute(Name ="اپراتور")]
        public string  OperatorTitle { get; set; }
        [DisplayAttribute(Name ="قیمت واحد")]
        public decimal OriginalAmount { get; set; }
        [DisplayAttribute(Name ="نوع شارژ")]
        public string ChargeType { get; set; }
        [DisplayAttribute(Name ="تعداد فروش")]
        public int Count { get; set; }
        [DisplayAttribute(Name ="کل فروش")]
        public decimal Amount { get; set; }
    }
    public partial class spsimcashClass
    {
        [DisplayAttribute(Name = "نام استان")]
        public string province_title { get; set; }
        [DisplayAttribute(Name = "نوع سیم کارت")]
        public string @SimType { get; set; }
        [DisplayAttribute(Name = "پیش شماره")]
        public string precode { get; set; }
        [DisplayAttribute(Name = "درجه سیم کارت")]
        public string CellGrade { get; set; }
        [DisplayAttribute(Name = "تعداد")]
        public int Count { get; set; }

    }
    public partial class sppincash
    {
        [DisplayAttribute(Name = "عنوان")]
        public string title { get; set; }
        [DisplayAttribute(Name = "تعداد پین موجود")]
        public int chargepoolCount { get; set; }
    }
    public partial class ChargeReport
    {
       
    
        [DisplayAttribute(Name = "تاریخ شارژ")]
        public DateTime ChargeDate { get; set; }
        [DisplayAttribute(Name = "شرح سند")]
        public string Title { get; set; }
        [DisplayAttribute(Name = "مقدار شارژ")]
        public decimal Amount { get; set; }
    }
    public partial class ChargeReportExcel
    {


        [DisplayAttribute(Name = "تاریخ شارژ")]
        public string ChargeDate { get; set; }
        [DisplayAttribute(Name = "شرح سند")]
        public string Title { get; set; }
        [DisplayAttribute(Name = "مقدار شارژ")]
        public string Amount { get; set; }
    }
}

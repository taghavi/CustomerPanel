using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;
using System.ComponentModel.DataAnnotations;

namespace Bko.Service.DTOs
{
    public class RepCheckDTO
    {
        [DisplayAttribute(Name = "نام جدول")]
        public string TBL_Name { get; set; }
        public Nullable<long> Id { get; set; }
        [DisplayAttribute(Name = "کد مشتری")]
        public long BId { get; set; }
        [DisplayAttribute(Name = "نام")]
        public string TitleFa { get; set; }
        [DisplayAttribute(Name = "نوع تراکنش")]
        public Nullable<int> prcode { get; set; }
        [DisplayAttribute(Name = "مبلغ شارژ")]
        public decimal OriginalAmount { get; set; }
        // public Nullable<long> FwBId { get; set; }
        [DisplayAttribute(Name = "کد پیگیری مشتری")]
        public string ReserveNumber_RequestSerial { get; set; }
        [DisplayAttribute(Name = "شماره مرجع")]
        public Nullable<long> RefrenceNumber { get; set; }
        [DisplayAttribute(Name = "کد پیگیری سامانه ")]
        public Nullable<long> LocalSerial { get; set; }
        [DisplayAttribute(Name = "کد پاسخ")]
        public Nullable<int> ResponseCode { get; set; }
        [DisplayAttribute(Name = "متن پاسخ")]
        public string ResponseMsg { get; set; }
        [DisplayAttribute(Name = "وضعیت تراکنش")]
        public Nullable<short> TxStatus { get; set; }
        [DisplayAttribute(Name = "شماره همراه ")]
        public string AddData1 { get; set; }
        [DisplayAttribute(Name = "شماره تلفن 2")]
        public string AddData2 { get; set; }
        [DisplayAttribute(Name = "آدرس")]
        public string Address { get; set; }
        public Nullable<byte> OperatorId { get; set; }
        [DisplayAttribute(Name = "اپراتور")]
        public string OperatorTitle { get; set; }
        [DisplayAttribute(Name = "تاریخ درخواست")]
        public System.DateTime RequestDate { get; set; }

        public System.DateTime CreatedOn { get; set; }
        [DisplayAttribute(Name = "تاریخ")]
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<System.DateTime> LocalDate_TX { get; set; }
        public Nullable<System.DateTime> LocalDate_RX { get; set; }
        [DisplayAttribute(Name = "سند حسابداری")]
        public Nullable<long> journalId { get; set; }

        public string CustomerResMsg { get; set; }

    }
}
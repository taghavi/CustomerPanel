//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Bko.DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class AutoRecharge
    {
        public long Id { get; set; }
        public long BId { get; set; }
        public string CellNumber { get; set; }
        public byte SimType { get; set; }
        public byte ChargeType { get; set; }
        public Nullable<byte> Service_Id { get; set; }
        public Nullable<byte> Threshold_Id { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public byte Status { get; set; }
        public decimal RechargeAmount { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
    }
}
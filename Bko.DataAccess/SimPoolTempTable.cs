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
    
    public partial class SimPoolTempTable
    {
        public long Id { get; set; }
        public Nullable<int> BId { get; set; }
        public string Title { get; set; }
        public string CellNumber { get; set; }
        public string PreCode { get; set; }
        public string Prefix { get; set; }
        public string Postfix { get; set; }
        public string pPin { get; set; }
        public string CellType { get; set; }
        public byte CellGrade { get; set; }
        public string BillId { get; set; }
        public string PayId { get; set; }
        public Nullable<System.DateTime> ExpDate { get; set; }
        public Nullable<System.DateTime> ProductDate { get; set; }
        public string DocCode { get; set; }
        public Nullable<int> Province { get; set; }
        public long FileId { get; set; }
        public int OperatorId { get; set; }
        public decimal Price { get; set; }
        public int SimType { get; set; }
        public byte Status { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
    }
}
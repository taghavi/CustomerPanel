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
    
    public partial class PoolTempTable
    {
        public long Id { get; set; }
        public int BId { get; set; }
        public long pSeriral { get; set; }
        public string pPin { get; set; }
        public long FileId { get; set; }
        public int OperatorId { get; set; }
        public decimal Price { get; set; }
        public int ChargeType { get; set; }
        public byte Status { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<long> FwBid { get; set; }
    }
}

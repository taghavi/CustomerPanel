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
    
    public partial class Vw_OpBolton
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int OpId { get; set; }
        public byte SimType { get; set; }
        public int eRfillId { get; set; }
        public int MsrId { get; set; }
        public string Expiry { get; set; }
        public decimal OriginalAmount { get; set; }
        public decimal AffectiveAmount { get; set; }
        public string Description { get; set; }
        public string VolumeGB { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public string pType { get; set; }
        public int OrderId { get; set; }
    }
}

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
    
    public partial class AC_Account
    {
        public long Id { get; set; }
        public string TitleFa { get; set; }
        public string TitleEn { get; set; }
        public string BankAccountNO { get; set; }
        public string IBAN { get; set; }
        public decimal BalanceAmount { get; set; }
        public bool IsPostPaid { get; set; }
        public Nullable<decimal> MinAmount { get; set; }
        public Nullable<decimal> MaxAmount { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
    }
}

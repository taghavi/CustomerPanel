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
    
    public partial class PoolConfig
    {
        public long Id { get; set; }
        public long Bid { get; set; }
        public int OperatorId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int CardCount { get; set; }
        public int MinBalance { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime CreatedOn { get; set; }
    }
}

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
    
    public partial class AC_Journal
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public byte TypeCode { get; set; }
        public byte StatusCode { get; set; }
        public Nullable<long> ReferenceId { get; set; }
        public System.DateTime JournalDate { get; set; }
        public System.DateTime CreatedOn { get; set; }
    }
}

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
    
    public partial class CM_Operator
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CM_Operator()
        {
            this.CM_CellPrefix = new HashSet<CM_CellPrefix>();
        }
    
        public byte Id { get; set; }
        public string Title { get; set; }
        public byte Identifier { get; set; }
        public long BId { get; set; }
    
        public virtual BE_BusinessEntity BE_BusinessEntity { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CM_CellPrefix> CM_CellPrefix { get; set; }
    }
}

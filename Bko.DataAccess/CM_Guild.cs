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
    
    public partial class CM_Guild
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CM_Guild()
        {
            this.BE_BusinessEntity = new HashSet<BE_BusinessEntity>();
        }
    
        public int Id { get; set; }
        public Nullable<int> ParentId { get; set; }
        public string TitleFa { get; set; }
        public string TitleEn { get; set; }
        public string LocalId { get; set; }
        public string PublicId { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BE_BusinessEntity> BE_BusinessEntity { get; set; }
    }
}
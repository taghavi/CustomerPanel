using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bko.DataAccess
{
    public partial class AC_JournalDetail
    {
        [ForeignKey("JournalId")]
        public AC_Journal AC_Journal { get; set; }
    }

    public partial class CM_Guild
    {
        [ForeignKey("ParentId")]
        public CM_Guild ParentCM_Guild { get; set; }
    }
}

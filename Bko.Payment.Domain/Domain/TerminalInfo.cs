using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Bko.Payment.Domain.Domain
{
    public class TerminalInfo
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]
        public int TerminalInfoId { get; set; }
        public string TerminalProvider { get; set; }      
        public string TerminalID { get; set; }
    }
}

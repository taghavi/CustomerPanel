using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bko.DataAccess
{
    public class Actions
    {
        public int Id { get; set; }
        public string text { get; set; }
    }
    public class Contorols
    {
        public int Id { get; set; }
        public string text { get; set; }
        public List<Actions> children { get; set; }
    }
    
}

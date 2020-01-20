using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bko.DataAccess.Model
{
    public class Response<T>
    {
        public List<T> Data { get; set; }
        public long? Count { get; set; }
    }
}

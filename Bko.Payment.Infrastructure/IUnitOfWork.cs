using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bko.Payment.Infrastructure
{
    public interface IUnitOfWork
    {
       void SaveChanges();
    }
}

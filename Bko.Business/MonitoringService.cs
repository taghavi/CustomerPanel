using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bko.Business.MonitoringSrv;

namespace Bko.Business
{
    public class MonitoringService
    {
        public long? GetBusinessEntityBalance(long? bId)
        {
            //  MonitoringService srv=new MonitoringService();
            MonitorinSrvClient client = new MonitorinSrvClient();
            client.dispose();
            if (bId == null)
                return null;
            return null;
            //return client.GetBEBalance((long)bId);
        }
    }
}

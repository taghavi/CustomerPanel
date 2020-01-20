using Bko.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bko.Service.Helper
{
    public class GetInfo
    {
        public static long Get_bId()
        {
            var username = HttpContext.Current.User.Identity.Name;
            long bId;
            var bid = new UserManage().GetBid(username);
            if (bid == null)
                bId = 9;
            else
                bId = long.Parse(bid);
            return bId;
        }
    }
}
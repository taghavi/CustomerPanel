using Bko.Business;
using Bko.DataAccess;
using Bko.Service.DTOs;
using Bko.Service.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;

namespace Bko.Service.Controllers
{
    public class DashboardController : ApiController
    {
        [Authorize]
        [Route("api/dashboard/getdashboarddata")]
        //[Route("api/dd2/dd1")]
        //[Route("myapi/dd3/{bot:regex(bot[abc])}")]
        [HttpPost]
        public IEnumerable<Txn_SystemInfoD> GetDashboardData([FromBody]ParametersofDashboard parameters)
        {
            IEnumerable<Txn_SystemInfoD> result= null;
            var claims = (ClaimsIdentity)HttpContext.Current.User.Identity;
           var bid= claims.FindFirst(ClaimTypes.Sid);
            var identityName = System.Web.HttpContext.Current.User.Identity.Name;
            //if (identityName != null)
            //{
            //    var res = new ReportBlo().GetName_Bid_Balance(identityName);
            //    if(res!=null)
                 result = new ReportBlo().GetChart(parameters.Date, long.Parse(bid.Value));
            //}
            return result;
        }
    }
}

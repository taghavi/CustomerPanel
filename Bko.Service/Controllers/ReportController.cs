using Bko.Business;
using Bko.Service.DTOs;
using Bko.Service.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;

namespace Bko.Service.Controllers
{
    public class ReportController : ApiController
    {
        // [EnableCors(origins: "http://imperclient.azurewebsites.net", headers: "*", methods: "*")]
        // [ResponseType(typeof(DataAccess.RepCheckModel))]
        [Authorize]
        [Route("api/report/queryrepcheck")]
        [HttpPost]
        public IEnumerable<RepCheckDTO> QueryRepCheck([FromBody]ParametersofTransactions parameters)
        {
            //if (System.Web.HttpContext.Current.User.Identity.Name != "admin")
            //    beentitylist = new usermanage().getbid(System.Web.HttpContext.Current.User.Identity.Name);

            var claims = (ClaimsIdentity)HttpContext.Current.User.Identity;
            var bid = claims.FindFirst(ClaimTypes.Sid);
            
            short tbl = 1;
            string responsecode = null, id = null, refnum = null, beentitylist = bid.Value;
            if (parameters.TransactionStatus.ToString().Count() > 0)
            {
                int yy = (int)parameters.TransactionStatus;
                parameters.TransactionStatus = yy.ToString().ToCharArray().First();
            }
                
            var result = new ReportBlo().Sp_Rep_Check(tbl, parameters.PhoneNumber, responsecode, parameters.TransactionType, id, refnum,
                parameters.SystemTransactionId, parameters.CustomerTransactionId, beentitylist, parameters.TransactionDate, parameters.TransactionStatus,
               null, parameters.MobileCellOperator, parameters.PageIndex, parameters.PageSize);

            List<RepCheckDTO> finalResult = new List<RepCheckDTO>();
            if(result.Count() > 0)
            {
                foreach(var el in result)
                {
                    finalResult.Add(new RepCheckDTO
                    {
                        OperatorTitle = el.OperatorTitle,
                        prcode = el.Prcode,
                        OriginalAmount = el.OriginalAmount,
                        TxStatus = el.TxStatus,
                        AddData1 = el.AddData1,
                        CreatedOn = el.CreatedOn,
                        ModifiedOn = el.ModifiedOn,
                        ReserveNumber_RequestSerial = el.ReserveNumber_RequestSerial.ToString(),
                        LocalSerial = el.LocalSerial,
                        ResponseMsg = el.ResponseMsg,
                        ResponseCode = el.ResponseCode,
                        CustomerResMsg = ReturnProperResMsg(el.CURSPMSG)
                    });
                }
            }
            return finalResult;
        }

        private string ReturnProperResMsg(string msg)
        {
            if (msg == "Pin Success")
                return "رمز شارژ با موفقیت ارسال شد.";
            else
                if (msg == "TopUp Success")
                return "شارژ با موفقیت انجام گردید.";
            else
                return msg;
        }

        [Authorize]
        [Route("api/report/chargereport")]
        [HttpPost]
        public IEnumerable<DataAccess.ChargeReport> ChargeReport([FromBody]ParametersofCharges parameters)
        {
            //if (System.Web.HttpContext.Current.User.Identity.Name != "admin")
            //    beentitylist = new usermanage().getbid(System.Web.HttpContext.Current.User.Identity.Name);                    

            var claims = (ClaimsIdentity)HttpContext.Current.User.Identity;
            var bid = claims.FindFirst(ClaimTypes.Sid);
            parameters.AccountId = bid.Value;
            var result = new ReportBlo().GetChargeReport(parameters.FromDate, parameters.ToDate, parameters.AccountId);

            return result;

        }




    }
}

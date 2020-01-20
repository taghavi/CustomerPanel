using Bko.Payment.Domain.Domain;
using Bko.Payment.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;
using Bko.Service.DTOs;
using Bko.Business;
using System.Security.Claims;
using System.Web;
using System.Text;

namespace Bko.Service.Controllers
{
    public class CreditController : ApiController
    {
        [Authorize]
        [Route("api/credit/getcredit")]
        [HttpGet]
        public string GetCredit()
        {
            var identityName = System.Web.HttpContext.Current.User.Identity.Name;
            var res = new ReportBlo().GetName_Bid_Balance(identityName);

            return res.BalanceAmount.ToString();

        }

        [Route("api/credit/addcredittest")]
        [HttpPost]
        public async Task<HttpResponseMessage> AddCredittest()
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
           
            return response;

        }
        //private UnitOfWork unitOfWOrk = new UnitOfWork();
        [Authorize]
        [Route("api/credit/addcredit")]
        [HttpPost]
        public HttpResponseMessage AddCredit([FromBody]Invoice parameters)
        {
            var claims = (ClaimsIdentity)HttpContext.Current.User.Identity;
            var sid = claims.FindFirst(ClaimTypes.Sid);
            var username = claims.FindFirst(ClaimTypes.GivenName);
            string url = Url.Content("~/");
            string CallBackURL = url + "api/credit/managecallback";
            InvoiceDTO Invoicedto;
            using (var unitOfWOrk = new UnitOfWork())
            {
                parameters.UserId = int.Parse(sid.Value);
                parameters.UserName = username.Value;
                unitOfWOrk.InvoiceRepository.Add(parameters);
                unitOfWOrk.SaveChanges();
                Invoicedto = new InvoiceDTO
                {
                    TerminalID = unitOfWOrk.TerminalRepository.GetById
                    (parameters.TerminalInfoId > 0 ? parameters.TerminalInfoId : 1).TerminalID,
                    Amount = parameters.Amount,
                    CallBackUrl = CallBackURL,
                    InvoiceId = parameters.InvoiceId,
                    PayLoad = parameters.PayLoad,
                };

            }

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, Invoicedto);
            return response;

        }

        [Route("api/credit/managecallback")]
        [HttpPost]
        public HttpResponseMessage ManageCallback(PaymentResponseDTO paymentResponse)
        {
            var test= HttpContext.Current.Request.UserHostAddress;
            if (paymentResponse == null)
            {
                return RedirectUIToDefaultPage(5);
            }
            else
            {
                PaymentTransactions paymentTrans = new PaymentTransactions
                {
                    Amount = paymentResponse.amount,
                    DatePaid = paymentResponse.datepaid,
                    DigitalReceipt = paymentResponse.digitalreceipt,
                    InvoiceId = paymentResponse.invoiceid,
                    IssuerBank = paymentResponse.issuerbank,
                    Payload = paymentResponse.payload,
                    RespCode = paymentResponse.respcode,
                    RRN = paymentResponse.rrn,
                    TerminalID = paymentResponse.terminalid,
                    TraceNumber = paymentResponse.tracenumber
                };
                using (var unitOfWOrk = new UnitOfWork())
                {
                    

                    unitOfWOrk.TransRepository.Add(paymentTrans);
                    unitOfWOrk.SaveChanges();
                    if (paymentResponse.respcode < 0)
                        return RedirectUIToDefaultPage(1);

                    var duplicateDigitalReciept = unitOfWOrk.TransRepository.Get
                       (x => x.DigitalReceipt == paymentResponse.digitalreceipt);
                    if (duplicateDigitalReciept.Count() > 1)
                        return RedirectUIToDefaultPage(2);

                    if (paymentResponse.respcode == 0)
                    {
                        HttpClient client = new HttpClient();
                        client.BaseAddress = new Uri("https://mabna.shaparak.ir:8081");
                        var requestApprove = new ApprovalRequest
                        {
                            TnxId = paymentTrans.DigitalReceipt,
                            Tid = paymentTrans.TerminalID
                        };
                        var res = client.PostAsJsonAsync("/v1/PeymentApi/Advice", requestApprove).GetAwaiter().GetResult();
                        res.EnsureSuccessStatusCode();
                        var approvResponse = res.Content.ReadAsAsync<ApprovalResponse>().GetAwaiter().GetResult();
                        if (approvResponse != null)
                        {
                            paymentTrans.AdviceReturnId = approvResponse.ReturnId;
                            paymentTrans.AdviceStatus = approvResponse.Status;
                            unitOfWOrk.TransRepository.Edit(paymentTrans);
                            unitOfWOrk.SaveChanges();
                            if (approvResponse.ReturnId >= 0)
                            {
                                if (paymentTrans.Amount != approvResponse.Amount)
                                {
                                    if (RollBackPayment(ref paymentTrans, client, requestApprove))
                                    {
                                        unitOfWOrk.TransRepository.Edit(paymentTrans);
                                        unitOfWOrk.SaveChanges();
                                    }
                                    return RedirectUIToDefaultPage(3);
                                }
                                else
                                {
                                    if (!FinallAddToJournal(paymentTrans))
                                    {
                                        RollBackPayment(ref paymentTrans, client, requestApprove);
                                        return RedirectUIToDefaultPage(6);
                                    }
                                }
                            }
                            else
                                return RedirectUIToDefaultPage(4);
                        }
                        else
                            return RedirectUIToDefaultPage(5);

                    }

                }

                return RedirectUIToDefaultPage(0);

            }


            //return Request.CreateResponse(HttpStatusCode.OK);
        }

        private bool RollBackPayment(ref PaymentTransactions paymentResponse,HttpClient client, ApprovalRequest requestApprove)
        {
            try
            {
                var resRollBack = client.PostAsJsonAsync("/v1/PeymentApi/RollBack ", requestApprove).GetAwaiter().GetResult();
                resRollBack.EnsureSuccessStatusCode();
                var RollBackResponse = resRollBack.Content.ReadAsAsync<ApprovalResponse>().GetAwaiter().GetResult();
                paymentResponse.RollBackReturnId = RollBackResponse.ReturnId;
                paymentResponse.RollBackStatus = RollBackResponse.Status;

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        private bool FinallAddToJournal(PaymentTransactions paymentResponse)
        {
            //successfull Transaction now insert to main DB
            var claims = (ClaimsIdentity)HttpContext.Current.User.Identity;
            var sid = claims.FindFirst(ClaimTypes.Sid);
            string title = "افزایش اعتبار از طریق پنل مشتریان ";

            title += "با رسید دیجیتال ";
            title += paymentResponse.DigitalReceipt;

            StringBuilder details = new StringBuilder();
            details.Append("<ROOT>");
            details.AppendFormat("<JournalDetail AccountID=\"{0}\" TransactionAmount=\"{1}\"></JournalDetail>", "9", paymentResponse.Amount);
            long tt;
            long.TryParse("-" + paymentResponse.Amount.ToString(), out tt);
            details.AppendFormat("<JournalDetail AccountID=\"{0}\" TransactionAmount=\"{1}\"></JournalDetail></ROOT>", "7", tt);
            try
            {
                var finalInsert = new ReportBlo().JournalInsert(title, details.ToString());
                if (finalInsert > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        private HttpResponseMessage RedirectUIToDefaultPage(int resCode)
        {
            var response = Request.CreateResponse(HttpStatusCode.Moved);
            var clientAddress = System.Configuration.ConfigurationManager.AppSettings["clientAddress"];
            response.Headers.Location = new Uri(clientAddress +"/#/credit/add-credit" + "?transresult=" + resCode);
            return response;
        }

        [Authorize]
        [Route("api/credit/credithistory")]
        [HttpPost]
        public IEnumerable<CreditHistoryDTO> CreditHistory([FromBody]ParametersofCharges parameters)
        {
            var claims = (ClaimsIdentity)HttpContext.Current.User.Identity;
            var sid = claims.FindFirst(ClaimTypes.Sid);
            List<CreditHistoryDTO> creditHistory = new List<CreditHistoryDTO>();
            using (var unitOfWOrk = new UnitOfWork())
            {
                var res = unitOfWOrk.TransRepository.GetIncludes(x => x.Invoice).Where( x => x.Invoice.UserId == 9  //int.Parse(sid.Value) && 
                   && x.DateCreated >= DateTime.Parse(parameters.FromDate) &&
                    x.DateCreated <= DateTime.Parse(parameters.ToDate)).ToList();
                
               foreach(var r in res)
                {
                    creditHistory.Add(new CreditHistoryDTO
                    {
                        Amount = r.Amount,
                        DateCreated = r.DateCreated,
                        RespCode = r.RespCode
                    });
                }

            }

            return creditHistory;
        }

        [Route("api/credit/testaddjournal")]
        [HttpPost]
        public HttpResponseMessage testaddjournal()
        {
            PaymentTransactions paymentResponse = new PaymentTransactions();
            paymentResponse.Amount = 2000;
            paymentResponse.DigitalReceipt = "12344566789";



            if (FinallAddToJournal(paymentResponse))
                return new HttpResponseMessage(HttpStatusCode.Accepted);
            else
                return new HttpResponseMessage(HttpStatusCode.ExpectationFailed);

        }

        [Route("api/credit/testapprove")]
        [HttpPost]
        public HttpResponseMessage testApprove()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://mabna.shaparak.ir:8081/V1/");
            var requestApprove = new ApprovalRequest
            {
                TnxId = "234234334",
                Tid = 61000063
            };
            var ress = client.PostAsJsonAsync("PeymentApi/Advice", requestApprove).GetAwaiter().GetResult();

            var oi = ress.Content.ReadAsAsync<ApprovalResponse>().GetAwaiter().GetResult();
            // ress.Content.ReadAsAsync<ApprovalResponse>().RunSynchronously();
            var res = client.PostAsJsonAsync("PeymentApi/Advice", requestApprove).GetAwaiter().GetResult();
            var tt = res.Content;
            ApprovalResponse res2 = new ApprovalResponse();
            var eee = tt.ReadAsStringAsync();
            var y = tt.ReadAsStreamAsync();
            bool iii = client.PostAsJsonAsync("PeymentApi/Advice", requestApprove).GetAwaiter().GetResult().TryGetContentValue<ApprovalResponse>(out res2);
            var yy = res2;

            return new HttpResponseMessage(HttpStatusCode.ExpectationFailed);
        }

        //[HttpPost]
        //public HttpResponseMessage adddata()
        //{
        //    using(var uow= new UnitOfWork()){
        //        var tt = new TerminalInfo
        //        {
        //            TerminalID = "61000063",
        //            TerminalProvider = "مبنا کارت آریا"
        //        };

        //    }

        //}

    }
}

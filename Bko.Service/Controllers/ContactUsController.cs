using Bko.Business;
using Bko.Service.DTOs;
using Bko.Service.Helper;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Bko.Service.Controllers
{
    public class ContactUsController : ApiController
    {
        [Authorize]
        [Route("api/contactus/sendmessage")]
        [HttpPost]
        public async Task<HttpResponseMessage> SendMessage()
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            //string root = HttpContext.Current.Server.MapPath("~/Content/Upload");
            //var provider = new CustomMultipartFormDataStreamProvider(root);
                        
            try
            {                
                //List<string> attachedFiles = new List<string>();
                //await Request.Content.ReadAsMultipartAsync(provider).ContinueWith((task) =>
                //{
                //    foreach (var item in task.Result.Contents)
                //    {
                //        using (var fileStream = item.ReadAsStreamAsync().Result)
                //        {
                //            attachedFiles.Add(fileStream.ToString());
                //        }
                //    }
                //});
                // Read the form data.
                //  await Request.Content.ReadAsMultipartAsync(provider);
                // This illustrates how to get the file names.
                //List<string> attachedFiles = new List<string>();
                //foreach (MultipartFileData file in provider.FileData)
                //{
                //    Trace.WriteLine(file.Headers.ContentDisposition.FileName);
                //    Trace.WriteLine("Server file path: " + file.LocalFileName);
                //    attachedFiles.Add(file.LocalFileName);
                //}

                var provider = await Request.Content.ReadAsMultipartAsync<InMemoryMultipartFormDataStreamProvider>(new InMemoryMultipartFormDataStreamProvider());
                //access files
                IList<HttpContent> files = provider.Files;
                Dictionary<string, Stream> attachedFiles = new Dictionary<string, Stream>();
                foreach (var file in files)
                {
                    attachedFiles.Add(InMemoryMultipartFormDataStreamProvider.UnquoteToken(file.Headers.ContentDisposition.FileName),
                        await file.ReadAsStreamAsync());
                }    
               
                var Parameters = provider.FormData;
                ParametersofContactUs Contactus = Newtonsoft.Json.JsonConvert.DeserializeObject<ParametersofContactUs>(Parameters["param"]);
                
                var res = await SendEmail(Contactus, attachedFiles);
                               
                if (res.IsSuccessStatusCode)
                    return Request.CreateResponse(HttpStatusCode.OK);
                else
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError,"خطا در ارسال ایمیل!");
            }
            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }


        public async Task<HttpResponseMessage> SendEmail(ParametersofContactUs contactUs, IDictionary<string,Stream> attachedFiles)
        {
            const string EmailSubject = "پنل مشتریان";
            try
            {
                MailMessage mail = new MailMessage()
                {
                    IsBodyHtml = true,
                    From = new MailAddress("database@mobtakerancell.ir"),
                    Subject = EmailSubject,
                    //  mail.CC.Add(model.Email);
                };
                SmtpClient SmtpServer = new SmtpClient();
                SmtpServer.Port = 25;
                SmtpServer.Host = "smtp.mobtakerancell.ir";
                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential("database@mobtakerancell.ir", "123456");
                SmtpServer.EnableSsl = false;

                if (attachedFiles.Count > 0)
                    foreach (var attachedFile in attachedFiles)
                        mail.Attachments.Add(new Attachment(attachedFile.Value, attachedFile.Key));

                var userName = System.Web.HttpContext.Current.User.Identity.Name;
                var companyname = "unknown";
                if(userName!=null)
                     companyname = new ReportBlo().GetName_Bid_Balance(userName).CompanyName;

                //setting new line
                contactUs.Message = contactUs.Message.Replace("\n", "<br />");
                mail.Body = "<div style=\"font-family:B Yekan;text-align: right;font-size: 14px;\" dir=\"rtl\" >" + "موضوع: " + contactUs.Subject + "<br />" +
                    "نام کاربری: " + userName + "<br />" + "نام مشتری: " + companyname + "<br />" +
                    " واحد: " + contactUs.Unit + "<br />" + "اولویت: " + contactUs.Priority + "<br /> </div><div style=\"font-family:B Yekan;text-align: right;direction:rtl\" dir=\"rtl\">" +
                    contactUs.Message + "</div>";

                mail.To.Add("support@mobtakerancell.ir");
                await SmtpServer.SendMailAsync(mail);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}

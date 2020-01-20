using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bko.DataAccess;
using System.Web.Security;
using System.Globalization;
namespace Bko.Business
{
   
    public class MainPageModel
    {
        private aspnetdbEntities2 _mem = new aspnetdbEntities2();

        public string getlastloginDate()
        {
            return null;
            var currentUser = System.Web.HttpContext.Current.User.Identity.Name != null ? System.Web.HttpContext.Current.User.Identity.Name : String.Empty;
            MembershipUser mu = Membership.GetUser(currentUser);
            var person = _mem.aspnet_Users.Where(a => a.UserName == mu.UserName).FirstOrDefault();
            var Date = person.LastLoginate;

            return Date;
        }
        public string getNumber(string number)
        {
            NumberFormatInfo nfi = new CultureInfo("fa-IR", false).NumberFormat;
            nfi.NumberDecimalSeparator = ",";
            if (string.IsNullOrEmpty(number)) return null;
            var s = int.Parse(number);
            return s.ToString("N", nfi);    
        }
    }
}

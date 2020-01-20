using Bko.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
namespace Bko.Business
{
    public class UserManage
    {
        
        private aspnetdbEntities2 _mem;
        private MsrvMainEntities _main;
        public UserManage()
        {
            _mem = new aspnetdbEntities2();
            _main = new MsrvMainEntities();
        }

        public  string GetBid(string currentUser)
        {
            var _bid = (from op in _mem.User_bid
                        
                        where op.UserName == currentUser
                        select op)
                 .SingleOrDefault();
            if (_bid == null) return null;
            return _bid.bid;
        }
        public string GetCustomerName(string currentUser)
        {
            var bid = this.GetBid(currentUser);
            if (bid == null) return null;
            var customer = (from op in _main.BE_BusinessEntity
                            select op)
                 .ToList();
            var c = customer.FirstOrDefault(a => a.Id == long.Parse(bid));
            return c.TitleFa;
        }
        public BE_BusinessEntity GetCustomer(string currentUser)
        {
            var bid = this.GetBid(currentUser);
            if (bid == null) return null;
            var customer = (from op in _main.BE_BusinessEntity
                            select op)
                 .ToList();
            var c = customer.FirstOrDefault(a => a.Id == long.Parse(bid));
            return c;
        }
        public void Setusername( string bid,string _username)
        {
            var customer = _main.BE_BusinessEntity.ToList();
            var c = customer.FirstOrDefault(a => a.Id == long.Parse(bid));
            c.UserName = _username;
            _main.SaveChanges();
        }
        /// <summary>
        /// return the username and bid
        /// </summary>
        /// <param name="bid">long</param>
        /// <returns>User_bid</returns>
        public User_bid Getuserinf(long bid)
        {
            return _mem.User_bid.FirstOrDefault(u => u.bid == bid.ToString());
            
        }
        /// <summary>
        /// return the username and bid
        /// </summary>
        /// <param name="userName">string</param>
        /// <returns>user_bid</returns>
        public User_bid Getuserinf(string userName)
        {
            return _mem.User_bid.FirstOrDefault(u => u.UserName == userName);

        }

        //public string GetcurrentUser()
        //{
        //    var c=System.Web.htt
        //    var cu= System.Web.HttpContext.Current.User.Identity.Name != null ? System.Web.HttpContext.Current.User.Identity.Name : String.Empty
        //}
    
    }
}

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Hosting;

namespace Framework.Helpers
{
    public class Utility
    {
        public static string MapPath(string virtualPath)
        {

            try
            {
                return HttpContext.Current.Request.MapPath(virtualPath);
            }
            catch (Exception)
            {
                try
                {
                    return HostingEnvironment.MapPath(virtualPath);
                }
                catch (Exception ex)
                {
                    // ToDo Log Error
                    return "";
                }
            }
        }






    }
}
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Utility
{
    public class ConfigReader
    {
       private static NameValueCollection configuration;


        /// <summary>
        /// Initializes static members of the ConfigurationReader class. 
        /// </summary>
       static ConfigReader()
        {
            configuration = ((NameValueCollection)ConfigurationSettings.GetConfig("appSettings"));
        }  private static string GetConfigurationOrDefault(string configurationKey)
        {
            string retValue = null;
            if (configuration != null)
            {
                retValue = configuration[configurationKey];
            }

            return retValue;
        }
        public static bool ActiveAutorize
        {
            get
            {
                try
                {
                    return Convert.ToBoolean(GetConfigurationOrDefault("ActiveAutorize"));
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}

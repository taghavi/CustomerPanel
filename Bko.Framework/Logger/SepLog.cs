using System.Text;

namespace Framework.Logger
{
    public class BkoLog
    {
        private log4net.ILog _logger;

        public log4net.ILog Logger
        {
            get
            {
                _logger = log4net.LogManager.GetLogger(GetType().Name);
                log4net.Config.XmlConfigurator.Configure();
                return _logger;
            }
            set { _logger = value; }
        }

        public void Log(StringBuilder logBody)
        {
            Logger.Debug(logBody);
        }
    }
}

using System;
using System.IO;
using System.Reflection;
using System.Web;
using log4net;

namespace PropertyManager.Logger
{
    public class LogManager : ILogManager
    {
        #region Fields

        private static readonly ILogManager _logManager;

        #endregion

        #region Properties

        public static string ApplicationPath
        {
            get
            {
                string applicationPath;

                if (HttpContext.Current != null)
                {
                    // web context
                    applicationPath = HttpContext.Current.Server.MapPath(HttpRuntime.AppDomainAppVirtualPath);
                }
                else
                {
                    // non web context
                    applicationPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                }

                return applicationPath;
            }
        }

        #endregion

        #region Constructors

        static LogManager()
        {
            //var log4NetFileInfo = new FileInfo(string.Format("{0}\\{1}", ApplicationPath, "log4net.config"));
            //log4net.Config.XmlConfigurator.Configure(log4NetFileInfo);
            log4net.Config.XmlConfigurator.Configure();
            _logManager = new LogManager();
        }

        #endregion

        #region Methods

        public static ILogger GetLogger<T>()
        {
            return _logManager.GetLogger(typeof(T));
        }

        public ILogger GetLogger(Type type)
        {
            ILog logger = log4net.LogManager.GetLogger(type);

            return new Log4NetLogger(logger);
        }

        #endregion
    }
}

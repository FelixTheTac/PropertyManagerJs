using System;
using System.Linq;

namespace PropertyManager.Logger
{
    public static class LoggerExtensions
    {
        public static void Debug(this ILogger log, Func<object> getMessage)
        {
            if (!log.IsDebugEnabled)
                return;

            var logMessage = getMessage();
            log.Debug(logMessage);
        }

        public static void Debug(this ILogger log, Func<object> getMessage, Func<Exception> getException)
        {
            if (!log.IsDebugEnabled)
                return;

            var logMessage = getMessage();
            var logException = getException();
            log.Debug(logMessage, logException);
        }

        public static void Info(this ILogger log, Func<object> getMessage)
        {
            if (!log.IsInfoEnabled)
                return;

            var logMessage = getMessage();
            log.Info(logMessage);
        }

        public static void Info(this ILogger log, Func<object> getMessage, Func<Exception> getException)
        {
            if (!log.IsInfoEnabled)
                return;

            var logMessage = getMessage();
            var logException = getException();
            log.Info(logMessage, logException);
        }

        public static void Warn(this ILogger log, Func<object> getMessage)
        {
            if (!log.IsWarnEnabled)
                return;

            var logMessage = getMessage();
            log.Warn(logMessage);
        }

        public static void Warn(this ILogger log, Func<object> getMessage, Func<Exception> getException)
        {
            if (!log.IsWarnEnabled)
                return;

            var logMessage = getMessage();
            var logException = getException();
            log.Warn(logMessage, logException);
        }

        public static void Error(this ILogger log, Func<object> getMessage)
        {
            if (!log.IsErrorEnabled)
                return;

            var logMessage = getMessage();
            log.Error(logMessage);
        }

        public static void Error(this ILogger log, Func<object> getMessage, Func<Exception> getException)
        {
            if (!log.IsErrorEnabled)
                return;

            var logMessage = getMessage();
            var logException = getException();
            log.Error(logMessage, logException);
        }

        public static void Fatal(this ILogger log, Func<object> getMessage)
        {
            if (!log.IsFatalEnabled)
                return;

            var logMessage = getMessage();
            log.Fatal(logMessage);
        }

        public static void Fatal(this ILogger log, Func<object> getMessage, Func<Exception> getException)
        {
            if (!log.IsFatalEnabled)
                return;

            var logMessage = getMessage();
            var logException = getException();
            log.Fatal(logMessage, logException);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace Utilies
{
    public static class Logger
    {
        private static readonly NLog.Logger _logger = LogManager.GetCurrentClassLogger();

        public static void Log(string message)
        {
            Log(message, LogLevel.Error);
        }

        public static void Log(string message, LogLevel level)
        {
            _logger.Log(level, message);
        }

        public static void Log(Exception exception)
        {
            Log(exception, LogLevel.Error);
        }

        public static void Log(Exception exception, LogLevel level)
        {
            var message = new StringBuilder(20);

            while (exception != null)
            {
                message.Append(exception.Message);
                if (exception.InnerException != null)
                {
                    message.Append("\r\n-> ");
                }

                exception = exception.InnerException;
            }

            _logger.Log(level, message);
        }

        public static void Log(string className, string message)
        {
            Log(className, message, LogLevel.Error);
        }

        public static void Log(string className, string message, LogLevel level)
        {
            _logger.Log(level, className + ": " + message);
        }

        public static void Log(string className, Exception exception)
        {
            Log(className, exception, LogLevel.Error);
        }

        public static void Log(string className, Exception exception, LogLevel level)
        {
            var message = new StringBuilder(className + ": ");

            while (exception != null)
            {
                message.Append(exception.Message);
                if (exception.InnerException != null)
                {
                    message.Append("\r\n-> ");
                }

                exception = exception.InnerException;
            }
            _logger.Log(level, message);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilies
{
    public static class Expect
    {
        public static void ArgumentNotNull(object obj, string paramName = "")
        {
            if (obj == null)
            {
                paramName = paramName ?? string.Empty;
                throw new ArgumentNullException(paramName);
            }
        }

        public static void ArgumentNotEmptyString(string obj, string paramName = "")
        {
            if (string.IsNullOrEmpty(obj))
            {
                paramName = paramName ?? string.Empty;
                throw new ArgumentException(paramName + " is null or empty");
            }
        }

        public static void ArgumentNotWhiteSpaceString(string obj, string paramName = "")
        {
            if (string.IsNullOrWhiteSpace(obj))
            {
                paramName = paramName ?? string.Empty;
                throw new ArgumentException(paramName + " is null or write space");
            }
        }

        public static void ArgumentNotLess(int number, int limit, string paramName = "")
        {
            if (number < limit)
            {
                paramName = paramName ?? string.Empty;
                throw new ArgumentException(paramName + " is less then " + limit);
            }
        }

        public static void ArgumentNotMore(int number, int limit, string paramName = "")
        {
            if (number > limit)
            {
                paramName = paramName ?? string.Empty;
                throw new ArgumentException(paramName + " is more then " + limit);
            }
        }

        public static void NotDispose(bool disposedFlag, string objectName = "")
        {
            if (disposedFlag)
            {
                objectName = objectName ?? string.Empty;
                throw new ObjectDisposedException(objectName);
            }
        }

        public static void ArgumentSatisfies<T>(T value, Func<T, bool> condition, string violationMessage = "",
                                                string paramName = "")
        {
            if (!condition(value))
            {
                violationMessage = violationMessage ?? string.Empty;
                paramName = paramName ?? string.Empty;
                throw new ArgumentException(violationMessage, paramName);
            }
        }
    }
}

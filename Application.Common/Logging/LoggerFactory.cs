using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Logging
{
    /// <summary>
    /// Log Factory
    /// </summary>
    public static class LoggerFactory
    {
        static ILoggerFactory _currentLogFactory = null;

        /// <summary>
        /// Set the log factory to use
        /// </summary>
        /// <param name="logFactory">Log factory to use</param>
        public static void SetCurrent(ILoggerFactory logFactory)
        {
            _currentLogFactory = logFactory;
        }

        /// <summary>
        /// Create a new Log
        /// </summary>
        /// <returns>Created ILogger</returns>
        public static ILogger CreateLog()
        {
            return (_currentLogFactory != null) ? _currentLogFactory.Create() : null;
        }
    }
}

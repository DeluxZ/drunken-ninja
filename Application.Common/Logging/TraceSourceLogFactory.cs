using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Logging
{
    public class TraceSourceLogFactory : ILoggerFactory
    {
        public ILogger Create()
        {
            return new TraceSourceLog();
        }
    }
}

using Microsoft.Azure.Functions.Worker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whiteboard.Service.Services
{
    public interface IClaimsHandler
    {
        public Guid GetUserId(FunctionContext executionContext);
    }
}

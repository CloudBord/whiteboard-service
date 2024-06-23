using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whiteboard.Service.Services
{
    public interface IMessageService
    {
        void TrySendMessage<T>(string queue, T payload, out bool result);
    }
}

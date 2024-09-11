using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public interface IMessageAgent
    {
        Task SendMessageAsync(string message);
        Task<string> ReceiveMessageAsync();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class MessageAgent : IMessageAgent
    {
        private Socket Handler { get; set; }

        public MessageAgent(Socket handler)
        {
            Handler = handler;
        }

        public async Task SendMessageAsync(string message)
        {
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            _ = await Handler.SendAsync(messageBytes, SocketFlags.None);
        }

        public async Task<string> ReceiveMessageAsync()
        {
            byte[] buffer = new byte[1_024];
            int received = await Handler.ReceiveAsync(buffer, SocketFlags.None);
            return Encoding.UTF8.GetString(buffer, 0, received);
        }
    }
}

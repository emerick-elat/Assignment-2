using System.Net.Sockets;
using System.Net;
using System.Text;
using Utilities;

namespace Server
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Welcome to the SERVER");
            Console.WriteLine("Awaiting connection requests from Clients...");
            IPEndPoint ipEndPoint = await Config.GetEndPoint();
            using Socket listener = new(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            listener.Bind(ipEndPoint);
            listener.Listen(100);

            var handler = await listener.AcceptAsync();
            MessageAgent agent = new MessageAgent(handler);
            
            while (true)
            {   
                string response = await agent.ReceiveMessageAsync();

                var eom = "<|EOM|>";
                if (response.IndexOf(eom) > -1 /* is end of message */)
                {
                    Console.WriteLine("Connection Requested from a client");
                    var ackMessage = "<|ACK|>";
                    await agent.SendMessageAsync(ackMessage);
                    Console.WriteLine("Connection granted to the client");
                    break;
                }
            }

            while (true)
            {
                string m = await agent.ReceiveMessageAsync();
                Console.WriteLine($"Client: {m}");
                m = Console.ReadLine() ?? "";
                if (m is not null && m.Equals("END"))
                {
                    break;
                }
                if (m is not null)
                {
                    await agent.SendMessageAsync(m);
                }
            }
        }

    }
}

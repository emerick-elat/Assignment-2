using System.Net.Sockets;
using System.Net;
using System.Text;
using Utilities;

namespace Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Welcome to the CLIENT");
            IPEndPoint ipEndPoint = await Config.GetEndPoint();

            using Socket client = new(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            await client.ConnectAsync(ipEndPoint);
            MessageAgent agent = new MessageAgent(client);
            while (true)
            {
                // Send message.
                var message = "Connect!<|EOM|>";
                await agent.SendMessageAsync(message);
                Console.WriteLine($"Requesting connection to the server...");

                // Receive ack.
                string response = await agent.ReceiveMessageAsync();
                if (response == "<|ACK|>")
                {
                    Console.WriteLine($"Connection to the server successfull");
                    break;
                }
            }

            while (true)
            {
                string msg = Console.ReadLine() ?? "";
                if (msg is not null && msg.Equals("END"))
                {
                    break;
                }
                if (msg is not null)
                {
                    await agent.SendMessageAsync(msg);
                }
                string m = await agent.ReceiveMessageAsync();
                Console.WriteLine($"SERVER: {m}");
                
            }

            client.Shutdown(SocketShutdown.Both);
        }
    }
}

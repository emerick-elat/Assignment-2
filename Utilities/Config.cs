using System.Net;

namespace Utilities
{
    public class Config
    {
        public static async Task<IPEndPoint> GetEndPoint()
        {
            var hostName = Dns.GetHostName();
            IPHostEntry localhost = await Dns.GetHostEntryAsync(hostName);

            IPAddress localIpAddress = localhost.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(localIpAddress, 11_000);
            return ipEndPoint;
        }

    }
}

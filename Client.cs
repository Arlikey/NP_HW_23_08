using System.Net.Sockets;
using System.Text;

namespace TCP_Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] availableExchanges = { "USD_EURO", "EURO_USD", "UAH_USD", "USD_UAH", "UAH_EURO", "EURO_UAH" };
            try
            {
                TcpClient client = new TcpClient("127.0.0.1", 8888);
                NetworkStream networkStream = client.GetStream();

                Console.WriteLine("ENTER CURRENCIES TO GET EXCHANGE RATE:");
                foreach (string exchanges in availableExchanges)
                {
                    Console.WriteLine($"[ {exchanges} ]");
                }
                while (true)
                {
                    Console.Write("-> ");
                    string request = Console.ReadLine();

                    if (request.ToLower() == "exit")
                    {
                        break;
                    }

                    byte[] requestBytes = Encoding.UTF8.GetBytes(request);
                    networkStream.Write(requestBytes, 0, requestBytes.Length);

                    byte[] buffer = new byte[1024];
                    int bytesRead = networkStream.Read(buffer, 0, buffer.Length);
                    string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                    Console.WriteLine($"Server response: {response}.");
                }

                networkStream.Close();
                client.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

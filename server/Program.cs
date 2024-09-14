using Grpc.Core;
using Messages;
using server.services;

namespace server
{
    internal class Program
    {
        const int Port = 50005;

        static void Main(string[] args)
        {
            Server server = null;

            try {
                server = new Server()
                {
                   Services = {MessageServices.BindService(
                       new MessageServicesImpl())},

                   Ports = { new ServerPort("localhost", Port,
                        ServerCredentials.Insecure) }
                };
                server.Start();
                Console.WriteLine($"Server running on port {Port}");
                Console.ReadKey();
            } 
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if(server != null)
                    server.ShutdownAsync().Wait();
            }
        }
    }
}

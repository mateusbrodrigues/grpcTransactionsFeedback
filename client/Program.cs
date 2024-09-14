using Grpc.Core;
using Grpc.Net.Client;
using Messages;
using System.Threading.Tasks;

namespace client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            GrpcChannel channel = GrpcChannel.ForAddress("http://localhost:50005");

            await BiDirectionalStreamingCallAsync(channel);
        }

        private static async Task BiDirectionalStreamingCallAsync(GrpcChannel channel)
        {
            var client = new MessageServices.MessageServicesClient(channel);

            //array com valor das trxs para simulacao
            int[] transactionsArray =
            {
        100, 500, 900, 300, 1000, 2000, 1500, 350, 980, 5530, 3210, 1230
    };

            var request = client.GetMaxValueRealTime();

            var serverListenerTask = Task.Run(async () =>
            {
                await foreach (var response in request.ResponseStream.ReadAllAsync())
                {
                    Console.WriteLine(response.Resultado + ". Saldo atual: " + response.SaldoAtual);
                }
            });

            foreach (var transaction in transactionsArray)
            {
                Console.WriteLine($"Client --> Server || Enviando transação: {transaction}");
                await request.RequestStream.WriteAsync(new SingleNumberMessage()
                {
                    Value = transaction
                });
                await Task.Delay(1000);
            }

            await request.RequestStream.CompleteAsync();
            await serverListenerTask;
        }

    }
}

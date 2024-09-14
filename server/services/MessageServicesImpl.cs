using Grpc.Core;
using Messages;
using System.Threading.Tasks;

namespace server.services
{
    public class MessageServicesImpl : MessageServices.MessageServicesBase
    {
        private decimal _saldo = 5000; //saldo inicial

        public override async Task GetMaxValueRealTime(
            IAsyncStreamReader<SingleNumberMessage> requestStream,
            IServerStreamWriter<SingleResponseMessage> responseStream,
            ServerCallContext context)
        {
            await foreach (var message in requestStream.ReadAllAsync())
            {
                decimal valorTransacao = message.Value;

                
                Console.WriteLine($"Server: Processando transação: {valorTransacao}");         

                await Task.Delay(500);

                string resultadoTransacao;

                if (_saldo >= valorTransacao)
                {
                    _saldo -= valorTransacao;
                    resultadoTransacao = "Aprovada";
                }
                else
                {
                    resultadoTransacao = "Negada: Saldo Insuficiente";
                    Console.WriteLine($"Server: Error");
                }

                //envia a resposta para o client
                await responseStream.WriteAsync(new SingleResponseMessage
                {
                    Resultado = resultadoTransacao,
                    SaldoAtual = (double)_saldo
                });

                await Task.Delay(500);
            }
        }
    }
}

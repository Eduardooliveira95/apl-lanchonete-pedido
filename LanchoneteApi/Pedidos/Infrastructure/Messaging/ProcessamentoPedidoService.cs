using Confluent.Kafka;
using LanchoneteApi.Pedidos.Application.Interfaces;
using System.Text.Json;

namespace LanchoneteApi.Pedidos.Infrastructure.Messaging
{
    public class ProcessamentoPedidoService : IProcessamentoPedidoService
    {
        private readonly ProducerConfig _producerConfig = new() { BootstrapServers = "localhost:9092"};
        private readonly string _topic = "pedidos";

        public async Task ProcessarPedido(int idPedido)
        {
            using var produtor = new ProducerBuilder<string, string>(_producerConfig).Build();
            await produtor.ProduceAsync(_topic, new Message<string, string>
            { Key = idPedido.ToString(), Value = idPedido.ToString() });
        }
    }
}

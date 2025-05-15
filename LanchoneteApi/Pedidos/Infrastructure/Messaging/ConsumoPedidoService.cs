using Confluent.Kafka;
using LanchoneteApi.Pedidos.Application.Interfaces;
using LanchoneteApi.Pedidos.Domain;
using Microsoft.Extensions.Caching.Memory;

namespace LanchoneteApi.Pedidos.Infrastructure.Messaging
{
    public class ConsumoPedidoService : IConsumoPedidoService
    {
        private readonly IMemoryCache _cache;
        private string _cacheKey = "PedidoCache";
        private readonly ConsumerConfig _config = new()
        {
            BootstrapServers = "localhost:9092",
            GroupId = "pedidos",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };
        private readonly string _topic = "pedidos";

        public ConsumoPedidoService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public async Task ConsumirPedido()
        {
            var consumidor = new ConsumerBuilder<string, string>(_config).Build();
            consumidor.Subscribe(_topic);

            var resultado = consumidor.Consume();

            var idPedido = int.Parse(resultado.Message.Value);

            if (_cache.TryGetValue(_cacheKey, out Dictionary<int, Pedido>? pedidos))
            {
                foreach (var item in pedidos.Values)
                {
                    item.StatusPedido = "processado";
                    _cache.Set(idPedido, item, TimeSpan.FromMinutes(20));
                }
            }
        }
    }
}

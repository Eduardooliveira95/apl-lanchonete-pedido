using Confluent.Kafka;
using LanchoneteApi.Interfaces;
using LanchoneteApi.Models;
using Microsoft.Extensions.Caching.Memory;

namespace LanchoneteApi.Services
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

            //while (true)
            //{
                var resultado = consumidor.Consume();

                var idPedido = int.Parse(resultado.Message.Value);

                if (_cache.TryGetValue(_cacheKey, out Dictionary<int, Pedido>? pedidos))
                {
                    pedidos[idPedido].StatusPedido = "processado";
                    _cache.Set(idPedido, pedidos, TimeSpan.FromMinutes(10));
                }
            //}
        }
    }
}

using AutoMapper;
using LanchoneteApi.Models;
using LanchoneteApi.Models.Interface;
using LanchoneteApi.Models.Request;
using LanchoneteApi.Models.Response;
using Microsoft.Extensions.Caching.Memory;

namespace LanchoneteApi.Services
{
    public class PedidoService : IPedidoService
    {

        private readonly IMemoryCache _cache;
        private string _cacheKey = "PedidoCache";
        private IMapper _mapper;

        public PedidoService(IMemoryCache cache
            , IMapper mapper
            )
        {
            _cache = cache;
            _mapper = mapper;
        }

        public Pedido SalvarPedido(PedidoRequest pedidoRequest) 
        {
            Pedido NovoPedido = _mapper.Map<Pedido>(pedidoRequest);

            Dictionary<int, Pedido> pedidos;

            if (!_cache.TryGetValue(_cacheKey, out pedidos)) 
            {
                pedidos = new Dictionary<int, Pedido>();
            }

            NovoPedido.IdPedido = pedidos.Any() ? pedidos.Keys.Max() + 1 : 1;

            pedidos[NovoPedido.IdPedido] = NovoPedido;

            _cache.Set(_cacheKey, pedidos, TimeSpan.FromMinutes(10));

            return NovoPedido;
        }

        public PedidoResponse? ConsultaPedido(int idPedido) 
        {
            if (_cache.TryGetValue(_cacheKey, out Dictionary<int, Pedido>? pedidos)) 
            {
                if (pedidos != null && pedidos.ContainsKey(idPedido)) return _mapper.Map<PedidoResponse>(pedidos[idPedido]);
            }
            return null;
        }
    }
}

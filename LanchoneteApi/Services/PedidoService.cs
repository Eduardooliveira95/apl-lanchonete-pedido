using AutoMapper;
using LanchoneteApi.Interfaces;
using LanchoneteApi.Models;
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
        private ProcessamentoPedidoService _processamentoPedidoService;

        public PedidoService(
            IMemoryCache cache
            ,IMapper mapper
            , ProcessamentoPedidoService processamentoPedidoService
            )
        {
            _cache = cache;
            _mapper = mapper;
            _processamentoPedidoService = processamentoPedidoService;
        }

        public async Task<Pedido> SalvarPedido(PedidoRequest pedidoRequest) 
        {
            try
            {
                Pedido NovoPedido = _mapper.Map<Pedido>(pedidoRequest);

                NovoPedido.StatusPedido = "Pendente";

                Dictionary<int, Pedido> pedidos;

                if (!_cache.TryGetValue(_cacheKey, out pedidos))
                {
                    pedidos = new Dictionary<int, Pedido>();
                }

                NovoPedido.IdPedido = pedidos.Any() ? pedidos.Keys.Max() + 1 : 1;

                pedidos[NovoPedido.IdPedido] = NovoPedido;

                _cache.Set(_cacheKey, pedidos, TimeSpan.FromMinutes(10));

                await _processamentoPedidoService.ProcessarPedido(NovoPedido.IdPedido);

                return NovoPedido;
            }
            catch (Exception ex) 
            {
                throw new ArgumentException("Erro ao cadastrar! " + ex);
            }
        }

        public async Task<PedidoResponse>? ConsultaPedido(int idPedido)
        {
            if (_cache.TryGetValue(_cacheKey, out Dictionary<int, Pedido>? pedidos)) 
            {
                if (pedidos != null && pedidos.ContainsKey(idPedido)) return await Task.FromResult(_mapper.Map<PedidoResponse>(pedidos[idPedido]));
            }
            return null;
        }
    }
}

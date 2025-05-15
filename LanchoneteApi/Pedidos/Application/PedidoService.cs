using AutoMapper;
using LanchoneteApi.Pedidos.Application.Interfaces;
using LanchoneteApi.Pedidos.Domain;
using LanchoneteApi.Pedidos.Infrastructure.Messaging;
using LanchoneteApi.Pedidos.Presentation.Request;
using LanchoneteApi.Pedidos.Presentation.Response;
using Microsoft.Extensions.Caching.Memory;

namespace LanchoneteApi.Pedidos.Application
{
    public class PedidoService : IPedidoService
    {

        private readonly IMemoryCache _cache;
        private string _cacheKey = "PedidoCache";
        private IMapper _mapper;
        private ProcessamentoPedidoService _processamentoPedidoService;
        private ConsumoPedidoService _consumoPedidoService;

        public PedidoService(
            IMemoryCache cache,
            IMapper mapper,
            ProcessamentoPedidoService processamentoPedidoService,
            ConsumoPedidoService consumoPedidoService
            )
        {
            _cache = cache;
            _mapper = mapper;
            _processamentoPedidoService = processamentoPedidoService;
            _consumoPedidoService = consumoPedidoService;
        }

        public async Task<Pedido> CriarPedido(PedidoRequest pedidoRequest) 
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

                _cache.Set(_cacheKey, pedidos, TimeSpan.FromMinutes(20));

                await _processamentoPedidoService.ProcessarPedido(NovoPedido.IdPedido);

                Task.Run(async () => IniciarDelayConsumo());

                return NovoPedido;
            }
            catch (Exception ex) 
            {
                throw new ArgumentException("Erro ao cadastrar! " + ex);
            }
        }

        public async Task IniciarDelayConsumo() 
        {
            _ = Task.Run(async () =>
            {
                await Task.Delay(TimeSpan.FromSeconds(30));
                await _consumoPedidoService.ConsumirPedido();
            });
        } 

        public async Task<PedidoResponse?> ConsultaPedido(int idPedido)
        {
            if (_cache.TryGetValue(_cacheKey, out Dictionary<int, Pedido>? pedidos)) 
            {
                if (pedidos != null && pedidos.ContainsKey(idPedido)) return await Task.FromResult(_mapper.Map<PedidoResponse>(pedidos[idPedido]));
            }
            return null;
        }
    }
}

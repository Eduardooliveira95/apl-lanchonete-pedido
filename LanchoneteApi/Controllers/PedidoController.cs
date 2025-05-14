using LanchoneteApi.Models;
using LanchoneteApi.Models.Request;
using LanchoneteApi.Models.Response;
using LanchoneteApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace LanchoneteApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PedidoController : ControllerBase
    {
        private PedidoService _pedidoService;
        private ConsumoPedidoService _consumoPedidoService;

        public PedidoController(PedidoService pedidoService, ConsumoPedidoService consumoPedidoService)
        {
            _pedidoService = pedidoService;
            _consumoPedidoService = consumoPedidoService;
        }

        [HttpGet("/{idPedido}")]
        public async Task<PedidoResponse> ConsultaPedidos(int idPedido)
        {
            return await _pedidoService.ConsultaPedido(idPedido);
        }

        [HttpPost]
        public async Task<ActionResult<Pedido>> AdicionarPedido(PedidoRequest pedido)
        {
            var NovoPedido = await _pedidoService.SalvarPedido(pedido);

            if (NovoPedido.IdPedido == 5) 
            {
                await _consumoPedidoService.ConsumirPedido();
            }

            return NovoPedido;
        }
    }
}

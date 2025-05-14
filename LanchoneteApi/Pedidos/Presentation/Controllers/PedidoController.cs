using LanchoneteApi.Pedidos.Application;
using LanchoneteApi.Pedidos.Domain;
using LanchoneteApi.Pedidos.Infrastructure.Messaging;
using LanchoneteApi.Pedidos.Presentation.Request;
using LanchoneteApi.Pedidos.Presentation.Response;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace LanchoneteApi.Pedidos.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        public async Task<ActionResult<PedidoResponse>> ConsultaPedidos(int idPedido)
        {
            var busca = await _pedidoService.ConsultaPedido(idPedido);

            if (busca is null) return NoContent();

            return busca;
        }

        [HttpPost]
        public async Task<ActionResult<Pedido>> AdicionarPedido(PedidoRequest pedido)
        {
            var NovoPedido = await _pedidoService.SalvarPedido(pedido);
            return NovoPedido;
        }
    }
}

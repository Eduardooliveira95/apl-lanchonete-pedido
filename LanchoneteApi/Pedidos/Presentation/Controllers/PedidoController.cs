using LanchoneteApi.Pedidos.Application;
using LanchoneteApi.Pedidos.Domain;
using LanchoneteApi.Pedidos.Infrastructure.Messaging;
using LanchoneteApi.Pedidos.Presentation.Request;
using LanchoneteApi.Pedidos.Presentation.Response;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
        [SwaggerOperation(Summary = "Consulta um pedido pelo ID", Description = "Retorna idPedido e StatusPedido baseado no ID informado.")]
        [SwaggerResponse(200, "Item encontrado com sucesso")]
        [SwaggerResponse(204, "Item não encontrado")]
        public async Task<ActionResult<PedidoResponse>> ConsultaPedidos(int idPedido)
        {
            var busca = await _pedidoService.ConsultaPedido(idPedido);

            if (busca is null) return NoContent();

            return Ok(busca);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Criação do pedido", Description = "Criação do novo pedido que será processado posteriormente.")]
        [SwaggerResponse(201, "Pedido Criado com sucesso!")]
        public async Task<ActionResult<Pedido>> AdicionarPedido(PedidoRequest pedido)
        {
            var NovoPedido = await _pedidoService.CriarPedido(pedido);
            return Ok(NovoPedido);
        }
    }
}

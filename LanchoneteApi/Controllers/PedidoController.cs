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

        public PedidoController(PedidoService pedidoService)
        {
            this._pedidoService = pedidoService;
        }

        [HttpGet("/{idPedido}")]
        public async Task<PedidoResponse> ConsultaPedidos(int idPedido)
        {
            return _pedidoService.ConsultaPedido(idPedido);
        }

        [HttpPost]
        public async Task<Pedido> AdicionarPedido(PedidoRequest pedido)
        {
            return _pedidoService.SalvarPedido(pedido);
        }
    }
}

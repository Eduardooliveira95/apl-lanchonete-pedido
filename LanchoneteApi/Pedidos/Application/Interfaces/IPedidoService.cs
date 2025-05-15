using LanchoneteApi.Pedidos.Domain;
using LanchoneteApi.Pedidos.Presentation.Request;
using LanchoneteApi.Pedidos.Presentation.Response;

namespace LanchoneteApi.Pedidos.Application.Interfaces
{
    public interface IPedidoService
    {
        public Task<Pedido> CriarPedido(PedidoRequest pedido);

        public Task<PedidoResponse?> ConsultaPedido(int idPedido);
    }
}

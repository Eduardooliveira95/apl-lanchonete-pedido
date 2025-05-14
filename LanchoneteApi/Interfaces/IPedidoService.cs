using LanchoneteApi.Models;
using LanchoneteApi.Models.Request;
using LanchoneteApi.Models.Response;

namespace LanchoneteApi.Interfaces
{
    public interface IPedidoService
    {
        public Task<Pedido> SalvarPedido(PedidoRequest pedido);

        public Task<PedidoResponse?> ConsultaPedido(int idPedido);
    }
}

using LanchoneteApi.Models.Request;
using LanchoneteApi.Models.Response;

namespace LanchoneteApi.Models.Interface
{
    public interface IPedidoService
    {
        public Pedido SalvarPedido(PedidoRequest pedido);

        public PedidoResponse? ConsultaPedido(int idPedido);
    }
}

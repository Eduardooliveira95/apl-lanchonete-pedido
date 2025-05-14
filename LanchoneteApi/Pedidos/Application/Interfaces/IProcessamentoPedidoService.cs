
namespace LanchoneteApi.Pedidos.Application.Interfaces
{
    public interface IProcessamentoPedidoService
    {
        public Task ProcessarPedido(int idPedido);
    }
}
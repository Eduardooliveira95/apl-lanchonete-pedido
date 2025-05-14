using LanchoneteApi.Models.Request;
using LanchoneteApi.Models.Response;
using LanchoneteApi.Models;

namespace LanchoneteApi.Interfaces
{
    public interface IProcessamentoPedidoService
    {
        public Task ProcessarPedido(int idPedido);
    }
}
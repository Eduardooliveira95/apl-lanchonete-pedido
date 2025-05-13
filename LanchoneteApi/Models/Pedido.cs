using LanchoneteApi.Models.Request;

namespace LanchoneteApi.Models
{
    public class Pedido
    {
        public int IdPedido { get; set; }
        public string CpfCliente { set; get; }
        public string StatusPedido { set; get; }
        public List<ItensPedido> ItensPedidoCliente { get; set; }
    }
}

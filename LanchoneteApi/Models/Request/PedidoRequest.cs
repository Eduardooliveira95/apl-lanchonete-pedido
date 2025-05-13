namespace LanchoneteApi.Models.Request
{
    public class PedidoRequest
    {
        public string CpfCliente { set; get; }

        public List<ItensPedidoRequest> ItensPedidoCliente { get; set; }
    }
}

using LanchoneteApi.Models.Request;
using System.ComponentModel.DataAnnotations;

namespace LanchoneteApi.Models
{
    public class Pedido
    {
        [Key]
        public int IdPedido { get; set; }
        [Required]
        public string CpfCliente { set; get; }
        public string StatusPedido { set; get; }
        [Required]
        public List<ItensPedido> ItensPedidoCliente { get; set; }
    }
}

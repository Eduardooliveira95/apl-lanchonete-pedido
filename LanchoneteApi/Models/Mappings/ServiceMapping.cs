using AutoMapper;
using LanchoneteApi.Models.Request;
using LanchoneteApi.Models.Response;

namespace LanchoneteApi.Models.Mappings
{
    public class ServiceMapping : Profile
    {
        public ServiceMapping()
        {
            CreateMap<PedidoRequest, Pedido>();
            CreateMap<ItensPedidoRequest, ItensPedido>();
            CreateMap<Pedido, PedidoResponse>();
        }
    }
}

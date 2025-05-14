using AutoMapper;
using LanchoneteApi.Pedidos.Domain;
using LanchoneteApi.Pedidos.Presentation.Request;
using LanchoneteApi.Pedidos.Presentation.Response;

namespace LanchoneteApi.Pedidos.Application.Mappings
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

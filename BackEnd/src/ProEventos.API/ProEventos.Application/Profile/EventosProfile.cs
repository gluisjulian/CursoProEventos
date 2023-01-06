using ProEventos.Application.DTOs;
using ProEventos.Domain.Entities;

namespace ProEventos.Application.Profile
{
    public class EventosProfile : AutoMapper.Profile
    {
        public EventosProfile()
        {
            CreateMap<Evento, EventoDTO>().ReverseMap();

            CreateMap<Lote, LoteDTO>().ReverseMap();
            
            CreateMap<RedeSocial, RedeSocialDTO>().ReverseMap();
            
            CreateMap<Palestrante, PalestranteDTO>().ReverseMap();
        }
    }
}

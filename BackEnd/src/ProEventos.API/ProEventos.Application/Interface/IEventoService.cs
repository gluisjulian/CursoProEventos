using ProEventos.Application.DTOs;

namespace ProEventos.Application.Interface
{
    public interface IEventoService
    {
        Task<EventoDTO> AddEvento(EventoDTO model);
        Task<EventoDTO> UpdateEvento(int id, EventoDTO model);
        Task<bool> DeleteEvento(int id);


        Task<EventoDTO[]> GetAllEventosAsync(bool includePalestrantes = false);
        Task<EventoDTO[]> GetAllEventosByTema(string tema, bool includePalestrantes = false);
        Task<EventoDTO> GetEventoByIdAsync(int enventoId, bool includePalestrantes = false);

    }
}

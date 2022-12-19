using ProEventos.Domain.Entities;

namespace ProEventos.Application.Interface
{
    public interface IEventoService
    {
        Task<Evento> AddEvento(Evento model);
        Task<Evento> UpdateEvento(int id, Evento model);
        Task<bool> DeleteEvento(int id);


        Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false);
        Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false);
        Task<Evento> GetEventoByIdAsync(int enventoId, bool includePalestrantes = false);

    }
}

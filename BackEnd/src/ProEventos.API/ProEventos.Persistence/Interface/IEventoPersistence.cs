using ProEventos.Domain.Entities;

namespace ProEventos.Persistence.Interface
{
    public interface IEventoPersistence
    {
        Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false);
        Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false );
        Task<Evento> GetEventoByIdAsync(int enventoId, bool includePalestrantes = false);

    }
}

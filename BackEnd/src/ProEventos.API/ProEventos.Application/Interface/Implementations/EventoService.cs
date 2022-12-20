using ProEventos.Domain.Entities;
using ProEventos.Persistence.Interface;

namespace ProEventos.Application.Interface.Implementations
{
    public class EventoService : IEventoService
    {
        private readonly IEventoPersistence _eventoPersistence;
        private readonly IGeralPersistence _geralPersistence;

        public EventoService(IEventoPersistence eventoPersistence, IGeralPersistence geralPersistence)
        {
            _eventoPersistence = eventoPersistence;
            _geralPersistence = geralPersistence;
        }



        public async Task<Evento> AddEvento(Evento model)
        {
            try
            {
                _geralPersistence.Add<Evento>(model);
                if(await _geralPersistence.SaveChangesAsync()) 
                { 
                   return await _eventoPersistence.GetEventoByIdAsync(model.Id, false);
                }
                
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento> UpdateEvento(int id, Evento model)
        {
            try
            {
                var evento = await _eventoPersistence.GetEventoByIdAsync(id, false);
                if (evento == null) return null;

                model.Id = evento.Id;


                _geralPersistence.Update<Evento>(model);
                if (await _geralPersistence.SaveChangesAsync())
                {
                    return await _eventoPersistence.GetEventoByIdAsync(model.Id, false);
                }

                return null;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteEvento(int id)
        {
            try
            {
                var evento = await _eventoPersistence.GetEventoByIdAsync(id, false);
                if (evento == null) throw new Exception("Evento para delete não encontrado.");

                _geralPersistence.Delete<Evento>(evento);
                return await _geralPersistence.SaveChangesAsync();

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoPersistence.GetAllEventosAsync(includePalestrantes);
                if (eventos == null) return null;


                return eventos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento[]> GetAllEventosByTema(string tema, bool includePalestrantes = false)
        {
            try
            {
                var eventosByTema = await _eventoPersistence.GetAllEventosByTemaAsync(tema, includePalestrantes);
                if (eventosByTema == null) return null;


                return eventosByTema;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento> GetEventoByIdAsync(int enventoId, bool includePalestrantes = false)
        {
            try
            {
                var eventosById = await _eventoPersistence.GetEventoByIdAsync(enventoId, includePalestrantes);
                if (eventosById == null) return null;


                return eventosById;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

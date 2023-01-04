using AutoMapper;
using ProEventos.Application.DTOs;
using ProEventos.Domain.Entities;
using ProEventos.Persistence.Interface;

namespace ProEventos.Application.Interface.Implementations
{
    public class EventoService : IEventoService
    {
        private readonly IEventoPersistence _eventoPersistence;
        private readonly IGeralPersistence _geralPersistence;
        private readonly IMapper _mapper;

        public EventoService(IEventoPersistence eventoPersistence, IGeralPersistence geralPersistence, IMapper mapper)
        {
            _eventoPersistence = eventoPersistence;
            _geralPersistence = geralPersistence;
            _mapper = mapper;
        }



        public async Task<EventoDTO> AddEvento(EventoDTO model)
        {
            try
            {
                var evento = _mapper.Map<Evento>(model);
                
                _geralPersistence.Add<Evento>(evento);
                if(await _geralPersistence.SaveChangesAsync()) 
                {
                    var eventoRetorno = await _eventoPersistence.GetEventoByIdAsync(evento.Id, false);
                    return _mapper.Map<EventoDTO>(eventoRetorno);
                }
                
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<EventoDTO> UpdateEvento(int id, EventoDTO model)
        {
            try
            {
                var evento = await _eventoPersistence.GetEventoByIdAsync(id, false);
                if (evento == null) return null;

                model.Id = evento.Id;


                //Mapeia DTO para Evento
                _mapper.Map(model, evento);


                _geralPersistence.Update<Evento>(evento);
                if (await _geralPersistence.SaveChangesAsync())
                {
                    var eventoRetorno = await _eventoPersistence.GetEventoByIdAsync(evento.Id, false);
                    return _mapper.Map<EventoDTO>(eventoRetorno);
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

        public async Task<EventoDTO[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoPersistence.GetAllEventosAsync(includePalestrantes);
                if (eventos == null) return null;

                var result = _mapper.Map<EventoDTO[]>(eventos);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EventoDTO[]> GetAllEventosByTema(string tema, bool includePalestrantes = false)
        {
            try
            {
                var eventosByTema = await _eventoPersistence.GetAllEventosByTemaAsync(tema, includePalestrantes);
                if (eventosByTema == null) return null;

                var result = _mapper.Map<EventoDTO[]>(eventosByTema);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EventoDTO> GetEventoByIdAsync(int enventoId, bool includePalestrantes = false)
        {
            try
            {
                var eventoById = await _eventoPersistence.GetEventoByIdAsync(enventoId, includePalestrantes);
                if (eventoById == null) return null;


                var result = _mapper.Map<EventoDTO>(eventoById);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using ProEventos.Domain.Entities;
using ProEventos.Persistence.Context;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {
        private readonly ProEventosContext _context;


        public EventoController(ProEventosContext context)
        {
            _context = context;
        }   


        [HttpGet]
        public IEnumerable<Evento> GetAll()
        {
            return _context.Eventos.ToList();
        }


        [HttpGet("{id}")]
        public Evento GetById(int id)
        {
            return _context.Eventos.FirstOrDefault(x => x.Id == id);
        }
    }
}
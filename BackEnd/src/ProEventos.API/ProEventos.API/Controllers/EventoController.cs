using Microsoft.AspNetCore.Mvc;
using ProEventos.API.Context;
using ProEventos.API.Model;

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
    }
}
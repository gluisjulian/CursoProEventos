using Microsoft.AspNetCore.Mvc;
using ProEventos.Application.DTOs;
using ProEventos.Application.Interface;
using ProEventos.Domain.Entities;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {
        private readonly IEventoService _eventoService;


        public EventoController(IEventoService eventoService)
        {
            _eventoService = eventoService;
        }   


        [HttpGet]
        public async Task<IActionResult> GetAllEventos()
        {
            try
            {
                var eventos = await _eventoService.GetAllEventosAsync(true);
                if (eventos == null) return NotFound("Nenhum evento encontrado");

                return Ok(eventos);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,$"Erro ao tentar recuperar os eventos. Erro:{ex.Message }");
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventoById(int id)
        {
            try
            {
                var eventos = await _eventoService.GetEventoByIdAsync(id);
                if (eventos == null) return NotFound("Evento não encontrado");

                return Ok(eventos);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar o evento. Erro:{ex.Message}");
            }
        }


        [HttpGet("tema/{tema}")]
        public async Task<IActionResult> GetAllEventosByTema(string tema)
        {
            try
            {
                var eventos = await _eventoService.GetAllEventosByTema(tema);
                if (eventos == null) return NotFound("Eventos por tema não encontrados");

                return Ok(eventos);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar o evento. Erro:{ex.Message}");
            }
        }



        [HttpPost]
        public async Task<IActionResult> AddEvento(EventoDTO model)
        {
            try
            {
                var evento = await _eventoService.AddEvento(model);
                if (evento == null) return BadRequest("Erro ao adicionar o evento");

                return Ok(evento);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar adicionar o evento. Erro:{ex.Message}");
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvento(int id, EventoDTO model)
        {
            try
            {
                var evento = await _eventoService.UpdateEvento(id, model);
                if (evento == null) return BadRequest("Erro ao atualizar o evento");

                return Ok(evento);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar atualizar o evento. Erro:{ex.Message}");
            }
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteEvento(int id)
        {
            try
            {
                return await _eventoService.DeleteEvento(id) ? Ok("Evento deletado.") : BadRequest("Erro ao deletar o evento");

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar deletar o evento. Erro:{ex.Message}");
            }
        }
    }
}
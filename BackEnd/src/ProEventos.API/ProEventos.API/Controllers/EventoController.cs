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
        private readonly IWebHostEnvironment _hostEnvironment;


        public EventoController(IEventoService eventoService, IWebHostEnvironment hostEnvironment)
        {
            _eventoService = eventoService;
            _hostEnvironment = hostEnvironment;
        }   


        [HttpGet]
        public async Task<IActionResult> GetAllEventos()
        {
            try
            {
                var eventos = await _eventoService.GetAllEventosAsync(true);
                if (eventos == null) return NoContent();

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
                if (eventos == null) return NoContent();

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
                if (eventos == null) return NoContent();

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


        //POST IMAGEM
        [HttpPost("upload-image/{eventoId}")]
        public async Task<IActionResult> UploadImage(int eventoId)
        {
            try
            {
                var evento = await _eventoService.GetEventoByIdAsync(eventoId);
                if (evento == null) return NoContent();

                var file = Request.Form.Files[0];
                if(file.Length > 0) 
                {
                    DeleteImage(evento.ImagemURL);
                    evento.ImagemURL = await SaveImage(file);
                }
                var eventoRetorno = await _eventoService.UpdateEvento(eventoId, evento);

                return Ok(eventoRetorno);
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
                var eventos = await _eventoService.GetEventoByIdAsync(id);
                if (eventos == null) return NoContent();


                return await _eventoService.DeleteEvento(id) ? Ok("Evento deletado.") : throw new Exception("Ocorreu um problema não especifico ao tentar deletar o Evento");

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar deletar o evento. Erro:{ex.Message}");
            }
        }



        [NonAction]
        public async Task<string> SaveImage(IFormFile imageFile)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');

            imageName = $"{imageName}{DateTime.UtcNow.ToString("yymmssfff")}{Path.GetExtension(imageFile.FileName)}";
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, @"Resources/Images", imageName);

            using(var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }


            return imageName;
        }



        [NonAction]
        public void DeleteImage(string imageName)
        {
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, @"Resources/Images", imageName);
            if(System.IO.File.Exists(imagePath)) 
            { 
                System.IO.File.Delete(imagePath);
            }
        }

    }
}
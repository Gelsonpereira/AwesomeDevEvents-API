using AutoMapper;
using AwesomeDevEvents.Api.Entities;
using AwesomeDevEvents.Api.Models;
using AwesomeDevEvents.Api.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AwesomeDevEvents.Api.Controllers
{
    [Route("api/dev-events")]
    [ApiController]

    public class DevEventsController : ControllerBase
    {
        private readonly DevEventsDbContext _context;
        private readonly IMapper _mapper;
        public DevEventsController(
            DevEventsDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        /// <summary>
        /// Obter todos os eventos
        /// </summary>
        /// <returns>Coleção de eventos</returns>
        /// <response code="200">Sucesso</response>>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            var devEvents = _context.DevEvents.Where(d => !d.IsDeleted).ToList();

            var viewModel = _mapper.Map<List<DevEventViewModel>>(devEvents);

            return Ok(viewModel);
        }
        /// <summary>
        /// Obter um evento
        /// </summary>
        /// <param name="id">Identificador do evento</param>
        /// <returns>Dados do evento</returns>
        /// <resonse code="200">Sucesso</resonse>
        /// <resonse code="404">Não encontrado</resonse>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            var devEvents = _context.DevEvents
                .Include(de => de.Speakers)
                .SingleOrDefault(d => d.Id == id);

            if (devEvents == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<DevEventViewModel>(devEvents);

            return Ok(viewModel);
        }

        /// <summary>
        /// Cadastrar um evento
        /// </summary>
        /// <param name="input">Dados do evento</param>
        /// <returns>Objeto recém criado</returns>
        /// <response code="201">Sucesso</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Post(DevEventInputModel input)
        {
            var devEvent = _mapper.Map<DevEvents>(input);

            _context.DevEvents.Add(devEvent);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = devEvent.Id }, devEvent);

        }

        /// <summary>
        /// Atualizar um evento
        /// </summary>
        /// <param name="id">Identificador do evento</param>
        /// <param name="input">Dados do evento</param>
        /// <returns>Nada</returns>
        /// <response code="404">Não encontrado</response>
        /// <response code="204">Sucesso</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Update(int id, DevEventInputModel input)
        {
            var devEvents = _context.DevEvents.FirstOrDefault(d => d.Id == id);

            if (devEvents == null)
            {
                return NotFound();
            }

            devEvents.Update(input.Title, input.Description, input.StartDate, input.EndDate);
            _context.DevEvents.Update(devEvents);
            _context.SaveChanges();

            return NoContent();
        }

        /// <summary>
        /// Deletar um evento
        /// </summary>
        /// <param name="id">Identificador de evento</param>
        /// <returns>Nada</returns>  
        /// <response code="404">Não encontrado</response>
        /// <response code="204">Sucesso</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Delete(int id)
        {
            var devEvents = _context.DevEvents.FirstOrDefault(d => d.Id == id);

            if (devEvents == null)
            {
                return NotFound();
            }

            devEvents.Delete();

            _context.SaveChanges();
            return NoContent();
        }
        /// <summary>
        /// Cadastrar palestrantes
        /// </summary>
        /// <param name="id">Identificador do evento</param>
        /// <param name="input">Dados do palestrante</param>
        /// <returns>Nada</returns>
        /// <response code="404">Evento não encontrado</response>
        /// <response code="204">Sucesso</response>
        [HttpPost("{id}/speakers")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult PostSpeaker( int id, DevEventSpeakerInputModel input) 
        {
            var speaker = _mapper.Map<DevEventSpeaker>(input);

            speaker.DevEventId = id; 
            var devEvents = _context.DevEvents.Any(d => d.Id == id);

            if (!devEvents)
            {
                return NotFound();
            }

            _context.DevEventSpeaker.Add(speaker);
               
            _context.SaveChanges();
            return NoContent();
        }
    }
}

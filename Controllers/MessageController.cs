using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AleniaAPI.Data;
using AleniaAPI.Models;
using AleniaAPI.DTOs;

namespace AleniaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly AleniaContext _context;

        public MessageController(AleniaContext context)
        {
            _context = context;
        }

        // GET: api/Message/interimaire/{interimaireId}
        [HttpGet("interimaire/{interimaireId}")]
        public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessagesInterimaire(Guid interimaireId)
        {
            var messages = await _context.Messages
                .Where(m => m.InterimaireId == interimaireId)
                .OrderByDescending(m => m.DateEnvoi)
                .Select(m => new MessageDto
                {
                    Id = m.Id,
                    InterimaireId = m.InterimaireId,
                    Expediteur = m.Expediteur,
                    Sujet = m.Sujet,
                    Contenu = m.Contenu,
                    Categorie = m.Categorie,
                    Important = m.Important,
                    Urgent = m.Urgent,
                    DateEnvoi = m.DateEnvoi,
                    Lu = m.Lu
                })
                .ToListAsync();

            return Ok(messages);
        }

        // POST: api/Message
        [HttpPost]
        public async Task<ActionResult<MessageDto>> EnvoyerMessage(MessageDto messageDto)
        {
            var message = new Message
            {
                InterimaireId = messageDto.InterimaireId,
                Expediteur = messageDto.Expediteur,
                Sujet = messageDto.Sujet,
                Contenu = messageDto.Contenu,
                Categorie = messageDto.Categorie,
                Important = messageDto.Important ?? false,
                Urgent = messageDto.Urgent ?? false,
                DateEnvoi = DateTime.UtcNow,
                Lu = false
            };

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            var result = new MessageDto
            {
                Id = message.Id,
                InterimaireId = message.InterimaireId,
                Expediteur = message.Expediteur,
                Sujet = message.Sujet,
                Contenu = message.Contenu,
                Categorie = message.Categorie,
                Important = message.Important,
                Urgent = message.Urgent,
                DateEnvoi = message.DateEnvoi,
                Lu = message.Lu
            };

            return CreatedAtAction(nameof(GetMessage), new { id = message.Id }, result);
        }

        // GET: api/Message/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<MessageDto>> GetMessage(int id)
        {
            var message = await _context.Messages.FindAsync(id);

            if (message == null)
            {
                return NotFound();
            }

            var messageDto = new MessageDto
            {
                Id = message.Id,
                InterimaireId = message.InterimaireId,
                Expediteur = message.Expediteur,
                Sujet = message.Sujet,
                Contenu = message.Contenu,
                Categorie = message.Categorie,
                Important = message.Important,
                Urgent = message.Urgent,
                DateEnvoi = message.DateEnvoi,
                Lu = message.Lu
            };

            return Ok(messageDto);
        }

        // PUT: api/Message/{id}/marquer-lu
        [HttpPut("{id}/marquer-lu")]
        public async Task<IActionResult> MarquerCommeLu(int id)
        {
            var message = await _context.Messages.FindAsync(id);

            if (message == null)
            {
                return NotFound();
            }

            message.Lu = true;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Message/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> SupprimerMessage(int id)
        {
            var message = await _context.Messages.FindAsync(id);

            if (message == null)
            {
                return NotFound();
            }

            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

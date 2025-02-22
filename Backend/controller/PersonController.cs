using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Person.data;
using Person.Models;

namespace Person.controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonsController : ControllerBase
    {
        private readonly PersonContext _context;

        public PersonsController(PersonContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonModel>>> GetAll()
        {
            var persons = await _context.Persons.ToListAsync();
            return Ok(persons);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PersonModel>> GetById(Guid id)
        {
            var person = await _context.Persons.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            return person;
        }

        [HttpPost]
        public async Task<ActionResult<PersonModel>> CreatePerson([FromBody] PersonModel person)
        {
            _context.Persons.Add(person);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = person.Id }, person);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePerson(Guid id, [FromBody] PersonModel updatePerson)
        {
            if (id != updatePerson.Id)
            {
                return BadRequest("Id do parâmetro não confere com o corpo da requisição.");
            }

            var existingPerson = await _context.Persons.FindAsync(id);
            if (existingPerson == null)
            {
                return NotFound();
            }

            existingPerson.Name = updatePerson.Name;
            existingPerson.Age = updatePerson.Age;
            existingPerson.Biography = updatePerson.Biography;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Persons.Any(e => e.Id == id))
                    return NotFound();
                else
                    throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(Guid id)
        {
            var person = await _context.Persons.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
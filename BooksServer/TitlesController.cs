using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BooksServer.Data;
using BooksServer.Models;
using System.Text;

namespace BooksServer.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class TitlesController : Controller
    {
        private readonly BooksContext _context;

        public TitlesController(BooksContext context)
        {
            _context = context;
        }

        // GET: api/Titles
        [HttpGet]
        public IEnumerable<Title> GetTitles()
        {
            //return new Title() { Id = "1", BookTitle = "test", Copyright = "2008", EditionNumber = 3333 };

            return _context.Titles.ToList();
        }

        // GET: api/Titles/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTitle([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var title = await _context.Titles.SingleOrDefaultAsync(m => m.Id == id);

            if (title == null)
            {
                return NotFound();
            }

            return Ok(title);
        }

        // PUT: api/Titles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTitle([FromRoute] string id, [FromBody] Title title)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != title.Id)
            {
                return BadRequest();
            }

            _context.Entry(title).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TitleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Titles
        //[HttpPost]
        public async Task<IActionResult> PostTitle([FromBody] Title title)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Titles.Add(title);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTitle", new { id = title.Id }, title);
        }

        // DELETE: api/Titles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTitle([FromRoute] string id)
        {
           
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var title = await _context.Titles.SingleOrDefaultAsync(m => m.Id == id);
            if (title == null)
            {
                return NotFound();
            }

            _context.Titles.Remove(title);
            await _context.SaveChangesAsync();

            return Ok(title);
        }

        private bool TitleExists(string id)
        {
            return _context.Titles.Any(e => e.Id == id);
        }
    }
}
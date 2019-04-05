using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ArtworksWebApi.Data;
using ArtworksWebApi.Models;

namespace ArtworksWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtworksController : ControllerBase
    {
        private readonly ArtContext _context;

        public ArtworksController(ArtContext context)
        {
            _context = context;
        }

        // GET: api/Artworks
        [HttpGet]
        public IEnumerable<Artwork> GetArtworks()
        {
            return _context.Artworks;//.Include(a=>a.ArtType);
        }

        // GET: api/Artworks/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetArtwork([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var artwork = await _context.Artworks
                //.Include(a => a.ArtType)
                .Where(a=>a.ID==id)
                .FirstOrDefaultAsync();

            if (artwork == null)
            {
                return NotFound();
            }

            return Ok(artwork);
        }

        // GET: api/PatientsByDoctor
        [HttpGet("ByArtType/{id}")]
        public IEnumerable<Artwork> GetArtworksByArtType(int id)
        {
            return _context.Artworks
                //.Include(a => a.ArtType)
                .Where(a => a.ArtTypeID == id);
        }

        // PUT: api/Artworks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArtwork([FromRoute] int id, [FromBody] Artwork artwork)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != artwork.ID)
            {
                return BadRequest();
            }

            _context.Entry(artwork).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(artwork);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!artWorkExists(id))
                {
                    ModelState.AddModelError("", "Concurrency Error: Artwork has been Removed.");
                    return BadRequest(ModelState);
                }
                else
                {
                    ModelState.AddModelError("", "Concurrency Error: Artwork has been updated by another user.  Cancel and try editing the record again.");
                    return BadRequest(ModelState);
                }
            }
            catch (DbUpdateException dex)
            {
                if (dex.InnerException.Message.Contains("IX_"))
                {
                    ModelState.AddModelError("", "Unable to save changes: Duplicate Name for this type of Artwork.");
                    return BadRequest(ModelState);
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save changes to the database. Try again, and if the problem persists see your system administrator.");
                    return BadRequest(ModelState);
                }
            }

        }

        // POST: api/Artworks
        [HttpPost]
        public async Task<IActionResult> PostArtwork([FromBody] Artwork artwork)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Artworks.Add(artwork);
            try
            {
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetArtWork", new { id = artwork.ID }, artwork);
            }
            catch (DbUpdateException dex)
            {
                if (dex.InnerException.Message.Contains("IX_"))
                {
                    ModelState.AddModelError("", "Unable to save: Duplicate Name for this type of Artwork.");
                    return BadRequest(ModelState);
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save changes to the database. Try again, and if the problem persists see your system administrator.");
                    return BadRequest(ModelState);
                }
            }
        }
        // DELETE: api/Artworks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtwork([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var artWork = await _context.Artworks.FindAsync(id);

            if (artWork == null)
            {
                ModelState.AddModelError("", "Delete Error: Artwork has already been Removed.");

            }
            else
            {
                try
                {
                    _context.Artworks.Remove(artWork);
                    await _context.SaveChangesAsync();
                    return Ok(artWork);
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Delete Error: Unable to delete Artwork.");
                }
            }
            return BadRequest(ModelState);
        }
        private bool artWorkExists(int id)
        {
            return _context.Artworks.Any(e => e.ID == id);
        }
    }
}
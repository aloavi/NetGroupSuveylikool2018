using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;

namespace WebApp.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiseasesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DiseasesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Diseases
        [HttpGet]
        public IEnumerable<Disease> GetDiseases()
        {
            return _context.Diseases;
        }

        // GET: api/Diseases/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDisease([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var disease = await _context.Diseases.FindAsync(id);

            if (disease == null)
            {
                return NotFound();
            }

            return Ok(disease);
        }

        // PUT: api/Diseases/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDisease([FromRoute] int id, [FromBody] Disease disease)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != disease.DiseaseId)
            {
                return BadRequest();
            }

            _context.Entry(disease).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiseaseExists(id))
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

        // POST: api/Diseases
        [HttpPost]
        public async Task<IActionResult> PostDisease([FromBody] Disease disease)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Diseases.Add(disease);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDisease", new { id = disease.DiseaseId }, disease);
        }

        // DELETE: api/Diseases/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDisease([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var disease = await _context.Diseases.FindAsync(id);
            if (disease == null)
            {
                return NotFound();
            }

            _context.Diseases.Remove(disease);
            await _context.SaveChangesAsync();

            return Ok(disease);
        }

        private bool DiseaseExists(int id)
        {
            return _context.Diseases.Any(e => e.DiseaseId == id);
        }
    }
}
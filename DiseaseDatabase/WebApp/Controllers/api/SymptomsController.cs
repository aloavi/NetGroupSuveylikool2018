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
    public class SymptomsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SymptomsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Symptoms
        [HttpGet]
        public IEnumerable<Symptom> GetSymptoms()
        {
            return _context.Symptoms;
        }

        // GET: api/Symptoms/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSymptom([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var symptom = await _context.Symptoms.FindAsync(id);

            if (symptom == null)
            {
                return NotFound();
            }

            return Ok(symptom);
        }

        // PUT: api/Symptoms/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSymptom([FromRoute] int id, [FromBody] Symptom symptom)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != symptom.SymptomId)
            {
                return BadRequest();
            }

            _context.Entry(symptom).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SymptomExists(id))
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

        // POST: api/Symptoms
        [HttpPost]
        public async Task<IActionResult> PostSymptom([FromBody] Symptom symptom)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Symptoms.Add(symptom);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSymptom", new { id = symptom.SymptomId }, symptom);
        }

        // DELETE: api/Symptoms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSymptom([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var symptom = await _context.Symptoms.FindAsync(id);
            if (symptom == null)
            {
                return NotFound();
            }

            _context.Symptoms.Remove(symptom);
            await _context.SaveChangesAsync();

            return Ok(symptom);
        }

        private bool SymptomExists(int id)
        {
            return _context.Symptoms.Any(e => e.SymptomId == id);
        }
    }
}
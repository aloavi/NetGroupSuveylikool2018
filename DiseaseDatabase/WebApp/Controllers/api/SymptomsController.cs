using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.DTO;
using BLL.Interfaces;
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
        private readonly ISymptomService _symptomService;

        public SymptomsController(ApplicationDbContext context, ISymptomService symptomService)
        {
            _context = context;
            _symptomService = symptomService;
        }

        #region CRUD
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
        #endregion

        /// <summary>
        /// Returns the number of unique symptoms in the database
        /// </summary>
        /// <returns>The number of symptoms</returns>
        [HttpGet("count")]
        public async Task<ActionResult<int>> Count()
        {
            return await _symptomService.SymptomCountAsync();
        }

        /// <summary>
        /// Returns top 3 symptoms occuring in most diseases
        /// by alphabetic order if the symptoms occure in the same amount of diseases.
        /// </summary>
        /// <param name="take">Number of diseases to get. Defaults to 3</param>
        /// <returns>A List of Symptoms</returns>
        [HttpGet("top")]
        public async Task<List<SymptomDTO>> GetTopDiseases(int? take)
        {
            if (take == null) take = 3;
            return await _symptomService.GetTopSymptomsAsync(take.Value);
        }
    }
}
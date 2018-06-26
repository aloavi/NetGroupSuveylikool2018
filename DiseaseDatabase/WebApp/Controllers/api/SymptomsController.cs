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
    [Route("api/symptoms")]
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
        public async Task<List<SymptomDTO>> GetSymptomsAsync()
        {
            return await _symptomService.GetAllAsync();
        }

        // GET: api/Symptoms/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSymptom([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var symptom = await _symptomService.GetByIdAsync(id);

            if (symptom == null)
            {
                return NotFound();
            }

            return Ok(symptom);
        }

        // PUT: api/Symptoms/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSymptom([FromRoute] int id, [FromBody] SymptomDTO symptom)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != symptom.SymptomId)
            {
                return BadRequest();
            }

            var dbSymptom = await _symptomService.GetByIdAsync(symptom.SymptomId);
            if (dbSymptom == null)
                return NotFound();

            try
            {
                dbSymptom.SymptomName = symptom.SymptomName;
                await _symptomService.Update(dbSymptom);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await SymptomExists(id))
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
        public async Task<IActionResult> PostSymptom([FromBody] SymptomDTO symptom)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            symptom = await _symptomService.AddAsync(symptom);

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

            var symptom = await _symptomService.GetByIdAsync(id);
            if (symptom == null)
            {
                return NotFound();
            }

            await _symptomService.Remove(id);

            return Ok(symptom);
        }


        private async Task<bool> SymptomExists(int id)
        {
            return await _symptomService.ExistsAsync(id);
        }
        #endregion

        /// <summary>
        /// Returns the number of unique symptoms in the database
        /// </summary>
        /// <returns>The number of symptoms</returns>
        [HttpGet("count")]
        public async Task<int> Count()
        {
            return await _symptomService.SymptomCountAsync();
        }

        /// <summary>
        /// Returns top 3 symptoms occuring in most symptoms
        /// by alphabetic order if the symptoms occure in the same amount of symptoms.
        /// </summary>
        /// <param name="take">Number of symptoms to get. Defaults to 3</param>
        /// <returns>A List of Symptoms</returns>
        [HttpGet("top/{take}")]
        public async Task<List<SymptomDTO>> GetTopSymptoms(int? take)
        {
            if (take == null) take = 3;
            return await _symptomService.GetTopSymptomsAsync(take.Value);
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.DTO;
using BLL.Interfaces;
using Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers.api
{
    [Route("api/diseases")]
    [ApiController]
    public class DiseasesController : ControllerBase
    {
        private readonly IDiseaseService _diseaseService;
        private readonly IAppDataInitializator _dataInitializator;

        public DiseasesController(IDiseaseService diseaseService, IAppDataInitializator dataInitializator)
        {
            _diseaseService = diseaseService;
            _dataInitializator = dataInitializator;
        }

        #region CRUD
        // GET: api/Diseases
        [HttpGet]
        public async Task<List<DiseaseDTO>> GetDiseasesAsync()
        {
            return await _diseaseService.GetAllAsync();
        }

        // GET: api/Diseases/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDisease([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var disease = await _diseaseService.GetByIdAsync(id);

            if (disease == null)
            {
                return NotFound();
            }

            return Ok(disease);
        }

        // PUT: api/Diseases/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDisease([FromRoute] int id, [FromBody] DiseaseDTO disease)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != disease.DiseaseId)
            {
                return BadRequest();
            }

            var dbDisease = await _diseaseService.GetByIdAsync(disease.DiseaseId);
            if(dbDisease == null)
                return NotFound();

            try
            {
                await _diseaseService.Update(disease);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await DiseaseExists(id))
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
        public async Task<IActionResult> PostDisease([FromBody] DiseaseDTO disease)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            disease = await _diseaseService.AddAsync(disease);

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

            var disease = await _diseaseService.GetByIdAsync(id);
            if (disease == null)
            {
                return NotFound();
            }

            await _diseaseService.Remove(id);

            return Ok(disease);
        }


        private async Task<bool> DiseaseExists(int id)
        {
            return await _diseaseService.ExistsAsync(id);
        }
        #endregion

        /// <summary>
        /// Returns top 3 diseases with the most symptoms
        /// by alphabetic order if the diseases have the same amount of symptoms.
        /// </summary>
        /// <param name="take">Number of symptoms to get. Defaults to 3</param>
        /// <returns>A List of Diseases</returns>
        [HttpGet("top")]
        public async Task<List<DiseaseDTO>> GetTopDiseases([FromQuery]int? take)
        {
            if (take == null) take = 3;
            return await _diseaseService.GetTopDiseasesAsync(take.Value);
        }


        [HttpPost("csv")]
        public async Task<IActionResult> Csv([FromBody]string[] csv)
        {
            await _dataInitializator.ClearDb();
            await _dataInitializator.InitializeDbAsync(csv);
            return NoContent();
        }
    }
}
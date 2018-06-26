﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers.api
{
    [Route("api/diagnose")]
    [ApiController]
    public class DiagnoseController : ControllerBase
    {
        private readonly IDiagnoseService _diagnoseService;

        public DiagnoseController(IDiagnoseService diagnoseService)
        {
            _diagnoseService = diagnoseService;
        }

        /// <summary>
        /// Returns a list of likely diseases based on recieved symptoms
        /// </summary>
        /// <param name="symptoms">List of symptoms</param>
        /// <returns>List of diseases</returns>
        [HttpPost]
        public async Task<ActionResult<List<DiseaseDTO>>> Diagnose([FromBody] List<SymptomDTO> symptoms)
        {
            //TODO Validation

            return await _diagnoseService.DiagnoseAsync(symptoms);
        }
    }
}
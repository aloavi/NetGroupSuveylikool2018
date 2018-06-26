using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }
}
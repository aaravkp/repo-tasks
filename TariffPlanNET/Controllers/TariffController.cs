using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TariffPlanNET.Services;
using TariffPlanNET.Model;

namespace TariffPlanNET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TariffController : Controller
    {
    
        // GET api/tariff/5
        [HttpGet("{consumption}")]
        public IActionResult GetTarffDetails(string consumption)
        {
            var _tariff = new CalculateTariff();

            if (_tariff == null)
                return NoContent();

            return Ok(_tariff.TariffPlan(consumption));
        }
    }
}

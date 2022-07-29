using Microsoft.AspNetCore.Mvc;
using GeoLocationByCep.Interfaces;

namespace GeoLocationByCep.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        private readonly IBaseServices _BaseService;
        public BaseController(IBaseServices baseService)
        {
            _BaseService = baseService;
        }

        [HttpGet("{cep}")]
        public async Task<ActionResult> Get(string cep)
        {
            var getCep = await _BaseService.GetCep(cep);
            if (getCep == null) return NotFound("Cep n�o encontrado");
            var getGeoLocation = await _BaseService.GetLatAndLong(getCep);
            if (getGeoLocation == null) return NotFound("Localiza��o n�o encontrada");
            return Ok(getGeoLocation);
        }
    }
}
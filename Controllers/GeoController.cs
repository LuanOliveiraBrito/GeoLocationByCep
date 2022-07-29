using Microsoft.AspNetCore.Mvc;
using GeoLocationByCep.DTOs;
using GeoLocationByCep.Interfaces;

namespace GeoLocationByCep.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GeoController : ControllerBase
    {
        private readonly IGeoServices _GeoService;
        public GeoController(IGeoServices baseService)
        {
            _GeoService = baseService;
        }

        [HttpGet("{cep}")]
        public async Task<ActionResult<ResponseGeo>> GetGeolocationByCep(string cep)
        {
            var getCep = await _GeoService.GetCep(cep);
            if (getCep == null) return NotFound("Cep não encontrado");
            var getGeoLocation = await _GeoService.GetLatAndLong(getCep);
            if (getGeoLocation == null) return NotFound("Localização não encontrada");
            return Ok(getGeoLocation);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseGeo>> GetGeolocationByAddress(AddressDTO address)
        {
            var getGeoLocation = await _GeoService.GetLatAndLongByAddress(address);
            if (getGeoLocation == null) return NotFound("Localização não encontrada");
            return Ok(getGeoLocation);
        }
    }
}
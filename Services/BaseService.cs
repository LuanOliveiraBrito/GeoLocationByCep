using Newtonsoft.Json;
using RestSharp;
using System.Web;
using GeoLocationByCep.ViewModels;
using GeoLocationByCep.Interfaces;

namespace GeoLocationByCep.Services
{
    public class BaseService : IBaseServices
    {
        private readonly IConfiguration _configuration;
        public BaseService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string?> GetCep(string cep)
        {
            if (cep.Contains("-")) cep = cep.Replace("-", "");
            var client = new RestClient("https://viacep.com.br/");
            var request = new RestRequest($"https://viacep.com.br/ws/{cep}/json/", Method.Get);
            var response = await client.GetAsync(request);
            if (response.Content.Contains("erro")) return null;
            return response.Content;
        }
        public async Task<object?> GetLatAndLong(string arg)
        {
            var cep = JsonConvert.DeserializeObject<CepViewModel>(arg);
            string url = HttpUtility.UrlEncode($"{cep.logradouro} {cep.localidade} {cep.uf}");
            var client = new RestClient($"https://maps.googleapis.com/maps/api/geocode/");
            var request = new RestRequest($"json?address=${url}&key={_configuration["Google:Key"]}", Method.Get);
            var response = await client.GetAsync(request);
            var responseFinal = JsonConvert.DeserializeObject<GoogleResultViewModel>(response.Content);
            if (responseFinal.Status == "ZERO_RESULTS") return null;
            return new { Lat = responseFinal.Results[0].Geometry.Location.Lat, Long = responseFinal.Results[0].Geometry.Location.Lng };
        }
    }
}

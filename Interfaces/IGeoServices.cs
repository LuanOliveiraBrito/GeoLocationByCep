
using GeoLocationByCep.DTOs;
namespace GeoLocationByCep.Interfaces
{
    public interface IGeoServices
    {
        Task<string?> GetCep(string cep);
        Task<ResponseGeo?> GetLatAndLong(string arg);
        Task<ResponseGeo?> GetLatAndLongByAddress(AddressDTO arg);
    }
}

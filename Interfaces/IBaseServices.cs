
namespace GeoLocationByCep.Interfaces
{
    public interface IBaseServices
    {
        Task<string?> GetCep(string cep);
        Task<object?> GetLatAndLong(string arg);
    }
}

using System.Threading.Tasks;
using Teleport.Models;

namespace Teleport.Services.Interfaces
{
    public interface ILocationFetcher
    {
        Task<AirportLocationModel> GetAirportLocation(string locationAbbr);
    }
}

using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RestSharp;
using Teleport.Models;
using Teleport.Services.Interfaces;

namespace Teleport.Services
{
    public class LocationFetcherService : ILocationFetcher
    {
        private const string BASE_URL = "https://places-dev.cteleport.com/airports";

        private readonly RestClient _client;
        private readonly ILogger<LocationFetcherService> _logger;

        public LocationFetcherService(ILogger<LocationFetcherService> logger)
        {
            _client = new RestClient(BASE_URL);
            _logger = logger;
        }

        public async Task<AirportLocationModel> GetAirportLocation (string locationAbbr)
        {
            if (string.IsNullOrWhiteSpace(locationAbbr))
                return null;

            var request = new RestRequest(locationAbbr.ToUpper(), DataFormat.Json);
            try
            {
                var response = await _client.GetAsync<AirportLocationModel>(request);
                return response;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return null;
            }
        }
    }
}

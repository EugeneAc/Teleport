using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using RestSharp;
using Teleport.Models;
using Teleport.Services.Interfaces;

namespace Teleport.Services
{
    public class LocationFetcherService : ILocationFetcher
    {
        private const string BASE_URL = "https://places-dev.cteleport.com/airports";

        private readonly MemoryCache _cache;

        private readonly RestClient _client;
        private readonly ILogger<LocationFetcherService> _logger;

        public LocationFetcherService(ILogger<LocationFetcherService> logger, MemoryCache cache)
        {
            _client = new RestClient(BASE_URL);
            _logger = logger;
            _cache = cache;
        }

        public async Task<AirportLocationModel> GetAirportLocation (string locationAbbr)
        {
            if (string.IsNullOrWhiteSpace(locationAbbr))
                return null;

            try
            {
                if (!_cache.TryGetValue(locationAbbr, out AirportLocationModel response))
                {
                    var request = new RestRequest(locationAbbr.ToUpper(), DataFormat.Json);
                    response = await _client.GetAsync<AirportLocationModel>(request);

                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromHours(1));

                    _cache.Set(locationAbbr, response, cacheEntryOptions);
                }
                
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

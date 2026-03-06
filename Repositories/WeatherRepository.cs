using SFI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SFI.Repositories
{
    class WeatherRepository
    {
        private const string ApiKey = "NLJTaRFjdWQJbvMfFYCDhOlX3dPmClqw19llPYgL";

        public static async Task<Weather> GetWeatherAsync(string city)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://api.api-ninjas.com/");
            client.DefaultRequestHeaders.Add("X-Api-Key", ApiKey);

            var geoResponse = await client.GetAsync($"v1/geocoding?city={city}");
            if (!geoResponse.IsSuccessStatusCode)
            {
                return null;
            }

            var geoJson = await geoResponse.Content.ReadAsStringAsync();
            var geoResults = JsonSerializer.Deserialize<List<GeoResult>>(geoJson);

            if (geoResults == null || geoResults.Count == 0)
            {
                return null;
            }

            var lat = geoResults[0].latitude;
            var lon = geoResults[0].longitude;

            var weatherResponse = await client.GetAsync($"v1/weather?lat={lat}&lon={lon}");
            if (!weatherResponse.IsSuccessStatusCode)
            {
                return null;
            }

            var weatherJson = await weatherResponse.Content.ReadAsStringAsync();
            var weather = JsonSerializer.Deserialize<Weather>(weatherJson);

            return weather;
        }
    }
}

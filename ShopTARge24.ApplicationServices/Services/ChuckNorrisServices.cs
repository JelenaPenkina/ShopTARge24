using System.Text;
using ShopTARge24.Core.ServiceInterface;
using System.Text.Json;

namespace ShopTARge24.ApplicationServices.Services
{
    public class ChuckNorrisServices : IChuckNorrisServices
    {
        public async Task<ChuckNorrisJokesDto> ChuckNorrisJokesResult(ChuckNorrisJokesDto dto)
        {
            var baseUrl = "https://api.chucknorris.io/jokes/random";

            using (var httpClient = new HttpClient())
            {
                // Teeme päringu Chuck Norris API-le
                var response = await httpClient.GetAsync(baseUrl);

                response.EnsureSuccessStatusCode(); // viskab exceptioni, kui midagi on valesti

                var json = await response.Content.ReadAsStringAsync();

                // Chuck Norris API tagastab sellise JSON struktuuri:
                // {
                //   "categories": [],
                //   "created_at": "...",
                //   "icon_url": "...",
                //   "id": "...",
                //   "updated_at": "...",
                //   "url": "...",
                //   "value": "Chuck Norris joke text"
                // }

                var joke = JsonSerializer.Deserialize<ChuckNorrisJokesDto>(json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return joke;
            }
        }
    }
}

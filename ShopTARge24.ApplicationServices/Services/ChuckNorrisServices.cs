using Microsoft.EntityFrameworkCore.Metadata;
using Nancy.Responses;
using ShopTARge24.Core;
using System.Net.Http;
using System.Text;
using ShopTARge24.Core.ServiceInterface;

namespace ShopTARge24.ApplicationServices.Services
{
    public class ChuckNorrisServices : IChuckNorrisServices
    {

        public async Task<ChuckNorrisJokesDto> ChuckNorrisResult(ChuckNorrisJokesDto dto)
        {
            var baseUrl = "https://api.chucknorris.io/";

            using (var httpClient = new HttpClient())
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://api.chucknorris.io/jokes/random{apiKey}&q={dto.id}"),
                    Content = new StringContent("", Encoding.UTF8, "application/json"),
                };

            }
        }
    }
}

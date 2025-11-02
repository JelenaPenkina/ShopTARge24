using Microsoft.AspNetCore.Mvc;
using ShopTARge24.ApplicationServices.Services;
using ShopTARge24.Models.OpenWeather;

namespace ShopTARge24.Controllers
{
    public class OpenWeatherController : Controller
    {
        private readonly OpenWeatherServices _openWeatherServices;

        public OpenWeatherController(OpenWeatherServices openWeatherServices)
        {
            _openWeatherServices = openWeatherServices;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SearchCity(OpenWeatherSearchViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            var weatherData = await _openWeatherServices.GetCityWeather(model.CityName);

            if (weatherData == null)
            {
                ViewBag.ErrorMessage = "City not found or API error";
                return View("Index", model);
            }

            return View("ShowWeather", weatherData);
        }
    }
}

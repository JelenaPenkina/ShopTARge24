using Microsoft.AspNetCore.Mvc;
using ShopTARge24.Core.Dto.ChuckNorris;
using ShopTARge24.Core.ServiceInterface;
using ShopTARge24.Models.ChuckNorris;

namespace ShopTARge24.Controllers
{
    public class ChuckNorrisController : Controller
    {
        //private readonly IChuckNorrisServices _chuckNorrisServices;

        //public ChuckNorrisController(IChuckNorrisServices chuckNorrisServic)
        //{
        //    _chuckNorrisServices = chuckNorrisServic;
        //}
        //public IActionResult Index()
        //{
        //    return View();
        //}

        //[HttpGet]
        //public IActionResult Categories(string category)
        //{
        //    ChuckNorrisJokesDto dto = new();

        //    dto.Categories = category;

        //    _chuckNorrisServices.ChuckNorrisJokesResult(dto);
        //    ChuckNorrisViewModel vm = new();

        //    vm.id = dto.id;
        //    vm.value = dto.value;
        //    vm.url = dto.url;
        //    vm.iconUrl = dto.ToString.iconUrl;
        //    vm.createdAt = dto.DateTime.Now();
        //    vm.updatedAt = dto.DateTime.Now();

        //    return View(vm);
        //}
    }
}

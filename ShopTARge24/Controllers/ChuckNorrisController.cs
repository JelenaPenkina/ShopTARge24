using Microsoft.AspNetCore.Mvc;

namespace ShopTARge24.Controllers
{
    public class ChuckNorrisController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

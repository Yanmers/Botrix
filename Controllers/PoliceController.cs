using Microsoft.AspNetCore.Mvc;

namespace Botrix.Controllers
{
    public class PoliceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

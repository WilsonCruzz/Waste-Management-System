using Microsoft.AspNetCore.Mvc;

namespace waste_management_system.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

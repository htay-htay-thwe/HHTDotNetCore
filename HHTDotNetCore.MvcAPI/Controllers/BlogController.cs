using Microsoft.AspNetCore.Mvc;

namespace HHTDotNetCore.MvcAPI.Controllers
{
    public class BlogController : Controller
    {
        
        public IActionResult BlogIndex()
        {
            return View("BlogIndex");
        }
       
    }
}

using Microsoft.AspNetCore.Mvc;

namespace FamilyHubs.Idam.Api.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("/")]
        public string Index()
        {
            return "OK";
        }
    }
}

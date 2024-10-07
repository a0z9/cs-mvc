using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApp6_mvc.Models;
using static WebApp6_mvc.Models.Students;
using static System.Console;

namespace WebApp6_mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public void Err() {
            int a = 1;
            int b = 1 / (a - 1);
        
        }

        [Route("status/{id:int?}")]
        public void status(int? id)
        {
            Response.StatusCode = id ?? 500;

        }


        public IActionResult PageError(int id) { 
        
            if(id == 404) return View("404");
            ViewData["status_code"] = id;
            return View();

        }

        //[Route("idx")]
        public IActionResult Index(string id, string name)
        {
            WriteLine($"id:{id} -- name:{name}");
            return View();
        }

        public IActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Check(string id, string pass)
        {
            WriteLine($"{id} -- {pass}");
           // ViewBag.Id = id;
            var student = students.FirstOrDefault(x => x.Id == id && x.Password == pass);
            if (student is null) return LocalRedirect("LogonFailed"); 
           
            return View(model: new StudentModel(id,pass));
        }

        public IActionResult LogonFailed() => View();
      
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(model: new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApp7_models.Models;
using static WebApp7_models.Models.Students;
using static System.Console;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Primitives;

namespace WebApp7_models.Controllers
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

        public IActionResult People() => View();

        public string PeopleAdd([FromForm] People people)
        {
            var ctx = HttpContext;

            StringBuilder sb = new StringBuilder();
            
            if (ModelState.IsValid) return "model ok";

            sb.Append($"errors: {ModelState.ErrorCount}\n");

            foreach (var k in ModelState.Keys)
            {
                sb.Append($"{k}={ModelState[k].RawValue}, state: {ModelState[k].ValidationState.ToString()}\n");
            }

            sb.Append("\n");

            foreach (var d in ModelState)
            {
                if(d.Value.ValidationState == ModelValidationState.Invalid)
                {
                    sb.Append($"{d.Key}: ");
                    foreach (var err in d.Value.Errors) sb.Append($"{err.ErrorMessage}, ");
                    sb.Append("\n");
                }
               
            }

            return sb.ToString();
        }
        


        

        [Route("status/{id?}")]
        public void Status(int? id)
        {
            WriteLine($"Status code: {id}");
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

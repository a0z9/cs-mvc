using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApp8_cookiee.Models;

using static System.Console;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Cryptography;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace WebApp8_cookiee.Controllers
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

        public IActionResult EmailCheck(string email) {
            
            if (email == "admin@ikit.ru") return Json(false);
            return Json(true);
        }
            

        public IActionResult AgeCheck(DateOnly BirthDate)
        {
         
         return Json( BirthDate <= Models.People.YOUNG && BirthDate >= Models.People.OLD);
         }



        public IActionResult Peoples() => View(Resources.Peoples);
       
        public IActionResult People() => View();

        //  public string PeopleAdd([FromForm] People people)
        public IActionResult PeopleAdd([FromForm] People people)
        {
            var ctx = HttpContext;

            StringBuilder sb = new StringBuilder();

            if (ModelState.IsValid) { 
                _logger.LogInformation("Model people valid!");
                people.InternalId = Guid.NewGuid();
                int id = Resources.Increment();

                while(Resources.Peoples.FirstOrDefault( p => p.Id == id ) is not null)
                {
                    id = Resources.Increment();
                }

                
                people.Id = (int)id;
                people.Password = "111";

                Resources.Peoples.Add(people);

                return View("Peoples", Resources.Peoples); 
            }

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
                    foreach (var err in d.Value.Errors) sb.Append($"{err.ErrorMessage} ");
                    sb.Append("\n");
                }
               
            }
            _logger.LogInformation(sb.ToString());
            return View("People", people);

            //return sb.ToString();
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
        public async Task<IActionResult> Check(string email, string pass)
        {
            //if (User.Identity.IsAuthenticated) return Redirect("/");

            WriteLine($"{email} -- {pass}");
            // ViewBag.Id = id;
            People people = Resources.Peoples.FirstOrDefault(x => x.Email == email && x.Password == pass);
            if (people is null) return LocalRedirect("LogonFailed");

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, people.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, people.Role.Name)
            };

            var claimId = new ClaimsIdentity(claims);
            var claimPr = new ClaimsPrincipal(claimId);

            await HttpContext.SignInAsync(claimPr);

            return View(people);

           // return View(model: new StudentModel(id, pass));
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

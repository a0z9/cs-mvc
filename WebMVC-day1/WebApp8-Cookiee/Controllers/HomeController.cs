using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApp8_cookiee.Models;
using Microsoft.AspNetCore.Authentication.Cookies;

using static System.Console;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Cryptography;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

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


        [Authorize(Roles = "Admin")]
        public IActionResult Peoples() => View(Resources.Peoples);
        
        [Authorize(Roles = "Admin")]
        public IActionResult People() => View();

        //  public string PeopleAdd([FromForm] People people)
        [Authorize(Roles = "Admin")]
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

        public async Task<IActionResult> Logout() {
            if (User.Identity.IsAuthenticated)
            {
                // client free code...
                await HttpContext.SignOutAsync("Cookies");
            }

            return Redirect("/");
        }

        public IActionResult Delete(int id) {
            
            _logger.LogWarning($"Delete record, id={id}");
            Models.People people = Resources.Peoples.First(p => p.Id == id);
         
            if(people is not null)
            Resources.Peoples.Remove(people);

            return View(model: Resources.Peoples, viewName: "Peoples");
        }

        [HttpPost]
        public async Task<IActionResult> Check(string email, string pass, string? url)
        {
            if (User.Identity.IsAuthenticated) return Redirect("/");

            WriteLine($"{email} -- {pass}");
            // ViewBag.Id = id;
            People people = Resources.Peoples.FirstOrDefault(x => x.Email == email && x.Password == pass);
            if (people is null) return LocalRedirect("LogonFailed");

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, people.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, people.Role.Name),
                new Claim("Role", people.Role.Name)
            };

            var claimId = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
            var claimPr = new ClaimsPrincipal(claimId);

           
            await HttpContext.SignInAsync(claimPr);

            if (String.IsNullOrEmpty(url) || url == "null") url = "/";

            return Redirect(url); //View(people);

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

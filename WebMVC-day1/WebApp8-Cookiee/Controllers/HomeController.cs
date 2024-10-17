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
using WebApp8_cookiee.Models.Binders;

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
        public IActionResult PeopleAdd([FromForm] People people, string act, int? peopleId=0)
        {
            var ctx = HttpContext;
            _logger.LogInformation($"act:{act}, peopleId:{peopleId}");

            StringBuilder sb = new StringBuilder();

            if (ModelState.IsValid) { 
                _logger.LogInformation("Model people valid!");
                if (act != "update")
                {

                    people.InternalId = Guid.NewGuid();
                    int id = Resources.Increment();

                    while (Resources.Peoples.FirstOrDefault(p => p.Id == id) is not null)
                    {
                        id = Resources.Increment();
                    }


                    people.Id = (int)id;
                    people.Password = Models.People.getPassHash($"{people.Email}111");

                    Resources.Peoples.Add(people);
                }
                else {

                    People peopleOld = Resources.Peoples.FirstOrDefault(p => p.Id == peopleId);
                    if (peopleOld != null)
                    {
                        peopleOld.Email = people.Email;
                        peopleOld.Name = people.Name;
                        peopleOld.Sname = people.Sname;
                        peopleOld.BirthDate = people.BirthDate;
                        peopleOld.Password = Models.People.getPassHash($"{peopleOld.Email}{people.Password}");
                        peopleOld.Role = people.Role;
                    }

                }

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

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id) {
            
            _logger.LogWarning($"Delete record, id={id}");
            Models.People people = Resources.Peoples.First(p => p.Id == id);
         
            if(people is not null)
            Resources.Peoples.Remove(people);

            return View(model: Resources.Peoples, viewName: "Peoples");
        }


        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {

            _logger.LogWarning($"Delete record, id={id}");
            People people = Resources.Peoples.First(p => p.Id == id);

            if (people is not null)
            {
                ViewData["update"] = true;
                return View(model: people, viewName: "People");
            }
          return View("/");
        }


        [Authorize(Roles = "Admin")]
        public IActionResult PeopleDetails(int id)
        {

            _logger.LogInformation($"Show record, id={id}");
            Models.People people = Resources.Peoples.First(p => p.Id == id);

            if (people is not null)   return View(people);

            return Redirect("/");
        }



        [HttpPost]
        public async Task<IActionResult> Check(string email, string pass, string? url)
        {
            if (User.Identity.IsAuthenticated) return Redirect("/");

            WriteLine($"{email} -- {pass}");
            // ViewBag.Id = id;
            People people = Resources.Peoples.FirstOrDefault(x => x.Email == email && x.Password == Models.People.getPassHash($"{email}{pass}"));
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

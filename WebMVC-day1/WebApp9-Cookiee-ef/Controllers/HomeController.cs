using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApp9_cookiee_ef.Models;
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
using WebApp9_cookiee_ef.Models.Binders;
using WebApp9_cookiee_ef.Services;
using System.Data;
using System.Data.Common;
using WebApp9_cookiee_ef.Entities;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.Arm;

namespace WebApp9_cookiee_ef.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private iData _data;
        private UniversityDb db;
       
        public HomeController(ILogger<HomeController> logger, iData data, UniversityDb db)
        {
            _logger = logger;
            _data = data;
            this.db = db;
        }


        public void Err() {
            int a = 1;
            int b = 1 / (a - 1);
        
        }

        public IActionResult EmailCheck(string email) {
            
            if (email == "admin@ikit.ru" ||
                db.Peoples.FirstOrDefault(p => p.Email.ToLower().Trim() == email.ToLower().Trim()) is not null
                ) return Json(false);
            
            return Json(true);
        }
            

        public IActionResult AgeCheck(DateOnly BirthDate)
        {
         
         return Json( BirthDate <= Models.People.YOUNG && BirthDate >= Models.People.OLD);
         }


        [Authorize(Roles = "Admin")]
        public IActionResult Peoples() => View(db.Peoples.Include(p=>p.Role).ToList());
        
        [Authorize(Roles = "Admin")]
        public IActionResult People() => View();

        //  public string PeopleAdd([FromForm] People people)
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PeopleAdd([FromForm] People people, string act, int? peopleId=0)
        {
            var ctx = HttpContext;
            _logger.LogInformation($"act:{act}, peopleId:{peopleId}");

            StringBuilder sb = new StringBuilder();

            if (ModelState.IsValid) { 
                _logger.LogInformation("Model people valid!");
                db.Attach(people.Role);
                if (act != "update")
                {

                    people.InternalId = Guid.NewGuid();
                    people.Password = Models.People.getPassHash($"{people.Email}111");
                    
                    
                    db.Peoples.Add(people);

                    await db.SaveChangesAsync();

                }
                else {

                    People peopleOld = db.Peoples.FirstOrDefault(p => p.Id == peopleId);
                    if (peopleOld != null)
                    {
                        peopleOld.Email = people.Email;
                        peopleOld.Name = people.Name;
                        peopleOld.Sname = people.Sname;
                        peopleOld.BirthDate = people.BirthDate;
                        peopleOld.Password = Models.People.getPassHash($"{peopleOld.Email}{people.Password}");

                      
                        peopleOld.Role = people.Role;
                        db.Peoples.Update(peopleOld);
                        await db.SaveChangesAsync();
                    }

                }

                return View("Peoples", db.Peoples.Include(p=>p.Role).ToList()); 
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
            _data.Send(new byte[] { 0x11, 0x44, 0x55 });
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
        public async Task<IActionResult> Delete(int id) {
            
            _logger.LogWarning($"Delete record, id={id}");
            People people = db.Peoples.First(p => p.Id == id);

            if (people is not null)
            {
                db.Peoples.Remove(people);
                await db.SaveChangesAsync();
            }

            return View(model: db.Peoples.Include(p => p.Role).ToList(), viewName: "Peoples");
        }


        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {

            _logger.LogWarning($"Delete record, id={id}");
            People people = db.Peoples.Include(p=>p.Role).First(p => p.Id == id);

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
            People people = db.Peoples.Include(p=>p.Role).First(p => p.Id == id);

            if (people is not null)  return View(people);

            return Redirect("/");
        }



        [HttpPost]
        public async Task<IActionResult> Check(string email, string pass, string? url)
        {
            if (User.Identity.IsAuthenticated) return Redirect("/");

            WriteLine($"{email} -- {pass}");
            
            People people = db.Peoples.Include(p=>p.Role).FirstOrDefault(x => x.Email == email && x.Password == Models.People.getPassHash($"{email}{pass}"));
                        
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

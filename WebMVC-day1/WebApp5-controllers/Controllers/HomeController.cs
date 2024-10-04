using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace WebApp5_controllers.Controllers
{
    public class HomeController : Controller
    {
        [ActionName("Index")]
        public IActionResult Test()
        {
            Response.ContentType = "text/html;charset=utf-8";

            StringBuilder s = new StringBuilder();
            foreach(var h in Request.Headers)
                s.Append($"{h.Key} -- {h.Value}<br/>");

            return Content("<h2>MVC controller/Обучение ИКИТ</h2><hr/>" + 
                s.ToString()
                );
            
        }





    }
}

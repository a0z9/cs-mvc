using Microsoft.AspNetCore.Mvc;

namespace WebApp4_mvc.Controllers
{
    public class TrainingController : Controller
    {

        public string Index() {
            return "-- MVC --";
        }

        public IActionResult mvc1()
        {
            Response.ContentType = "text/html;";
            return Content("<h2> -- MVC1 -- </h2>");
        }

        public async Task mvc2()
        {
            Response.ContentType = "text/html;";
            await Response.WriteAsync("<h2> -- MVC2 -- </h2>");
        }

    }
}

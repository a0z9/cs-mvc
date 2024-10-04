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

            Response.Headers.Add("Institute", "IKIT");

            StringBuilder s = new StringBuilder();
            foreach(var h in Request.Headers)
                s.Append($"{h.Key} -- {h.Value}<br/>");
           
            s.Append("<hr/>");
            foreach (var h in Response.Headers)
                s.Append($"{h.Key} -- {h.Value}<br/>");

            return Content("<h2>MVC controller/Обучение ИКИТ</h2><hr/>" + 
                s.ToString()
                );
            
        }

        [HttpGet]
        public IActionResult Student1(string name, string department="ПромАвто")
        {

            Response.ContentType = "text/html;charset=utf-8";
            Response.Headers.Append("Institute", "IKIT");

            StringBuilder s = new StringBuilder();
            //foreach (var h in Request.Headers)
            //    s.Append($"{h.Key} -- {h.Value}<br/>");
            s.Append($"Имя студента: {name}<br/>");
            s.Append($"Факультет: {department}<br/>");
            s.Append("<hr/>");
            
            //foreach (var h in Response.Headers)
            //    s.Append($"{h.Key} -- {h.Value}<br/>");

            return Content("<h2>MVC controller/Обучение ИКИТ</h2><hr/>" +
                s.ToString()
                );

        }

        [HttpGet]
        public IActionResult Student2()
        {
            string name=Request.Query["name"];
            string department = Request.Query["department"];
            if (String.IsNullOrEmpty(department)) department = "ПромАвто";

            Response.ContentType = "text/html;charset=utf-8";
            Response.Headers.Append("Institute", "IKIT");

            StringBuilder s = new StringBuilder();
           
            s.Append($"Имя студента: {name}<br/>");
            s.Append($"Факультет: {department}<br/>");
            s.Append("<hr/>");

           
            return Content("<h2>MVC controller/Обучение ИКИТ</h2><hr/>" +
                s.ToString()
                );

        }


        public IActionResult Form()
        {
            Response.ContentType = "text/html;charset=utf-8";
            Response.Headers.Append("Institute", "IKIT");

            StringBuilder s = new StringBuilder();

            s.Append( @"
             <form method='post' action='Student3'>
             Name: <input type='text' name='Name'><br/>
             Department: <input type='text' name='Department'><br/>
             <input type='submit' value='Submit'>
             </form>");


            return Content("<h2>Форма ввода данных студента/Обучение ИКИТ</h2><hr/>" +
                s.ToString()
                );

        }
        


            [HttpGet]
        [HttpPost]
        public IActionResult Student3(Student st)
        {
            
            Response.ContentType = "text/html;charset=utf-8";
            Response.Headers.Append("Institute", "IKIT");

            StringBuilder s = new StringBuilder();

            s.Append($"Имя студента: {st.Name}<br/>");
            s.Append($"Факультет: {st.Department}<br/>");
            s.Append("<hr/>");


            return Content("<h2>MVC controller/Обучение ИКИТ</h2><hr/>" +
                s.ToString()
                );

        }


       




    }
   public class Student
    {
        public string? Name { get; set; }
        public string? Department { get; set; } = "ПромАвто";
    }
}

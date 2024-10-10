using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;
using WebApp7_models.Models;
using System.Text;
//using Newtonsoft.Json;

namespace WebApp7_models.Utils
{
    public static class CustomHtmlHelper
    {
        //public static HtmlString CustomHelper(this IHtmlHelper htmlHelper, string jsonRolesData)
        //{
        //    StringBuilder result = new StringBuilder("<ol>");
        //    List<Role> roles = JsonSerializer.Deserialize<List<Role>>(jsonRolesData);

        //    foreach (Role role in roles) result.Append($"<li>{role.Name}</li>");
        //    result.Append("</ol>");

        //    return new HtmlString(result.ToString());
        //}

        public static HtmlString RolesHelper(this IHtmlHelper htmlHelper, IEnumerable<Role> roles)
        {
            StringBuilder result = new StringBuilder("<ol>");
            //List<Role> roles = JsonSerializer.Deserialize<List<Role>>(jsonRolesData);

            foreach (Role role in roles) result.Append($"<li>{role.Name}</li>");
            result.Append("</ol>");

            return new HtmlString(result.ToString());
        }

        public static HtmlString RoleSelector(this IHtmlHelper htmlHelper, IEnumerable<Role> roles)
        {
            StringBuilder result = new StringBuilder("<select name='role'>");
            foreach (Role role in roles) result.Append($"<option value='{role.Id}'>{role.Name}</option>");
            result.Append("</select>");

            return new HtmlString(result.ToString());
        }


    }
}

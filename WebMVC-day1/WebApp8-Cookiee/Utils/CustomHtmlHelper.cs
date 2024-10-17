using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;
using WebApp8_cookiee.Models;
using System.Text;
//using Newtonsoft.Json;

namespace WebApp8_cookiee.Utils
{
    public static class CustomHtmlHelper
    {
 
        public static HtmlString RolesHelper(this IHtmlHelper htmlHelper, IEnumerable<Role> roles)
        {
            StringBuilder result = new StringBuilder("<ol>");
            //List<Role> roles = JsonSerializer.Deserialize<List<Role>>(jsonRolesData);

            foreach (Role role in roles) result.Append($"<li>{role.Name}</li>");
            result.Append("</ol>");

            return new HtmlString(result.ToString());
        }

        public static HtmlString RoleSelector(this IHtmlHelper htmlHelper, IEnumerable<Role> roles, string selected="Student")
        {
            if (String.IsNullOrEmpty(selected)) selected = "Student";
            StringBuilder result = new StringBuilder("<select name='role'>");
            foreach (Role role in roles)
            {
                if (role.Name == selected) {
                    result.Append($"<option selected value={role.Id} >{role.Name}</option>");
                }
                else
                {
                    result.Append($"<option value={role.Id} >{role.Name}</option>");
                }
            }
            
            
            result.Append("</select>");

            return new HtmlString(result.ToString());
        }


    }
}

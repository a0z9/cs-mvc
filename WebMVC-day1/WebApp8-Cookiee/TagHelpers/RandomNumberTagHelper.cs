using System;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebApp8_cookiee.TagHelpers
{
    public class RandomNumberTagHelper : TagHelper
    {

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            int min = 10, max = 1000;

            int seed = (int)DateTime.Now.Ticks;
         
            TagHelperAttribute minAttr, maxAttr;

            context.AllAttributes.TryGetAttribute("min", out minAttr);
            if (minAttr is not null) int.TryParse(minAttr.Value.ToString(), out min);

            context.AllAttributes.TryGetAttribute("max", out maxAttr);
            if (maxAttr is not null) int.TryParse(maxAttr.Value.ToString(), out max);

            output.Attributes.Remove(minAttr);
            output.Attributes.Remove(maxAttr);

            output.Attributes.SetAttribute("minInteger", min);
            output.Attributes.SetAttribute("maxInteger", max);

            output.Content.SetContent($"{new Random(seed).Next(min, max)}");
    
        }


    }
}

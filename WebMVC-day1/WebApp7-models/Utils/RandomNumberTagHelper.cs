using System;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebApp7_models.Utils
{
    public class RandomNumberTagHelper : TagHelper
    {

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            int min = 10, max = 1000;

            int seed = (int) DateTime.Now.Ticks;
            int rnd = new Random(seed).Next(min, max);

            // output.MergeAttributes()
            foreach (var item in context.AllAttributes)
            {
                Console.WriteLine($"attr:{item}");
            }

            output.Content.SetContent($"{rnd}");
            
          //  base.Process(context, output);
        }


    }
}

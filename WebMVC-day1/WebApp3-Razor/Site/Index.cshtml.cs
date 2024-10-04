using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp3_Razor.Site
{
    public class IndexModel2 : PageModel
    {
        private readonly ILogger<IndexModel2> _logger;

        public string Data { get; set; }

        public IndexModel2(ILogger<IndexModel2> logger)
        {
            _logger = logger;
            Data = $"My Model Datas.. obj: {GetHashCode():x}";
        }

        public void OnGet()
        {

        }
    }
}

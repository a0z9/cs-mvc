using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp3_Razor.Pages
{
    public class IndexModel22 : PageModel
    {
        private readonly ILogger<IndexModel22> _logger;

        public string Data { get; set; }

        public IndexModel22(ILogger<IndexModel22> logger)
        {
            _logger = logger;
            Data = $"My Model Datas.. obj: {GetHashCode():x}";
        }

        public void OnGet()
        {

        }
    }
}

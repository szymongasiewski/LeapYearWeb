using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LeapYearWeb.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace LeapYearWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        [BindProperty]
        public YearName YearName { get; set; }

        public string[] Arr;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {

                Arr = new string[3];
                Arr[0] = YearName.Name;
                Arr[1] = YearName.Year.ToString();
                Arr[2] = YearName.IsLeapYear(YearName.Year);


                List<string[]> List;

                var Data = HttpContext.Session.GetString("CheckedList");
                if (Data != null)
                    List = JsonConvert.DeserializeObject<List<string[]>>(Data);
                else
                    List = new List<string[]>();

                List.Add(Arr);
                HttpContext.Session.SetString("CheckedList", JsonConvert.SerializeObject(List));
            }
            return Page();
        }
    }
}
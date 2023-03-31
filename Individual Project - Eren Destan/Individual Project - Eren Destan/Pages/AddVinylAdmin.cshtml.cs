using Individual_Project___Eren_Destan.Data;
using Individual_Project___Eren_Destan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Individual_Project___Eren_Destan.Pages
{
    public class AddVinylAdminModel : PageModel
    {
        public ApplicationDatabaseContext _db { get; set; }

        [BindProperty]
        public Vinyl Vinyl { get; set; }
        //public Models.Member Member { get; set; } --> They both are same

        public AddVinylAdminModel(ApplicationDatabaseContext db)
        {
            _db = db;
        }
        public void OnGet()
        {
           
        }
        public IActionResult OnPost()
        {
            //return Page();
            if (ModelState.IsValid)
            {
                string message = "The album" + Vinyl.Name + "(ID:" + Vinyl.Id + ") - " + Vinyl.VinylType + "RPM from " + Vinyl.Artist + " has been added to system. " +
                    "This album's genre is " + Vinyl.Genre + " and release date is " + Vinyl.ReleaseDate + ". The price is " + Vinyl.Price + " and " + Vinyl.Stock + " in current stock.";


                ViewData["Message"] = message;

                _db.Vinyls.Add(Vinyl);
                _db.SaveChanges();

                return Page();
            }
            else
            {
                ViewData["Message"] = "Please enter all data fields";
                return Page();
            }
        }
    }
}

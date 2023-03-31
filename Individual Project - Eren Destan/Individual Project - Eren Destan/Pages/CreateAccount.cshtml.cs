using Individual_Project___Eren_Destan.Data;
using Individual_Project___Eren_Destan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Individual_Project___Eren_Destan.Pages
{
    public class CreateAccountModel : PageModel
    {
        public ApplicationDatabaseContext _db { get; set; }

        [BindProperty]
        //public Models.Member Member { get; set; } --> They both are same
        public Member Member { get; set; }

        public CreateAccountModel(ApplicationDatabaseContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                string message = "Hello " + Member.Name + ", thank you for being a part of our vintage haven! ";
                message += "You can start using your account via " + Member.Email + ".";

                ViewData["Message"] = message;
                
                _db.Members.Add(Member);
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

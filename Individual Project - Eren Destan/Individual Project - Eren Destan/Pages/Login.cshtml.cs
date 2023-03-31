using Individual_Project___Eren_Destan.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Individual_Project___Eren_Destan.Pages
{
    public class LoginModel : PageModel
    {
        //public List<MemberInfo> memberInfoList = new List<MemberInfo>();

        //[BindProperty]
        //public MailAndPassword Credentials { get; set; }

        [BindProperty]
        public Credential Credential { get; set; } 

        public void OnGet()
        {
            
        }
        

        public async Task<IActionResult> OnPostAsync() 
        {
            
            string connectionString = "Data Source=mssqlstud.fhict.local; TrustServerCertificate=True;Initial Catalog=dbi504303_indproject;User ID=dbi504303_indproject;Password=123456789";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            string query = $"SELECT * FROM Members WHERE Email = '{Credential.Email}' AND Password = '{Credential.Password}'";

            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();

            //It is going to return the view
            if (!ModelState.IsValid) return Page();

            if (reader.Read()){
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, Credential.Email)
                };
                var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                //It's going to serialize the claims principle into a string and then encrypt that string, save that as a cookie in the http context. 
                await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);

                reader.Close();
                connection.Close();

                return RedirectToPage("/UserLoggedIn");
            }
            else
            {
                reader.Close();
                connection.Close();

                return Page();
            }
        }
    }
    public class Credential
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

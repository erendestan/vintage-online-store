using Individual_Project___Eren_Destan.Data;
using Individual_Project___Eren_Destan.Models;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Individual_Project___Eren_Destan.Pages
{
    public class AddUserAdminModel : PageModel
    {
        //[BindProperty]
        //public Member Member { get; set; }
        public List<MemberInfo> memberInfoList = new List<MemberInfo>();
        
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=mssqlstud.fhict.local;TrustServerCertificate=True;Initial Catalog=dbi504303_indproject;User ID=dbi504303_indproject;Password=123456789"; 
                //"Server=LAPTOP-KM1R2KBL; Database=IndividualProjectVintageHaven; Trusted_Connection=True; TrustServerCertificate=True";
                //Trusted_Connection=True; TrustServerCertificate=True
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Members";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                MemberInfo member = new MemberInfo();
                                member.id = reader.GetInt32(0);
                                member.name = reader.GetString(1);
                                member.email = reader.GetString(2);
                                member.password = reader.GetString(3);
                                member.dateOfBirth = reader.GetDateTime(4);
                                member.address = reader.GetString(5);

                                memberInfoList.Add(member);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public class MemberInfo
        {
            public int id;
            public string name;
            public string email;
            public string password;
            public DateTime dateOfBirth;
            public string address;
        }
    }
}

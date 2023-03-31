using System.ComponentModel.DataAnnotations;

namespace Individual_Project___Eren_Destan.Models
{
    public class Member
    {
        //private List<Member> memberDetailsList; 
        public Member() { }
        public Member(string name, string email, string password, DateTime dateOfBirth, string address)
        {
            Name = name;
            Email = email;
            Password = password;
            DateOfBirth = dateOfBirth;
            Address = address;
        }


        //public int Id { get; set; }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string Address { get; set; }

        public void AddProductToFavorite(Product product)
        {
            throw new NotImplementedException();
        }
        public void RemoveProductFromFavorite(Product product)
        {
            throw new NotImplementedException();
        }

        public void AddProductToShoppingList(Product product)
        {
            throw new NotImplementedException();
        }

        public void RemoveProductFromShoppingList(Product product)
        {
            throw new NotImplementedException();
        }

    }
}

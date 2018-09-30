using System.ComponentModel.DataAnnotations;

namespace Domain.Data
{
    public class User
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string NormalisedUserName { get; set; }

        [Required]
        public string Salt { get; set; }

        [Required]
        public string Password { get; set; }

        public string FullName => FirstName + " " + LastName;
    }
}

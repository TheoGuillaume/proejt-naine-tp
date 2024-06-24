
using System.ComponentModel.DataAnnotations;

namespace Crud.Models.Domain
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Email est requis")]
        [EmailAddress(ErrorMessage = "Email non valide")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mot de passe est requis")]
        public string Password { get; set; }
    }
}
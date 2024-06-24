using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Crud.Pages
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            // Effacer la session utilisateur
            HttpContext.Session.Clear();

            // Rediriger vers la page de connexion
            return RedirectToPage("/Index");
        }
    }
}

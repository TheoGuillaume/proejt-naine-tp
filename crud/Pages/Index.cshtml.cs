// Index.cshtml.cs
using Crud.Data;
using Crud.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace crud.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly RazorPageDbContext _dbContext;

        public IndexModel(ILogger<IndexModel> logger, RazorPageDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [BindProperty]
        public User LoginUser { get; set; }

        public void OnGet()
        {
            // Effacer la session existante à chaque chargement de la page de login
            HttpContext.Session.Clear();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                // Vérification des informations de connexion dans la base de données
                var user = _dbContext.User.FirstOrDefault(u => u.Email == LoginUser.Email && u.Password == LoginUser.Password);

                if (user != null)
                {
                    // Utilisateur trouvé, enregistrer l'utilisateur connecté en session
                    HttpContext.Session.SetInt32("UserId", user.UserId);
                    HttpContext.Session.SetString("UserEmail", user.Email);

                    // Rediriger vers une page sécurisée après connexion réussie
                    return RedirectToPage("/Employees/List");
                }
                else
                {
                    // Utilisateur non trouvé, afficher un message d'erreur
                    ModelState.AddModelError(string.Empty, "Email ou mot de passe incorrect.");
                    return Page();
                }
            }

            // Si le modèle n'est pas valide, revenir à la page de login avec les erreurs
            return Page();
        }
    }
}

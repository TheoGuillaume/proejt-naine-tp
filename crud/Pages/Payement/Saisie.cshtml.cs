using Crud.Data;
using Crud.Models.Domain;
using Crud.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace Crud.Pages.Payement
{
    public class SaisieModel : PageModel
    {
        private readonly RazorPageDbContext dbContext;

        public List<Mois> Mois { get; set; }
        public List<Semaine> Semaines { get; set; }
        public List<Employee> Employees { get; set; }

        public SaisieModel(RazorPageDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [BindProperty]
        public AddPayeViewModel AddPayeRequest { get; set; }

        public void OnGet()
        {
            LoadData();
        }

        public void OnPost()
        {
            try
            {
                var existingPaye = dbContext.Paye.FirstOrDefault(p =>
                    p.EmployeeId == AddPayeRequest.EmployeeId.GetValueOrDefault() &&
                    p.annee == "2024" &&
                    p.nbSemaine == AddPayeRequest.nbSemaine &&
                    p.nbMois == AddPayeRequest.nbMois);

                if (existingPaye != null)
                {
                    ViewData["Error"] = "Cette saisie a déjà été effectuée.";
                     LoadData();
                }
                else
                {
                    var request = new Paye
                    {
                        dateOfPayment = DateTime.Now,
                        mois = "mois",
                        nbSemaine = AddPayeRequest.nbSemaine,
                        nbMois = AddPayeRequest.nbMois,
                        EmployeeId = AddPayeRequest.EmployeeId.GetValueOrDefault(),
                        HeureNuit = AddPayeRequest.HeureNuit,
                        heureDimanche = AddPayeRequest.heureDimanche,
                        heureTotal = AddPayeRequest.heureTotal,
                        annee = "2024"
                    };
                    dbContext.Paye.Add(request);
                    dbContext.SaveChanges();
                    ViewData["Message"] = "Saisie effectuée";
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ViewData["Error"] = ex.Message;
                LoadData();
            }
        }

        private void LoadData()
        {
            Mois = dbContext.Mois.ToList() ?? new List<Mois>();
            Semaines = dbContext.Semaine.ToList() ?? new List<Semaine>();
            Employees = dbContext.Employees.ToList() ?? new List<Employee>();
        }
    }
}

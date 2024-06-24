using Crud.Data;
using Crud.Models.Domain;
using Crud.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crud.Pages.Payement
{
    public class EditPayeModel : PageModel
    {
        private readonly RazorPageDbContext _dbContext;

        [BindProperty]
        public EditPayeViewModel EditPaye { get; set; }

        public List<Mois> Mois { get; set; }
        public List<Semaine> Semaines { get; set; }
        public List<Employee> Employees { get; set; }

        public EditPayeModel(RazorPageDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            LoadData();

            var payment = await _dbContext.Paye
                .FirstOrDefaultAsync(p => p.Id == id);

            if (payment == null)
            {
                return NotFound();
            }

            EditPaye = new EditPayeViewModel
            {
                Id = payment.Id,
                dateOfPayment = payment.dateOfPayment,
                heureTotal = payment.heureTotal,
                heureNuit = payment.HeureNuit,
                heureDimanche = payment.heureDimanche,
                nbSemaine = payment.nbSemaine,
                nbMois = payment.nbMois,
                mois = payment.mois,
                annee = payment.annee,
                EmployeeId = payment.EmployeeId
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            var paymentToUpdate = await _dbContext.Paye
                .FirstOrDefaultAsync(p => p.Id == EditPaye.Id);

            if (paymentToUpdate == null)
            {
                return NotFound();
            }

            // Mettre à jour les propriétés du paiement avec les valeurs du ViewModel
            paymentToUpdate.dateOfPayment = EditPaye.dateOfPayment;
            paymentToUpdate.heureTotal = EditPaye.heureTotal;
            paymentToUpdate.HeureNuit = EditPaye.heureNuit;
            paymentToUpdate.heureDimanche = EditPaye.heureDimanche;
            paymentToUpdate.nbSemaine = EditPaye.nbSemaine;
            paymentToUpdate.nbMois = EditPaye.nbMois;
            paymentToUpdate.mois = "mois";
            paymentToUpdate.annee = "2024";
            paymentToUpdate.EmployeeId = EditPaye.EmployeeId;

            try
            {
                Console.WriteLine("Informations de paiement modifiées :");
                Console.WriteLine($"Date de paiement : {EditPaye.dateOfPayment}");
                Console.WriteLine($"Heures totales : {EditPaye.heureTotal}");
                Console.WriteLine($"Heures de nuit : {EditPaye.heureNuit}");
                Console.WriteLine($"Heures de dimanche : {EditPaye.heureDimanche}");
                Console.WriteLine($"Nombre de semaines : {EditPaye.nbSemaine}");
                Console.WriteLine($"Nombre de mois : {EditPaye.nbMois}");
                Console.WriteLine($"Mois : {EditPaye.mois}");
                Console.WriteLine($"Année : {EditPaye.annee}");
                Console.WriteLine($"ID de l'employé : {EditPaye.EmployeeId}");

                await _dbContext.SaveChangesAsync();
                TempData["Message"] = "Les informations de paiement ont été mises à jour avec succès.";
                return RedirectToPage("/Payement/ListeTimeWork");
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }


        }

        private void LoadData()
        {
            Mois = _dbContext.Mois.ToList();
            Semaines = _dbContext.Semaine.ToList();
            Employees = _dbContext.Employees.ToList();
        }
    }
}

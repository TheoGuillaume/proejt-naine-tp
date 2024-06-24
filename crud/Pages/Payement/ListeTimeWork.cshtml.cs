using Microsoft.AspNetCore.Mvc.RazorPages;
using Crud.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Crud.Data;
using System.Collections.Generic;
using System.Linq;
using Crud.Models.ViewModels;
using OfficeOpenXml;
using System.Globalization;

namespace Crud.Pages.Payement
{
    public class ListTimeWorkModel : PageModel
    {
        private readonly RazorPageDbContext dbContext;

        public ListTimeWorkModel(RazorPageDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<PayeViewModel> PayeDetails { get; set; }
        public string CurrentSearch { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

        public List<Paye>  ImportPayeFromCsv(string filePath)
        {
            var payes = new List<Paye>();
            using (var reader = new StreamReader(filePath))
            {
                var header = reader.ReadLine(); // Read header line

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line?.Split(',');

                    if (values == null || values.Length < 8)
                    {
                        continue;
                    }

                    var paye = new Paye
                    {
                        Id = Guid.NewGuid(),
                        dateOfPayment = DateTime.TryParseExact(values[0], "yyyy-MM-dd", CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime dateOfPayment) ? dateOfPayment : DateTime.MinValue,
                        mois = values[1],
                        nbSemaine = int.TryParse(values[2], out int nbSemaine) ? nbSemaine : 0,
                        EmployeeId = Guid.TryParse(values[3], out Guid employeeId) ? employeeId : Guid.Empty,
                        HeureNuit = int.TryParse(values[4], out int heureNuit) ? heureNuit : 0,
                        heureDimanche = int.TryParse(values[5], out int heureDimanche) ? heureDimanche : 0,
                        nbMois = int.TryParse(values[6], out int nbMois) ? nbMois : 0,
                        heureTotal = int.TryParse(values[7], out int heureTotal) ? heureTotal : 0,
                        annee = values[8]
                    };

                    payes.Add(paye);
                }
            }

            return payes;
        }
        public async Task<IActionResult> OnPostImportCsvAsync(IFormFile csvFile)
        {
            if (csvFile == null || csvFile.Length == 0)
            {
                return Content("CSV file not selected");
            }

            var filePath = Path.GetTempFileName();
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await csvFile.CopyToAsync(stream);
            }

            Console.WriteLine(csvFile);
            // Call the ImportPayeFromCsv function
            var paye = ImportPayeFromCsv(filePath);

            // Refresh the list after import
            OnGet("", 1);

           Console.WriteLine("Csv Count:"+paye.Count());
            dbContext.Paye.AddRange(paye);
            await dbContext.SaveChangesAsync();

            return Page();
        }

        public void OnGet(string search, int currentPage = 1)
        {
            CurrentSearch = search ?? ""; // Ensure CurrentSearch is not null
            CurrentPage = currentPage;

            int pageSize = 5;

            var result = Paye.GetPayeDetails(dbContext, search, currentPage, pageSize);

            PayeDetails = result.Items ?? new List<PayeViewModel>(); // Initialize PayeDetails even if null
            TotalPages = result.TotalPages;
        }
    }

}

using Crud.Data;
using Crud.Models.ViewModels;
using OfficeOpenXml; // for working with Excel files
using System.Globalization;
using System.IO;
using System.Linq;

namespace Crud.Models.Domain
{
    public class Paye
    {
        public Guid Id { get; set; }
        public DateTime dateOfPayment { get; set; }
        public int heureTotal { get; set; }
        public int HeureNuit { get; set; }
        public int heureDimanche { get; set; }
        public int nbSemaine { get; set; }
        public int nbMois { get; set; }
        public string mois { get; set; }

        public string annee { get; set; }
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public static PagedResult<PayeViewModel> GetPayeDetails(RazorPageDbContext dbContext, string search, int page, int pageSize)
        {
            var query = from p in dbContext.Paye
                        join e in dbContext.Employees on p.EmployeeId equals e.Id
                        join m in dbContext.Mois on p.nbMois equals m.Id
                        select new PayeViewModel
                        {
                            Id = p.Id,
                            EmployeeId = e.Id,
                            Nom = e.Name,
                            HT = p.heureTotal,
                            HN = e.heureDeTravail,
                            HS = (p.heureTotal - e.heureDeTravail) < 0 ? 0 : (p.heureTotal - e.heureDeTravail),
                            HS130 = (p.heureTotal - e.heureDeTravail) < 8 ? ((p.heureTotal - e.heureDeTravail) < 0 ? 0 : (p.heureTotal - e.heureDeTravail)) : 8,
                            HS150 = (p.heureTotal - e.heureDeTravail) <= 8 ? 0 : (p.heureTotal - e.heureDeTravail) - 8,
                            nbSemaine = p.nbSemaine,
                            mois = p.mois,
                            HN30 = p.HeureNuit,
                            Hdim40 = p.heureDimanche,
                            nameMois = m.mois
                        };

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.Nom.Contains(search));
            }

            var totalItems = query.Count();
            var items = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return new PagedResult<PayeViewModel>
            {
                Items = items,
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = totalItems,
                CurrentPage = page  // Assigner la page actuelle
            };

        }

        


        public class PagedResult<T>
        {
            public List<T> Items { get; set; }
            public int PageNumber { get; set; }
            public int PageSize { get; set; }
            public int TotalItems { get; set; }
            public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
            public int CurrentPage { get; set; } // Nouvelle propriété pour la page actuelle
        }

    }
}
namespace Crud.Models.ViewModels
{
    public class AddPayeViewModel
    {
        public DateTime dateOfPayment { get; set; }
        public int heureTotal {get; set;}
        public int HeureNuit {get; set;}
        public int heureDimanche {get; set;}
        public int nbSemaine {get; set;}
        public int nbMois {get; set;}
        public string mois {get; set;}
        public string annee {get; set;}
        public Guid? EmployeeId { get; set; }
    
    }
}
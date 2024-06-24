namespace Crud.Models.ViewModels
{
    public class PayeViewModel
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public string Nom { get; set; }
        public int HT { get; set; }
        public int HN { get; set; }
        public int HS { get; set; }
        public int HS130 { get; set; }
        public int HS150 { get; set; }
        public int nbSemaine { get; set; }
        public string mois { get; set; }
        public int HN30 { get; set; }
        public int Hdim40 { get; set; }
        public string nameMois { get; set; }
    }
}

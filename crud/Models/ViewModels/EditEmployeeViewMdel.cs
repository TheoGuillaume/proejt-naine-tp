namespace Crud.Models.Domain.ViewModels
{
    public class EditEmployeeViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public long Salary { get; set; }

        public int heureDeTravail {get; set; }

        public string codeEmployee { get; set; }

        public DateTime DateOfBirth { get; set; }
         public Guid PosteId { get; set; }

        public string Department { get; set; }
    }
}
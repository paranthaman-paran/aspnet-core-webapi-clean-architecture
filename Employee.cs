namespace RepositoryPattern
{
    public class Employee
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int EmployeeID { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string? EmployeeName { get; set; }

        public string? EmployeeDescription { get; set; }

        public string? EmployeePhoneNumber { get; set; }
    }
}

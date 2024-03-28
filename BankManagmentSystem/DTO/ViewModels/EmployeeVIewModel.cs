namespace BankManagmentSystem.DTO.ViewModels
{
    public class EmployeeVIewModel
    {
        public int EmployeeId { get; set; }
        public string EmployeeFirstName { get; set; } = null!;
        public string EmployeeLastName { get; set; } = null!;
        public DateTime EmployeeDateOfBirth { get; set; }
        public long EmployeeMobileNo { get; set; }
        public string EmployeeEmailId { get; set; } = null!;
        public long EmployeeAadharNo { get; set; }
    }
}

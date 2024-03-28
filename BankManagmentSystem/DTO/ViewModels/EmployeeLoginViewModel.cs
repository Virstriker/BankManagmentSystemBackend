namespace BankManagmentSystem.DTO.ViewModels
{
    public class EmployeeLoginViewModel
    {
        public bool sucess { get; set; }
        public bool Admin { get; set; }
        public EmployeeVIewModel EmployeeVIewModel { get; set; }
        public string Token { get; set; }
    }
}

using BankManagmentSystem.DTO.AddModels;
using BankManagmentSystem.DTO.ViewModels;
using BankManagmentSystem.Models;

namespace BankManagmentSystem.Business_Logic
{
    public interface IEmployeeManager
    {
        Task<OperationViewModel> AddEmployee(EmployeeAddModel employee);
        Task<OperationViewModel> DeleteEmployee(int employeeId);
        Task<List<BankEmployee>> GetAllAdmin();
        Task<List<BankEmployee>> GetAllEmployee();
        Task<BankEmployee> GetEmployee(int employeeId);
        Task<OperationViewModel> UpdateEmployee(BankEmployee employee);
    }
}
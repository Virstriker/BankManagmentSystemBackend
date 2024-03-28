using BankManagmentSystem.DTO.AddModels;
using BankManagmentSystem.DTO.ViewModels;

namespace BankManagmentSystem.Business_Logic
{
    public interface ILoginManager
    {
        Task<EmployeeLoginViewModel> BankLogin(LoginBankModel model);
    }
}
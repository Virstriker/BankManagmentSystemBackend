using BankManagmentSystem.DTO;
using BankManagmentSystem.DTO.AddModels;
using BankManagmentSystem.DTO.ViewModels;
using BankManagmentSystem.Models;

namespace BankManagmentSystem.Business_Logic
{
    public interface IBankAccountManager
    {
        Task<List<UserViewModel>> AllUsers();
        Task<OperationViewModel> DeleteUser(long id);
        Task<List<JoinModel>> GetAllDetails(long id);
        Task<object> jointable();
        Task<OperationViewModel> OpenBankAccount(UserAddModel user);
        Task<OperationViewModel> UpdateUserData(UpdateModel update);
        Task<UserViewModel> UserMapper(BankUser user);
    }
}
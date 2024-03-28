using BankManagmentSystem.DTO.AddModels;
using BankManagmentSystem.DTO.ViewModels;

namespace BankManagmentSystem.Business_Logic
{
    public interface IAccountManager
    {
        Task<OperationViewModel> AddAccount(long id, AccountAddModel accountAdd);
        Task<OperationViewModel> DeleteAccount(long id);
        Task<List<AccountViewModel>> GetAllAccount();
        Task<List<AccountViewModel>> GetDeActiveAccount();
    }
}
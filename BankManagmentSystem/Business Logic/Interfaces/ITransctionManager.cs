using BankManagmentSystem.DTO;
using BankManagmentSystem.DTO.AddModels;
using BankManagmentSystem.DTO.ViewModels;

namespace BankManagmentSystem.Business_Logic
{
    public interface ITransactionManager
    {
        Task<OperationViewModel> DepositTransction(DepositTransctionAddModel money);
        Task<List<TransactionViewModel>> GetTransaction(long account);
        Task<OperationViewModel> TransferMoney(TransferTransctionAddModel money);
        Task<OperationViewModel> WithdrawalTransction(WithdrawalTransctionAddModel money);
    }
}
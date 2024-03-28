using BankManagmentSystem.Business_Logic;
using BankManagmentSystem.DTO;
using BankManagmentSystem.DTO.AddModels;
using BankManagmentSystem.DTO.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankManagmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin,Employee")]
    public class TransctionCnotroller : ControllerBase
    {
        private readonly ITransactionManager _transctionManager;

        public TransctionCnotroller(ITransactionManager transctionManager)
        {
            _transctionManager = transctionManager;
        }
        [HttpGet("GetAccountTransaction/{id}")]
        public async Task<List<TransactionViewModel>> AccountTransactio(int id)
        {
            var result = await _transctionManager.GetTransaction(id);
            return result;
        }

        [HttpPost("DepositMoney")]
        public async Task<OperationViewModel> DepositMoney(DepositTransctionAddModel model)
        {
            var result = await _transctionManager.DepositTransction(model);
                return result;
        }
        [HttpPost("WithdrawMoney")]
        public async Task<OperationViewModel> WithdrawMoney(WithdrawalTransctionAddModel model)
        {
            var result = await _transctionManager.WithdrawalTransction(model);
                return result;
        }
        [HttpPost("TransferMoney")]
        public async Task<OperationViewModel> TransferMoney(TransferTransctionAddModel model)
        {
            var result = await _transctionManager.TransferMoney(model);
            return result;
        }
    }
}

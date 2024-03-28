using BankManagmentSystem.Business_Logic;
using BankManagmentSystem.DTO;
using BankManagmentSystem.DTO.AddModels;
using BankManagmentSystem.DTO.ViewModels;
using BankManagmentSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankManagmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin,Employee")]
    public class BankAccountController : ControllerBase
    {
        private readonly IBankAccountManager _bankAccountManager;

        public BankAccountController(IBankAccountManager bankAccountManager)
        {
            _bankAccountManager = bankAccountManager;
        }
        [HttpPost("CreateAccount")]
        public async Task<OperationViewModel> AddBankAccount(UserAddModel model)
        {
            var result = await _bankAccountManager.OpenBankAccount(model);
            return result;
        }
        [HttpDelete("DeleteUser/{id}")]
        public async Task<OperationViewModel> DeleteBankAccount(long id)
        {
            var result = await _bankAccountManager.DeleteUser(id);
            return result;
        }
        [HttpGet("GetAllUser")]
        public async Task<List<UserViewModel>> GetAllUsers()
        {
            return await _bankAccountManager.AllUsers();
        }
        [HttpGet("GetAllDetails")]
        public async Task<object> GetAllDetails()
        {
            return await _bankAccountManager.jointable();
        }
        [HttpGet("GetAllDetailsById/{id}")]
        public async Task<List<JoinModel>> GetAllDetailsById(long id)
        {
            var result = await _bankAccountManager.GetAllDetails(id);
            if(result != null)
                return result;
            return null;
        }
        [HttpPut("UpdateBankUser")]
        public async Task<OperationViewModel> UpdateBankUser(UpdateModel model)
        {
            var result = await _bankAccountManager.UpdateUserData(model);
            if (result.sucess)
            {
                return result;
            }
            return result;
        }
    }
}

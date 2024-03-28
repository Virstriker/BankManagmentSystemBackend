using BankManagmentSystem.Business_Logic;
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
    public class AccountController : ControllerBase
    {
        private readonly IAccountManager _accountManager;

        public AccountController(IAccountManager accountManager)
        {
            _accountManager = accountManager;
        }
        [HttpPost("AddAccount/{id}")]
        public async Task<OperationViewModel> AddAccount(long id,AccountAddModel model)
        {
            return await _accountManager.AddAccount(id, model);
        }
        [HttpDelete("DeleteAccount/{id}")]
        public async Task<OperationViewModel> DeleteAccount(long id)
        {
            return await _accountManager.DeleteAccount(id);
        }
        [HttpGet("GetAllAccount")]
        public async Task<List<AccountViewModel>> GetAllAccount()
        {
            return await _accountManager.GetAllAccount();
        }
        [HttpGet("GetAllDeActiveAccount")]
        public async Task<List<AccountViewModel>> GetAllDeActive()
        {
            return await _accountManager.GetDeActiveAccount();
        }
    }
}

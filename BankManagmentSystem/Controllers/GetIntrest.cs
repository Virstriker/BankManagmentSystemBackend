using BankManagmentSystem.Business_Logic.Interfaces;
using BankManagmentSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankManagmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
        [Authorize(Roles ="Admin,Employee")]
    public class GetIntrest : ControllerBase
    {
        private readonly IEnumerable<IGetIntrest> _getIntrests;

        public GetIntrest(IEnumerable<IGetIntrest> getIntrests)
        {
            _getIntrests = getIntrests;
        }
        [HttpGet("GetIntrest/{id}")]
        public async Task<AccountType> GetIntrestRate(int id)
        {
            foreach (var item in _getIntrests)
            {
                if (item.Id == id)
                {
                    return await item.GetIntrestrate();
                }
            }
            return null;
        }
    }
}

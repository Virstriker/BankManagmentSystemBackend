using BankManagmentSystem.Business_Logic;
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
    [Authorize(Roles ="Admin")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeManager _employeeManager;
        public EmployeeController(IEmployeeManager employeeManager)
        {
            _employeeManager = employeeManager;
        }
        [HttpPost("AddEmployee")]
        public async Task<OperationViewModel> AddBankEmployee(EmployeeAddModel model)
        {
            return await _employeeManager.AddEmployee(model); ;
        }
        [HttpDelete("DeleteEmployee/{Id}")]
        public async Task<OperationViewModel> DeleteEmployee(int Id)
        {
            return await _employeeManager.DeleteEmployee(Id); ;
        }
        [HttpGet("GetAllEmployee")]
        public async Task<List<BankEmployee>> GetAllEmployees()
        {
            return await _employeeManager.GetAllEmployee(); ;
        }[HttpGet("GetAllAdmin")]
        public async Task<List<BankEmployee>> GetAllAdmin()
        {
            return await _employeeManager.GetAllAdmin(); ;
        }
        [HttpGet("GetEmployeeById/{id}")]
        public async Task<BankEmployee> GetEmployeeById(int id)
        {
            return await _employeeManager.GetEmployee(id); ;
        }
        [HttpPut("UpdateEmployee")]
        public async Task<OperationViewModel> UpdateEmployee(BankEmployee model)
        {
            return await _employeeManager.UpdateEmployee(model); ;
        }
    }
}

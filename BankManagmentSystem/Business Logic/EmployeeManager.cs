using AutoMapper;
using BankManagmentSystem.DTO.AddModels;
using BankManagmentSystem.DTO.ViewModels;
using BankManagmentSystem.Models;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BankManagmentSystem.Business_Logic
{
    public class EmployeeManager : IEmployeeManager
    {
        private readonly IMapper _mapper;
        private readonly BankingSystemContext _data;

        public EmployeeManager(IMapper mapper, BankingSystemContext bankingSystemContext)
        {
            _mapper = mapper;
            _data = bankingSystemContext;
        }
        public async Task<OperationViewModel> ValidateEmployee(BankEmployee employee)
        {
            DateTime cur = DateTime.Now;
            var yearDifference = (cur.Year - employee.EmployeeDateOfBirth.Year);
            var condition2 = !(yearDifference >= 18);
            if ((yearDifference <= 21))
                return new OperationViewModel() { sucess = false, Message = "Invalid Age!" };
            if (!(employee.EmployeeAadharNo.ToString().Length == 12))
                return new OperationViewModel() { sucess = false, Message = "Invalid AadharNo" };
            if (!(employee.EmployeeMobileNo.ToString().Length == 10))
                return new OperationViewModel() { sucess = false, Message = "Invalid MobileNo" };
            if(employee.EmployeeId == null)
            {
            if (_data.BankEmployees.Where(s => s.EmployeeMobileNo == employee.EmployeeMobileNo).Any())
                return new OperationViewModel() { sucess = false, Message = "Employee with this number alredy exist!" };
            if (_data.BankEmployees.Where(s => s.EmployeeAadharNo == employee.EmployeeAadharNo).Any())
                return new OperationViewModel() { sucess = false, Message = "Employee with this Aadhar alredy exist!" };
            if (_data.BankEmployees.Where(s => s.EmployeLoginId == employee.EmployeLoginId).Any())
                return new OperationViewModel() { sucess = false, Message = "Employee with this UserName alredy exist!" };
            }
            return new OperationViewModel() { sucess = true, Message = "" };
        }
        public async Task<OperationViewModel> AddEmployee(EmployeeAddModel employee)
        {
            try
            {
                var Employee = _mapper.Map<BankEmployee>(employee);
                var validate = await ValidateEmployee(Employee);
                if (validate.sucess != true)
                {
                    return validate;
                }
                else
                {
                    await _data.BankEmployees.AddAsync(Employee);
                    await _data.SaveChangesAsync();
                    return new OperationViewModel() { sucess = true, Message = "Employee Added!" };
                }
            }
            catch (Exception ex)
            {
                return new OperationViewModel() { sucess = false, Message = ex.Message };
            }
        }

        public async Task<OperationViewModel> DeleteEmployee(int employeeId)
        {
            try
            {
                if (_data.BankEmployees.Where(s => s.EmployeeId == employeeId).Any())
                {
                    _data.BankEmployees.Remove(await _data.BankEmployees.FindAsync(employeeId));
                    await _data.SaveChangesAsync();
                    return new OperationViewModel() { sucess = true, Message = "Employee Deleted!" };
                }
                else
                {
                    return new OperationViewModel() { sucess = false, Message = "Employee Not Found!" };
                }
            }
            catch (Exception ex)
            {
                return new OperationViewModel() { sucess = false, Message = ex.Message };
            }
        }
        public async Task<List<BankEmployee>> GetAllEmployee()
        {
            try
            {
                var result = await _data.BankEmployees.Where(s=>s.EmployeeIsAdmin == false).ToListAsync();
                return result;
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }public async Task<List<BankEmployee>> GetAllAdmin()
        {
            try
            {
                var result = await _data.BankEmployees.Where(s=>s.EmployeeIsAdmin == true).ToListAsync();
                return result;
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<BankEmployee> GetEmployee(int employeeId)
        {
            try
            {
                var result = await _data.BankEmployees.FirstOrDefaultAsync(s => s.EmployeeId == employeeId && s.EmployeeIsAdmin == false);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<OperationViewModel> UpdateEmployee(BankEmployee employee)
        {
            try
            {
                var validate = await ValidateEmployee(employee);
                if (validate.sucess != true)
                {
                    return validate;
                }
                else
                {
                    _data.Entry(employee).State = EntityState.Modified;
                    await _data.SaveChangesAsync();
                    return new OperationViewModel { sucess = true, Message = "Employee Updated" };
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

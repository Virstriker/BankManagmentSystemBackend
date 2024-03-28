using AutoMapper;
using BankManagmentSystem.DTO.AddModels;
using BankManagmentSystem.DTO.ViewModels;
using BankManagmentSystem.Models;

namespace BankManagmentSystem.Business_Logic
{
    public class LoginManager : ILoginManager
    {
        private readonly IMapper _mapper;
        private readonly BankingSystemContext _data;

        public LoginManager(IMapper mapper, BankingSystemContext bankingSystemContext)
        {
            _mapper = mapper;
            _data = bankingSystemContext;
        }
        public async Task<EmployeeLoginViewModel> BankLogin(LoginBankModel model)
        {
            var Profile = _data.BankEmployees.FirstOrDefault(s => s.EmployeLoginId == model.EmployeLoginId && s.EmployeeLoginPassword == model.EmployeeLoginPassword);
            if (Profile != null)
            {
                var mappedProfiel = _mapper.Map<EmployeeVIewModel>(Profile);
                if (Profile.EmployeeIsAdmin == true)
                {
                    return new EmployeeLoginViewModel() { sucess = true, Admin = true, EmployeeVIewModel = mappedProfiel };
                }
                return new EmployeeLoginViewModel() { sucess = true, Admin = false, EmployeeVIewModel = mappedProfiel };
            }
            return new EmployeeLoginViewModel() { sucess = false, Admin = false, EmployeeVIewModel = null };
        }
    }
}

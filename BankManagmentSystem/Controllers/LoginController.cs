using BankManagmentSystem.Business_Logic;
using BankManagmentSystem.DTO.AddModels;
using BankManagmentSystem.DTO.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BankManagmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginManager _loginManager;
        private readonly IConfiguration _configuration;
        public LoginController(ILoginManager loginManager, IConfiguration configuration)
        {
            _loginManager = loginManager;
            _configuration = configuration;
        }
        private string GenerateToken(EmployeeLoginViewModel model)
        {
            var claims = new List<Claim>();
            if(model.Admin == true)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            }
            else
            {
                claims.Add(new Claim(ClaimTypes.Role, "Employee"));
            }
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securitykey,SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], 
                claims: claims,
                expires: DateTime.Now.AddHours(24),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        [HttpPost("BankLogin")]
        public async Task<EmployeeLoginViewModel> BankLoginCheck(LoginBankModel loginBankModel)
        {
            var login =  await _loginManager.BankLogin(loginBankModel);
            if(login.sucess == true)
            {
                login.Token = GenerateToken(login);
                return login;
            }
            return login;
        }
        [HttpPost("GenerateToken")]
        public async Task<OperationViewModel> GenerateTokenEverySce(LoginBankModel login)
        {
            var login1 =  await _loginManager.BankLogin(login);
            if(login1.sucess == true)
            {
                
                return new OperationViewModel() { sucess=true ,Message = GenerateToken(login1) };
            }
            return new OperationViewModel() { sucess = false, Message ="Failed" };
        }
    }
}

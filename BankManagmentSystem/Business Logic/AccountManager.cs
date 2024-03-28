using AutoMapper;
using BankManagmentSystem.DTO.AddModels;
using BankManagmentSystem.DTO.ViewModels;
using BankManagmentSystem.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace BankManagmentSystem.Business_Logic
{
    public class AccountManager : IAccountManager
    {
        private readonly IMapper _mapper;
        private readonly BankingSystemContext _data;

        public AccountManager(IMapper mapper, BankingSystemContext bankingSystemContext)
        {
            _mapper = mapper;
            _data = bankingSystemContext;
        }
        public async Task<OperationViewModel> AddAccount(long id, AccountAddModel accountAdd) 
        {
            try
            { 
                if(_data.BankUsers.Where(s=>s.UserId == id && s.Active == true).Any()) {
                    if(!_data.UserAccounts.Where(x=>x.AccountType == accountAdd.AccountType && x.AccountUser == id && x.Active == true).Any())
                    {
                        var NewAcc = _mapper.Map<UserAccount>(accountAdd);
                        NewAcc.AccountUser = (int)id;
                        _data.UserAccounts.Add(NewAcc);
                        await _data.SaveChangesAsync();
                        return new OperationViewModel() { sucess = true, Message = "Account Added" };
                    }
                        return new OperationViewModel() { sucess = false, Message = "This type of account alredy exist" };
                }
                        return new OperationViewModel() { sucess = false, Message = "User Not Found" };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<OperationViewModel> DeleteAccount(long id)
        {
            try
            {
                var Account = _data.UserAccounts.FirstOrDefault(s=>s.AccountNumber == id && s.Active == true);
                if (Account != null)
                {
                    if(_data.UserAccounts.Where(x=>x.AccountUser == Account.AccountUser).Any())
                    {
                        Account.Active = false;
                        _data.Entry(Account).State = EntityState.Modified;
                        await _data.SaveChangesAsync();
                        return new OperationViewModel() { sucess = true, Message = "Account Deleted" };
                    }
                        return new OperationViewModel() { sucess = false, Message = "Can not delete all accounts" };
                }
                        return new OperationViewModel() { sucess = false, Message = "Account Not Found" };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<AccountViewModel>> GetAllAccount()
        {
            return _mapper.Map<List<AccountViewModel>>(_data.UserAccounts.Where(s=>s.Balance>0));
        }public async Task<List<AccountViewModel>> GetDeActiveAccount()
        {
            return _mapper.Map<List<AccountViewModel>>(_data.UserAccounts.Where(s=>s.Active==false));
        }
    }
}

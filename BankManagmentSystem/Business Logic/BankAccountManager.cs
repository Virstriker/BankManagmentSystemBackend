using AutoMapper;
using BankManagmentSystem.DTO;
using BankManagmentSystem.DTO.AddModels;
using BankManagmentSystem.DTO.ViewModels;
using BankManagmentSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BankManagmentSystem.Business_Logic
{
    public class BankAccountManager : IBankAccountManager
    {
        private readonly BankingSystemContext _data;
        private readonly IMapper _mapper;
        public BankAccountManager(IMapper mapper, BankingSystemContext data)
        {
            _data = data;
            _mapper = mapper;
        }
        public async Task<OperationViewModel> OpenBankAccount(UserAddModel user)
        {
            try{
                int customerId = 0;
                foreach (var s in _data.BankUsers)
                {
                    if (s.AadharNo == user.AadharNo)
                        if (s.Active == false)
                            customerId = s.UserId;
                }
                DateTime cur = DateTime.Now;
                var yearDifference = (cur.Year - user.DateOfBirth.Year);
                var condition1 = !(yearDifference >= 18);
                var condition2 = !(user.AadharNo.ToString().Length == 12);
                var condition3 = (_data.BankUsers.Where(s => s.AadharNo == user.AadharNo && s.Active == true).Any());
                var condition4 = !(user.MobileNo.ToString().Length == 10);
                var condition5 = !(user.AccountType == "savings" || user.AccountType == "current");
                if (condition1) return new OperationViewModel() { sucess = false, Message = "Invalid Age" };
                if (condition2) return new OperationViewModel() { sucess = false, Message = "Invalid AadharNo" };
                if (condition3) return new OperationViewModel() { sucess = false, Message = "Invalid MobileNo" };
                if (condition4) return new OperationViewModel() { sucess = false, Message = "Cannot create two account from same AadharNo" };
                if (condition5) return new OperationViewModel() { sucess = false, Message = "Invalid Account Type" };
                if (user.AccountType == "current")
                    if (user.Balance <= 1000)
                        return new OperationViewModel() { sucess = false, Message = "Balance Should At least be 1000" };
                if (customerId != 0)
                    goto Existing;

                var result = _mapper.Map<BankUser>(user);
                var result1 = _mapper.Map<UserAccount>(user);

                _data.Add(result);
                await _data.SaveChangesAsync();

                var dbId = await _data.BankUsers.FirstOrDefaultAsync(s => s.AadharNo == user.AadharNo);
                result1.AccountUser = dbId.UserId;
                _data.UserAccounts.Add(result1);
                await _data.SaveChangesAsync();

                return new OperationViewModel() { sucess = true, Message = "Account Added" };

            Existing:
                var update = _mapper.Map<BankUser>(user);
                update.Active = true;
                update.UserId = customerId;
                _data.Entry(update).State = EntityState.Modified;
                await _data.SaveChangesAsync();

                var result2 = _mapper.Map<UserAccount>(user);
                result2.AccountUser = customerId;
                _data.UserAccounts.Add(result2);
                await _data.SaveChangesAsync();

                return new OperationViewModel() { sucess = true, Message = "Account Added" };
            }
            catch (Exception e)
            {
                return new OperationViewModel() { sucess = false, Message = e.Message };
            }
        }
        public async Task<OperationViewModel> DeleteUser(long id)
        {
            if(_data.BankUsers.Where(s=>s.AadharNo == id && s.Active == true).Any())
            {
                var updateUser = await _data.BankUsers.FirstOrDefaultAsync(s=>s.AadharNo == id);
                var updateAccount = await _data.UserAccounts.Where(s=>s.AccountUser == updateUser.UserId && s.Active == true).ToListAsync();
                updateUser.Active = false;
                _data.Entry(updateUser).State = EntityState.Modified;
                await _data.SaveChangesAsync();
                foreach(var user in updateAccount)
                {
                     user.Active = false;
                     _data.Entry(user).State = EntityState.Modified;
                }
                     await _data.SaveChangesAsync();
                return new OperationViewModel() { sucess = true, Message = "Deleted Sucessfully" };
            }
            return new OperationViewModel() { sucess = false, Message = "User Not Found"};
        }
        public async Task<UserViewModel> UserMapper(BankUser user)
        {
            return _mapper.Map<UserViewModel>(user);
        }
        public async Task<List<UserViewModel>> AllUsers()
        {
            List<UserViewModel> users = new List<UserViewModel>();
            foreach(var user in _data.BankUsers.Where(s=>s.Active==true))
            {
                users.Add(await UserMapper(user));
            }
            return users;
        }
        public async Task<List<JoinModel>> GetAllDetails(long id)
        {
            var data = await _data.JoinModels.FromSqlRaw("SP_Join").ToListAsync();
            var result = data.Where(s=>s.AadharNo == id).ToList();
            return result;
        }
        public async Task<object> jointable()
        {
            var a = await _data.JoinModels.FromSqlRaw("SP_Join").ToListAsync();
            return a;
        }
        public async Task<OperationViewModel> UpdateUserData(UpdateModel model)
        {
            var update = _mapper.Map<BankUser>(model);
            var condition1 = !(update.AadharNo.ToString().Length == 12);
            var condition2 = !(update.MobileNo.ToString().Length == 10);
            if (condition1) return new OperationViewModel() { sucess = false, Message = "Invalid AadharNo" };
            if (condition2) return new OperationViewModel() { sucess = false, Message = "Invalid MobileNo" };
            _data.Entry(update).State = EntityState.Modified;
            await _data.SaveChangesAsync();
            return new OperationViewModel() { sucess = true, Message = "Data Updated" };
        }
    }
}

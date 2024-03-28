using AutoMapper;
using BankManagmentSystem.DTO;
using BankManagmentSystem.DTO.AddModels;
using BankManagmentSystem.DTO.ViewModels;
using BankManagmentSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace BankManagmentSystem.Business_Logic
{
    public class TransactionManager : ITransactionManager
    {
        private readonly IMapper _mapper;
        private readonly BankingSystemContext _data;

        public TransactionManager(IMapper mapper, BankingSystemContext bankingSystemContext)
        {
            _mapper = mapper;
            _data = bankingSystemContext;
        }
        public async Task<OperationViewModel> DepositTransction(DepositTransctionAddModel money)
        {
            var account = await _data.UserAccounts.FirstOrDefaultAsync(s => s.AccountNumber == money.TransactionAccount && s.Active == true);
            if (account != null)
            {
                var transaction = _mapper.Map<MoneyTransaction>(money);
                transaction.TransactionType = "Deposit";
                account.Balance += money.TransactionAmount;
                _data.Entry(account).State = EntityState.Modified;
                await _data.SaveChangesAsync();

                _data.MoneyTransactions.Add(transaction);
                await _data.SaveChangesAsync();

                return new OperationViewModel() { sucess = true, Message = "Transcation sucessful" };

            }
            return new OperationViewModel() { sucess = false, Message = "Account Not Found" };
        }

        public async Task<OperationViewModel> WithdrawalTransction(WithdrawalTransctionAddModel money)
        {
            var account = await _data.UserAccounts.FirstOrDefaultAsync(s => s.AccountNumber == money.TransactionAccount && s.Active == true);
            int panelty = 0;
            if (account != null)
            {
                if((account.Balance - money.TransactionAmount) < 0)
                {
                    return new OperationViewModel() { sucess = false, Message = "Not Enough Balance" };
                }
                if (account.AccountType == "current")
                {
                    if ((account.Balance - money.TransactionAmount) < 1000)
                    {
                        panelty = 1;
                    }
                }
                var transaction = _mapper.Map<MoneyTransaction>(money);
                transaction.TransactionType = "Withdrawal";
                account.Balance -= money.TransactionAmount;
                _data.Entry(account).State = EntityState.Modified;
                await _data.SaveChangesAsync();

                _data.MoneyTransactions.Add(transaction);
                await _data.SaveChangesAsync();

                if (panelty == 1)
                {
                    return new OperationViewModel() { sucess = true, Message = "Withdrawal Success || Balance is less then 1000 Penalty will be applied " };
                }
                return new OperationViewModel() { sucess = true, Message = "Transcation sucessful" };

            }
            return new OperationViewModel() { sucess = false, Message = "Account Not Found" };
        }

        public async Task<OperationViewModel> TransferMoney(TransferTransctionAddModel money)
        {
            var account1 = await _data.UserAccounts.FirstOrDefaultAsync(s => s.AccountNumber == money.TransactionAccount && s.Active == true);
            var account2 = await _data.UserAccounts.FirstOrDefaultAsync(s => s.AccountNumber == money.TransactionToAccount && s.Active == true);
            int penalty = 0;
            if (account1 != null && account2 != null)
            {
                if ((account1.Balance - money.TransactionAmount) < 0)
                {
                    return new OperationViewModel() { sucess = false, Message = "Not Enough Balance" };
                }
                if (account1.AccountType == "current")
                {
                    if ((account1.Balance - money.TransactionAmount) < 1000)
                    {
                        penalty = 1;
                    }
                }
                var transaction = _mapper.Map<MoneyTransaction>(money);
                transaction.TransactionType = "Transfer";
                account1.Balance -= money.TransactionAmount;
                account2.Balance += money.TransactionAmount;
                _data.Entry(account1).State = EntityState.Modified;
                await _data.SaveChangesAsync();

                _data.Entry(account2).State = EntityState.Modified;
                await _data.SaveChangesAsync();

                _data.MoneyTransactions.Add(transaction);
                await _data.SaveChangesAsync();

                if (penalty == 0)
                {
                    return new OperationViewModel() { sucess = true, Message = "Transfer Success" };
                }
                return new OperationViewModel() { sucess = true, Message = "Transfer Success || Balance is less then 1000 Penalty will be applied " };
            }
            return new OperationViewModel() { sucess = false, Message = "Account Not Found" };
        }
        public async Task<List<TransactionViewModel>> GetTransaction(long account)
        {
            List<TransactionViewModel> transactions = new List<TransactionViewModel>();
            foreach(var item in _data.MoneyTransactions)
            {
                if(item.TransactionAccount == account || item.TransactionToAccount == account)
                {
                    var map = _mapper.Map<TransactionViewModel>(item);
                    transactions.Add(map);
                }
            }
            if(transactions.Count > 0)
            {
                return transactions;
            }
            else {
                return null;
            }
        }
    }
}

using BankManagmentSystem.Models;

namespace BankManagmentSystem.Business_Logic.Interfaces
{
    public interface IGetIntrest
    {
        int Id { get; }

        Task<AccountType> GetIntrestrate();
    }
}

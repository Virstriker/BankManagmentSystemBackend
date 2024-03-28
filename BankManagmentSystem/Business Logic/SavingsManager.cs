using AutoMapper;
using BankManagmentSystem.Business_Logic.Interfaces;
using BankManagmentSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BankManagmentSystem.Business_Logic
{
    public class SavingsManager : IGetIntrest
    {
        public int Id { get { return 2; } }
        private readonly BankingSystemContext _data;
        private readonly IMapper _mapper;
        public SavingsManager(IMapper mapper, BankingSystemContext data)
        {
            _data = data;
            _mapper = mapper;
        }

        public async Task<AccountType> GetIntrestrate()
        {
            return await _data.AccountTypes.FirstOrDefaultAsync(s=>s.AccountType1 == "savings");
        }
    }
}


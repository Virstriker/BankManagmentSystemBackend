using AutoMapper;
using BankManagmentSystem.Business_Logic.Interfaces;
using BankManagmentSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BankManagmentSystem.Business_Logic
{
    public class CurrentManager : IGetIntrest
    {
        public int Id { get { return 1; } }
        private readonly BankingSystemContext _data;
        private readonly IMapper _mapper;
        public CurrentManager(IMapper mapper, BankingSystemContext data)
        {
            _data = data;
            _mapper = mapper;
        }
        public async Task<AccountType> GetIntrestrate()
        {
            return await _data.AccountTypes.FirstOrDefaultAsync(s => s.AccountType1 == "current");
        }
    }
}

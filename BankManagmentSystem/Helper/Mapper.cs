using AutoMapper;
using BankManagmentSystem.DTO;
using BankManagmentSystem.DTO.AddModels;
using BankManagmentSystem.DTO.ViewModels;
using BankManagmentSystem.Models;

namespace BankManagmentSystem.Helper
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            //CreateMap<BankUser,UserAddModel>();
            CreateMap<BankUser,UserAddModel>().ReverseMap();
            //CreateMap<BankUser,UserViewModel>();
            CreateMap<BankUser,UserViewModel>().ReverseMap();
            //CreateMap<UserAccount, UserAddModel>();
            CreateMap<UserAccount, UserAddModel>().ReverseMap();
            //CreateMap<DepositTransctionAddModel, MoneyTransaction>();
            CreateMap<DepositTransctionAddModel, MoneyTransaction>().ReverseMap();
            //CreateMap<WithdrawalTransctionAddModel, MoneyTransaction>();
            CreateMap<WithdrawalTransctionAddModel, MoneyTransaction>().ReverseMap();
            //CreateMap<TransferTransctionAddModel, MoneyTransaction>();
            CreateMap<TransferTransctionAddModel, MoneyTransaction>().ReverseMap();
            //CreateMap<TransactionViewModel, MoneyTransaction>();
            CreateMap<TransactionViewModel, MoneyTransaction>().ReverseMap();
            CreateMap<BankUser, UpdateModel>().ReverseMap();
            CreateMap<EmployeeAddModel, BankEmployee>().ReverseMap();
            CreateMap<AccountAddModel,UserAccount>().ReverseMap();
            CreateMap<AccountViewModel,UserAccount>().ReverseMap();
            CreateMap<EmployeeVIewModel, BankEmployee>().ReverseMap();

        }
    }
}

namespace BankManagmentSystem.DTO.ViewModels
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public long MobileNo { get; set; }
        public string EmailId { get; set; } = null!;
        public long AadharNo { get; set; }
        public bool? Active { get; set; }
    }
}

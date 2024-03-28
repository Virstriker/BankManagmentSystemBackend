namespace BankManagmentSystem.DTO.AddModels
{
    public class UpdateModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public long MobileNo { get; set; }
        public string EmailId { get; set; } = null!;
        public long AadharNo { get; set; }
        public bool? Active { get; set; }
    }
}

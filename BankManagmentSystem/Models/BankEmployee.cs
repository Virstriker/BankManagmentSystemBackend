using System;
using System.Collections.Generic;

namespace BankManagmentSystem.Models
{
    public partial class BankEmployee
    {
        public int EmployeeId { get; set; }
        public string EmployeeFirstName { get; set; } = null!;
        public string EmployeeLastName { get; set; } = null!;
        public DateTime EmployeeDateOfBirth { get; set; }
        public long EmployeeMobileNo { get; set; }
        public string EmployeeEmailId { get; set; } = null!;
        public long EmployeeAadharNo { get; set; }
        public string EmployeLoginId { get; set; } = null!;
        public string EmployeeLoginPassword { get; set; } = null!;
        public bool? EmployeeIsAdmin { get; set; }
    }
}

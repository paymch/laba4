using System;

namespace Lab1.Shared
{
    public class BankAccountDto
    {
        public int Id { get; set; }
        public string Number { get; set; } = string.Empty;
        public string AccountType { get; set; } = string.Empty;
        public string OwnerName { get; set; } = string.Empty;
        public int OwnerId { get; set; }
        public DateTime DateCreate { get; set; }
        public decimal Balance { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}

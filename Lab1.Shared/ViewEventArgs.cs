using System;

namespace Lab1.Shared
{
    public class AddAccountEventArgs : EventArgs
    {
        public string Number { get; }
        public int OwnerId { get; }
        public int TypeId { get; }
        public decimal InitialBalance { get; }

        public AddAccountEventArgs(string number, int ownerId, int typeId, decimal initialBalance)
        {
            Number = number;
            OwnerId = ownerId;
            TypeId = typeId;
            InitialBalance = initialBalance;
        }
    }

    public class ChangeOwnerEventArgs : EventArgs
    {
        public int AccountId { get; }
        public int NewOwnerId { get; }

        public ChangeOwnerEventArgs(int accountId, int newOwnerId)
        {
            AccountId = accountId;
            NewOwnerId = newOwnerId;
        }
    }

    public class AccountIdEventArgs : EventArgs
    {
        public int AccountId { get; }

        public AccountIdEventArgs(int accountId)
        {
            AccountId = accountId;
        }
    }

    public class TransferEventArgs : EventArgs
    {
        public int SenderAccountId { get; }
        public string TargetNumber { get; }
        public decimal Amount { get; }

        public TransferEventArgs(int senderAccountId, string targetNumber, decimal amount)
        {
            SenderAccountId = senderAccountId;
            TargetNumber = targetNumber;
            Amount = amount;
        }
    }
}

using System;
using System.Collections.Generic;
using Lab1.Model.ModelsDB;

namespace Lab1.BusinessLogic
{
    public class MainModel : IMainModel
    {
        private readonly Logic _logic;

        public event EventHandler<IEnumerable<BankAccount>>? AccountsLoaded;
        public event EventHandler<OperationResult>? OperationCompleted;

        public MainModel(Logic logic)
        {
            _logic = logic;
        }

        public void LoadAccounts(bool showDeleted)
        {
            var accounts = _logic.GetAccounts(showDeleted);
            AccountsLoaded?.Invoke(this, accounts);
        }

        public void AddAccount(string? number, int ownerId, int typeId, decimal initialBalance)
        {
            var result = _logic.AddAccount(number, ownerId, typeId, initialBalance);
            OperationCompleted?.Invoke(this, result);
        }

        public void ChangeOwner(int accountId, int newOwnerId)
        {
            var result = _logic.ChangeOwner(accountId, newOwnerId);
            OperationCompleted?.Invoke(this, result);
        }

        public void DeleteAccount(int accountId)
        {
            var result = _logic.DeleteAccount(accountId);
            OperationCompleted?.Invoke(this, result);
        }

        public void RestoreAccount(int accountId)
        {
            var result = _logic.RestoreAccount(accountId);
            OperationCompleted?.Invoke(this, result);
        }

        public void ToggleFreeze(int accountId)
        {
            var result = _logic.ToggleFreeze(accountId);
            OperationCompleted?.Invoke(this, result);
        }

        public void Transfer(int senderId, string? targetNumber, decimal amount)
        {
            var result = _logic.Transfer(senderId, targetNumber, amount);
            OperationCompleted?.Invoke(this, result);
        }
    }
}

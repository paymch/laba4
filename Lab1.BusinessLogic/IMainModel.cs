using System;
using System.Collections.Generic;
using Lab1.Model.ModelsDB;

namespace Lab1.BusinessLogic
{
    public interface IMainModel
    {
        event EventHandler<IEnumerable<BankAccount>> AccountsLoaded;
        event EventHandler<OperationResult> OperationCompleted;

        void LoadAccounts(bool showDeleted);
        void AddAccount(string? number, int ownerId, int typeId, decimal initialBalance);
        void ChangeOwner(int accountId, int newOwnerId);
        void DeleteAccount(int accountId);
        void RestoreAccount(int accountId);
        void ToggleFreeze(int accountId);
        void Transfer(int senderId, string? targetNumber, decimal amount);
    }
}

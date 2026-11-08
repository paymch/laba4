using System;
using System.Collections.Generic;

namespace Lab1.Shared
{
    public interface IMainView
    {
        // Events triggered by user interaction
        event EventHandler<bool> ShowAccountsRequested; // true if deleted should be shown
        event EventHandler<AddAccountEventArgs> AddAccountRequested;
        event EventHandler<ChangeOwnerEventArgs> ChangeOwnerRequested;
        event EventHandler<AccountIdEventArgs> DeleteAccountRequested;
        event EventHandler<AccountIdEventArgs> RestoreAccountRequested;
        event EventHandler<AccountIdEventArgs> ToggleFreezeRequested;
        event EventHandler<TransferEventArgs> TransferRequested;

        // Methods invoked by Presenter to update the View
        void DisplayAccounts(IEnumerable<BankAccountDto> accounts);
        void ShowMessage(string message, bool isError = false);
        void ClearInputs();
    }
}

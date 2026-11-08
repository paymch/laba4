using System;
using System.Collections.Generic;
using System.Linq;
using Lab1.BusinessLogic;
using Lab1.Model.ModelsDB;
using Lab1.Shared;

namespace Lab1.Presenter
{
    public class MainPresenter
    {
        private readonly IMainView _view;
        private readonly IMainModel _model;

        private bool _showDeleted = false;

        public MainPresenter(IMainView view, IMainModel model)
        {
            _view = view;
            _model = model;

            _view.ShowAccountsRequested += View_ShowAccountsRequested;
            _view.AddAccountRequested += View_AddAccountRequested;
            _view.ChangeOwnerRequested += View_ChangeOwnerRequested;
            _view.DeleteAccountRequested += View_DeleteAccountRequested;
            _view.RestoreAccountRequested += View_RestoreAccountRequested;
            _view.ToggleFreezeRequested += View_ToggleFreezeRequested;
            _view.TransferRequested += View_TransferRequested;

            _model.AccountsLoaded += Model_AccountsLoaded;
            _model.OperationCompleted += Model_OperationCompleted;
        }

        public void LoadAccounts()
        {
            _model.LoadAccounts(_showDeleted);
        }

        private void Model_AccountsLoaded(object? sender, IEnumerable<BankAccount> accounts)
        {
            var dtos = accounts.Select(MapToDto).ToList();
            _view.DisplayAccounts(dtos);
        }

        private void Model_OperationCompleted(object? sender, OperationResult result)
        {
            if (!result.Success && !string.IsNullOrEmpty(result.Message))
            {
                _view.ShowMessage(result.Message, true);
            }
            else
            {
                _view.ClearInputs();
                LoadAccounts();
            }
        }

        private void View_ShowAccountsRequested(object? sender, bool showDeleted)
        {
            _showDeleted = showDeleted;
            LoadAccounts();
        }

        private void View_AddAccountRequested(object? sender, AddAccountEventArgs e)
        {
            _model.AddAccount(e.Number, e.OwnerId, e.TypeId, e.InitialBalance);
        }

        private void View_ChangeOwnerRequested(object? sender, ChangeOwnerEventArgs e)
        {
            _model.ChangeOwner(e.AccountId, e.NewOwnerId);
        }

        private void View_DeleteAccountRequested(object? sender, AccountIdEventArgs e)
        {
            _model.DeleteAccount(e.AccountId);
        }

        private void View_RestoreAccountRequested(object? sender, AccountIdEventArgs e)
        {
            _model.RestoreAccount(e.AccountId);
        }

        private void View_ToggleFreezeRequested(object? sender, AccountIdEventArgs e)
        {
            _model.ToggleFreeze(e.AccountId);
        }

        private void View_TransferRequested(object? sender, TransferEventArgs e)
        {
            _model.Transfer(e.SenderAccountId, e.TargetNumber, e.Amount);
        }

        private BankAccountDto MapToDto(BankAccount account)
        {
            return new BankAccountDto
            {
                Id = account.Id,
                Number = account.number,
                AccountType = account.Type_BankAccount?.name ?? string.Empty,
                OwnerName = account.Account?.full_name ?? string.Empty,
                OwnerId = account.id_owner,
                DateCreate = account.date_create,
                Balance = account.balance,
                IsActive = account.isActiv,
                IsDeleted = account.isDeleted
            };
        }
    }
}

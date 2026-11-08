using System;
using System.Collections.Generic;
using Lab1.Shared;

namespace Lab1.ConsoleApp
{
    public class ConsoleView : IMainView
    {
        public event EventHandler<bool>? ShowAccountsRequested;
        public event EventHandler<AddAccountEventArgs>? AddAccountRequested;
        public event EventHandler<ChangeOwnerEventArgs>? ChangeOwnerRequested;
        public event EventHandler<AccountIdEventArgs>? DeleteAccountRequested;
        public event EventHandler<AccountIdEventArgs>? RestoreAccountRequested;
        public event EventHandler<AccountIdEventArgs>? ToggleFreezeRequested;
        public event EventHandler<TransferEventArgs>? TransferRequested;

        private List<BankAccountDto> _currentAccounts = new List<BankAccountDto>();

        public void DisplayAccounts(IEnumerable<BankAccountDto> accounts)
        {
            _currentAccounts.Clear();
            _currentAccounts.AddRange(accounts);
        }

        public void ShowMessage(string message, bool isError = false)
        {
            Console.WriteLine($"\n[{(isError ? "ОШИБКА" : "ИНФО")}] {message}");
            Console.WriteLine("Нажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }

        public void ClearInputs()
        {
            // Not strictly needed in console
        }

        public void Run()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("--- УПРАВЛЕНИЕ СЧЕТАМИ (MVP) ---");
                Console.WriteLine("1. Показать список счетов (без удаленных)");
                Console.WriteLine("2. Добавить новый счет");
                Console.WriteLine("3. Изменить владельца счета");
                Console.WriteLine("4. Заморозить / Разморозить счет");
                Console.WriteLine("5. Удалить счет");
                Console.WriteLine("6. Показать удаленные счета и восстановить");
                Console.WriteLine("7. Перевод средств");
                Console.WriteLine("0. Выход");
                Console.Write("\nВыберите действие: ");

                var input = Console.ReadLine();
                if (input == "0") break;

                switch (input)
                {
                    case "1":
                        ShowAccountsRequested?.Invoke(this, false);
                        PrintAccounts(_currentAccounts);
                        Console.WriteLine("Нажмите любую клавишу для продолжения...");
                        Console.ReadKey();
                        break;
                    case "2":
                        Console.Write("Номер счета: ");
                        var number = Console.ReadLine() ?? string.Empty;
                        Console.Write("ID владельца: ");
                        int.TryParse(Console.ReadLine(), out int ownerId);
                        Console.Write("Начальный баланс: ");
                        decimal.TryParse(Console.ReadLine(), out decimal bal);
                        AddAccountRequested?.Invoke(this, new AddAccountEventArgs(number, ownerId, 1, bal));
                        break;
                    case "3":
                        var accId = SelectAccountDialog(false);
                        if (accId.HasValue)
                        {
                            Console.Write("Новый ID владельца: ");
                            if (int.TryParse(Console.ReadLine(), out int newOwnerId))
                                ChangeOwnerRequested?.Invoke(this, new ChangeOwnerEventArgs(accId.Value, newOwnerId));
                        }
                        break;
                    case "4":
                        var freezeId = SelectAccountDialog(false);
                        if (freezeId.HasValue)
                            ToggleFreezeRequested?.Invoke(this, new AccountIdEventArgs(freezeId.Value));
                        break;
                    case "5":
                        var delId = SelectAccountDialog(false);
                        if (delId.HasValue)
                            DeleteAccountRequested?.Invoke(this, new AccountIdEventArgs(delId.Value));
                        break;
                    case "6":
                        var resId = SelectAccountDialog(true);
                        if (resId.HasValue)
                            RestoreAccountRequested?.Invoke(this, new AccountIdEventArgs(resId.Value));
                        break;
                    case "7":
                        var senderId = SelectAccountDialog(false);
                        if (senderId.HasValue)
                        {
                            Console.Write("Номер счета получателя: ");
                            string target = Console.ReadLine() ?? string.Empty;
                            Console.Write("Сумма перевода: ");
                            decimal.TryParse(Console.ReadLine(), out decimal amount);
                            TransferRequested?.Invoke(this, new TransferEventArgs(senderId.Value, target, amount));
                        }
                        break;
                    default:
                        ShowMessage("Неверный ввод", true);
                        break;
                }
            }
        }

        private int? SelectAccountDialog(bool showDeleted)
        {
            ShowAccountsRequested?.Invoke(this, showDeleted);
            if (_currentAccounts.Count == 0)
            {
                ShowMessage("Список счетов пуст.");
                return null;
            }

            PrintAccounts(_currentAccounts);
            Console.Write("\nВведите порядковый номер счета: ");
            if (int.TryParse(Console.ReadLine(), out int idx) && idx >= 1 && idx <= _currentAccounts.Count)
            {
                return _currentAccounts[idx - 1].Id;
            }

            ShowMessage("Неверный номер.", true);
            return null;
        }

        private void PrintAccounts(List<BankAccountDto> accounts)
        {
            for (int i = 0; i < accounts.Count; i++)
            {
                var acc = accounts[i];
                var owner = !string.IsNullOrEmpty(acc.OwnerName) ? acc.OwnerName : $"ID: {acc.OwnerId}";
                var status = acc.IsActive ? "Активен" : "Заморожен";
                if (acc.IsDeleted) status = "Удален";
                Console.WriteLine($"{i + 1}. Номер: {acc.Number} | Баланс: {acc.Balance:C2} | Владелец: {owner} | Статус: {status}");
            }
        }
    }
}

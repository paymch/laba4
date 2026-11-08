
﻿using Lab1.DataAccessLayer;
using Lab1.Model.ModelsDB;

namespace Lab1.BusinessLogic
{
    /// <summary>
    /// Фасад бизнес-логики для слоя представления.
    /// Делегирует операции управления счетами и переводы специализированным сервисам.
    /// </summary>
    public class Logic 
    {
        private readonly IAccountService accountService;
        private readonly ITransferService transferService;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Logic"/>.
        /// 
        /// </summary>
        /// <param name="accountService">
        /// Сервис для управления банковскими счетами.
        /// </param>
        /// <param name="transferService">
        /// Сервис для выполнения переводов между банковскими счетами.
        /// </param>
        public Logic(AccountService accountService, TransferService transferService)
        {
            this.accountService = accountService;
            this.transferService = transferService;
        }


        /// <summary>
        /// Получает список банковских счетов.
        /// </summary>
        /// <param name="deleted">Флаг, указывающий, нужно ли включать удалённые счета.</param>
        /// <returns>Список банковских счетов.</returns>
        public List<BankAccount> GetAccounts(bool deleted) =>
            accountService.GetAccounts(deleted);

        /// <summary>
        /// Получает список активных банковских счетов.
        /// </summary>
        /// <returns>Список активных банковских счетов.</returns>
        public List<BankAccount> GetActiveAccounts() =>
            accountService.GetActiveAccounts();


        /// <summary>
        /// Получает банковский счёт по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор счёта.</param>
        /// <returns>Банковский счёт или null, если не найден.</returns>
        public BankAccount? GetById(int id) =>
            accountService.GetById(id);

        /// <summary>
        /// Получает банковский счёт по его номеру.
        /// </summary>
        /// <param name="number">Номер счёта.</param>
        /// <param name="includeDeleted">Флаг, указывающий, нужно ли включать удалённые счета.</param>
        /// <returns>Банковский счёт или null, если не найден.</returns>
        public BankAccount? FindByNumber(string? number, bool includeDeleted = false) =>
            accountService.FindByNumber(number, includeDeleted);


        /// <summary>
        /// Создаёт новый банковский счёт.
        /// </summary>
        /// <param name="number">Номер счёта.</param>
        /// <param name="ownerId">Идентификатор владельца счёта.</param>
        /// <param name="typeId">Идентификатор типа счёта.</param>
        /// <param name="initialBalance">Начальный баланс счёта.</param>
        /// <returns>Результат операции.</returns>
        public OperationResult AddAccount(
            string? number, int ownerId, int typeId, decimal initialBalance) =>
            accountService.AddAccount(number, ownerId, typeId, initialBalance);

        /// <summary>
        /// Изменяет владельца банковского счёта.
        /// 
        /// </summary>
        /// <param name="accountId">Идентификатор счёта.</param>
        /// <param name="newOwnerId">Идентификатор нового владельца.</param>
        /// <returns>Результат операции.</returns>
        public OperationResult ChangeOwner(int accountId, int newOwnerId) =>
            accountService.ChangeOwner(accountId, newOwnerId);

        /// <summary>
        /// Удаляет банковский счёт.
        /// </summary>
        /// <param name="accountId">Идентификатор счёта.</param>
        /// <returns>Результат операции.</returns>
        public OperationResult DeleteAccount(int accountId) =>
            accountService.DeleteAccount(accountId);

        /// <summary>
        /// Восстанавливает удалённый банковский счёт.
        /// </summary>
        /// <param name="accountId">Идентификатор счёта.</param>
        /// <returns>Результат операции.</returns>
        public OperationResult RestoreAccount(int accountId) =>
            accountService.RestoreAccount(accountId);

        /// <summary>
        /// Переключает заморозку банковского счёта.
        /// </summary>
        /// <param name="accountId">Идентификатор счёта.</param>
        /// <returns>Результат операции.</returns>
        public OperationResult ToggleFreeze(int accountId) =>
            accountService.ToggleFreeze(accountId);

        /// <summary>
        /// Выполняет перевод средств между счётами.
        /// </summary>
        /// <param name="senderId">Идентификатор счёта-отправителя.</param>
        /// <param name="targetNumber">Номер счёта-назначения.</param>
        /// <param name="amount">Сумма перевода.</param>
        /// <returns>Результат операции.</returns>
        public OperationResult Transfer(int senderId, string? targetNumber, decimal amount) =>
            transferService.Transfer(senderId, targetNumber, amount);
    }
}

